using PrimeAlgorithm.Contracts;

namespace Wheel210Sieve;

/// <summary>
/// 210-轮位压缩筛（Wheel-210 Bitwise Sieve）
/// 使用前四个质数 2、3、5、7 构造模 210（= 2×3×5×7）的质数轮：
/// - φ(210) = 48，每 210 个连续整数中仅有 48 个候选数（约 22.9%），
///   远少于 30-轮的 26.7% 或纯奇数筛的 50%。
/// - 每个 210-数块的 48 个候选位压缩存储在 1 个 ulong 中（48/64 位），
///   n=1M 时整个筛数组约 38 KB，通常可完全装入 L1 缓存。
/// - 预计算乘积余数表，内层标记循环无需模运算。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n/210 × 48 位) ≈ O(n/35)
///
/// 参考算法思想来自：
///   J. Sorenson, "An Analysis of Two Prime Number Sieves", 1991.
///   T. Oliveira e Silva, "Fast implementation of the segmented sieve of Eratosthenes", 2002.
///   Kim Walisch, "primesieve: Fast Prime Number Generator", primesieve.org, 2010-2024.
/// </summary>
public class Wheel210SievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "210-轮位压缩筛 (Wheel-210 Bitwise Sieve)";

    private const int WheelSize = 210;          // 2 × 3 × 5 × 7
    private const int WheelResidues = 48;       // φ(210)

    // The 48 residues mod 210 coprime to 210
    private static readonly int[] Residues = BuildResidues();

    // Gaps between consecutive residues (with wrap-around: last gap brings 209 → 210+1=211)
    private static readonly int[] Gaps = BuildGaps();

    // residue → index in Residues, or -1 when gcd(r,210) > 1
    private static readonly sbyte[] ResidueIndex = BuildResidueIndex();

    // ProductIndex[i,j] = index in Residues of (Residues[i] * Residues[j] % 210)
    // Used to avoid expensive modulo in the inner sieve loop.
    private static readonly int[,] ProductIndex = BuildProductIndex();

    // ──────────────────────────── static initializers ────────────────────────

    private static int[] BuildResidues()
    {
        var res = new List<int>(WheelResidues);
        for (int i = 1; i < WheelSize; i++)
            if (Gcd(i, WheelSize) == 1)
                res.Add(i);
        return [.. res];
    }

    private static int[] BuildGaps()
    {
        var gaps = new int[WheelResidues];
        for (int i = 0; i < WheelResidues; i++)
        {
            int next = (i < WheelResidues - 1) ? Residues[i + 1] : Residues[0] + WheelSize;
            gaps[i] = next - Residues[i];
        }
        return gaps;
    }

    private static sbyte[] BuildResidueIndex()
    {
        var map = new sbyte[WheelSize];
        Array.Fill(map, (sbyte)-1);
        for (int i = 0; i < WheelResidues; i++)
            map[Residues[i]] = (sbyte)i;
        return map;
    }

    private static int[,] BuildProductIndex()
    {
        var table = new int[WheelResidues, WheelResidues];
        for (int i = 0; i < WheelResidues; i++)
            for (int j = 0; j < WheelResidues; j++)
                table[i, j] = ResidueIndex[(Residues[i] * Residues[j]) % WheelSize];
        return table;
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0) { int t = b; b = a % b; a = t; }
        return a;
    }

    // ──────────────────────────── IsPrime ────────────────────────────────────

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5 || n == 7) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0 || n % 7 == 0) return false;
        if (ResidueIndex[(int)(n % WheelSize)] < 0) return false;

        // Trial division with 210-wheel: candidates are multiples of 210 plus each of the 48 residues.
        // The first candidate divisor ≥ 11 corresponds to Residues[1] = 11.
        long i = Residues[1]; // = 11
        int ri = 1;
        while (i * i <= n)
        {
            if (n % i == 0) return false;
            i += Gaps[ri];
            ri = (ri + 1) % WheelResidues;
        }
        return true;
    }

    // ──────────────────────────── GetPrimesUpTo ──────────────────────────────

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;
        if (max >= 2) yield return 2;
        if (max >= 3) yield return 3;
        if (max >= 5) yield return 5;
        if (max >= 7) yield return 7;
        if (max < 11) yield break;

        // ── Allocate bit-packed composite array ──────────────────────────────
        // Block b covers candidates  210*b + Residues[0..47].
        // Bit ri of isComposite[b] is set iff 210*b + Residues[ri] is composite.
        long numBlocks = max / WheelSize + 2;
        var isComposite = new ulong[numBlocks];

        // Residues[0] = 1 is not prime; mark it so the output loop skips it.
        isComposite[0] |= 1UL;

        long sqrtMax = (long)Math.Sqrt((double)max) + 1;

        // ── Sieve: mark composites ────────────────────────────────────────────
        // Outer loop walks all candidates in [11, sqrtMax] using the wheel.
        // For each unmarked candidate p (a prime), mark composites p*q for q ≥ p.
        for (long b = 0; b * WheelSize < sqrtMax + WheelSize; b++)
        {
            ulong blockBits = isComposite[b];
            int riStart = (b == 0) ? 1 : 0; // skip residue 1 when b==0

            for (int ri = riStart; ri < WheelResidues; ri++)
            {
                if ((blockBits & (1UL << ri)) != 0) continue; // composite, skip

                long p = (long)WheelSize * b + Residues[ri];
                if (p > sqrtMax) goto afterSieve; // primes > √max have p*p > max

                // Mark composites p*q for all wheel-coprime q ≥ p.
                // We start q = p (block=b, residue=ri), then advance via wheel gaps.
                // composite = p * q; residue index of composite = ProductIndex[ri, qi].
                long composite = p * p;
                int qi = ri; // q starts at p, so q%210 = Residues[ri]

                while (composite <= max)
                {
                    long cb = composite / WheelSize;
                    int cri = ProductIndex[ri, qi];
                    isComposite[cb] |= 1UL << cri;

                    composite += (long)p * Gaps[qi];
                    qi = (qi + 1) % WheelResidues;
                }
            }
        }
        afterSieve:;

        // ── Output: yield all unmarked candidates in [11, max] ───────────────
        for (long b = 0; b * WheelSize <= max; b++)
        {
            ulong bits = isComposite[b];
            int riStart = (b == 0) ? 1 : 0; // skip the "1" entry

            for (int ri = riStart; ri < WheelResidues; ri++)
            {
                if ((bits & (1UL << ri)) == 0)
                {
                    long n = (long)WheelSize * b + Residues[ri];
                    if (n > max) yield break;
                    yield return n;
                }
            }
        }
    }
}
