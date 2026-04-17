using PrimeAlgorithm.Contracts;

namespace Wheel2310Sieve;

/// <summary>
/// 2310-轮位压缩筛（Wheel-2310 Bitwise Sieve）
/// 使用前五个质数 2、3、5、7、11 构造模 2310（= 2×3×5×7×11）的质数轮：
/// - φ(2310) = 480，每 2310 个连续整数中仅有 480 个候选数（约 20.8%），
///   优于 210-轮的 22.9%，进一步缩减筛选候选数量约 9%。
/// - 每个 2310-数块的 480 个候选位压缩存储在 8 个 ulong 中（480/512 位），
///   n=1M 时整个筛数组约 27 KB，可完全装入 L1 缓存。
/// - 内层标记循环通过增量模运算跟踪复合数余数，避免构造 480×480 乘积表
///   （否则需约 900 KB，无法驻留缓存），改用每个素数 480 项的增量数组
///   （1.9 KB，可驻留 L1 缓存）。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n/2310 × 480 位) ≈ O(n / 38.5)
///
/// 参考算法思想：
///   J. Sorenson, "An Analysis of Two Prime Number Sieves", 1991.
///   T. Oliveira e Silva, "Fast implementation of the segmented sieve of Eratosthenes", 2002.
///   Kim Walisch, "primesieve: Fast Prime Number Generator", primesieve.org, 2010-2024.
/// </summary>
public class Wheel2310SievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "2310-轮位压缩筛 (Wheel-2310 Bitwise Sieve)";

    private const int WheelSize = 2310;         // 2 × 3 × 5 × 7 × 11
    private const int WheelResidues = 480;      // φ(2310)
    private const int ULongsPerBlock = 8;       // ceil(480/64) = 8  (uses 480 of 512 bits)

    // The 480 integers in [1, 2309] coprime to 2310.
    private static readonly int[] Residues = BuildResidues();

    // Gap from Residues[i] to Residues[(i+1) % 480] (with wrap-around over WheelSize).
    private static readonly int[] Gaps = BuildGaps();

    // residue r → its index in Residues[], or -1 when gcd(r, 2310) > 1.
    private static readonly short[] ResidueIndex = BuildResidueIndex();

    // ──────────────────────────── static table builders ──────────────────────

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

    private static short[] BuildResidueIndex()
    {
        var map = new short[WheelSize];
        Array.Fill(map, (short)-1);
        for (int i = 0; i < WheelResidues; i++)
            map[Residues[i]] = (short)i;
        return map;
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
        if (n == 2 || n == 3 || n == 5 || n == 7 || n == 11) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0 || n % 7 == 0 || n % 11 == 0) return false;
        if (ResidueIndex[(int)(n % WheelSize)] < 0) return false;

        // Trial division: candidates start at 13 (= Residues[1]) and advance via wheel gaps.
        long i = Residues[1]; // 13
        int ri = 1;
        while (i * i <= n)
        {
            if (n % i == 0) return false;
            i += Gaps[ri];
            if (++ri == WheelResidues) ri = 0;
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
        if (max >= 11) yield return 11;
        if (max < 13) yield break;

        // ── Allocate bit-packed composite array ──────────────────────────────
        // Block b covers the 480 candidates  2310*b + Residues[0..479].
        // Within each block, residue index ri occupies bit (ri & 63) of ulong
        // at position b*8 + (ri >> 6).
        long numBlocks = max / WheelSize + 2;
        var isComposite = new ulong[numBlocks * ULongsPerBlock];

        // Residues[0] = 1 is not prime; mark it so the output loop skips it.
        isComposite[0] |= 1UL; // block 0, ulong 0, bit 0

        long sqrtMax = (long)Math.Sqrt((double)max) + 1;

        // ── Sieve: mark composites ────────────────────────────────────────────
        // For each prime p (unmarked candidate ≤ √max), mark all composites p·q
        // for wheel-coprime q ≥ p.  The residue of p·q mod 2310 is tracked
        // incrementally: after each step q → next_q, the composite residue
        // advances by (p_resid × gap[qi]) mod 2310.
        for (long b = 0; b * WheelSize < sqrtMax + WheelSize; b++)
        {
            long bOffset = b * ULongsPerBlock;
            int riStart = (b == 0) ? 1 : 0;

            for (int ri = riStart; ri < WheelResidues; ri++)
            {
                if ((isComposite[bOffset + (ri >> 6)] & (1UL << (ri & 63))) != 0)
                    continue; // already marked composite

                long p = (long)WheelSize * b + Residues[ri];
                if (p > sqrtMax) goto afterSieve;

                // Per-prime incremental step table: pMods[qi] = (p % 2310 * Gaps[qi]) % 2310
                // Using a fixed-size array avoids heap allocation churn.
                var pMods = new int[WheelResidues];
                long pResid = Residues[ri];
                for (int qi = 0; qi < WheelResidues; qi++)
                    pMods[qi] = (int)(pResid * Gaps[qi] % WheelSize);

                // Mark composites p·q for all wheel-coprime q ≥ p.
                // compositeModW tracks composite % WheelSize incrementally.
                long composite = p * p;
                int compositeModW = (int)(composite % WheelSize);
                int cri = ResidueIndex[compositeModW];
                int qi_mark = ri;

                while (composite <= max)
                {
                    long cb = composite / WheelSize;
                    isComposite[cb * ULongsPerBlock + (cri >> 6)] |= 1UL << (cri & 63);

                    composite += p * Gaps[qi_mark];
                    compositeModW += pMods[qi_mark];
                    if (compositeModW >= WheelSize) compositeModW -= WheelSize;
                    cri = ResidueIndex[compositeModW];
                    if (++qi_mark == WheelResidues) qi_mark = 0;
                }
            }
        }
        afterSieve:;

        // ── Output: yield all unmarked candidates in [13, max] ───────────────
        for (long b = 0; ; b++)
        {
            long blockBase = (long)WheelSize * b;
            if (blockBase > max) yield break;

            long bOffset = b * ULongsPerBlock;
            int riStart = (b == 0) ? 1 : 0;

            for (int ri = riStart; ri < WheelResidues; ri++)
            {
                if ((isComposite[bOffset + (ri >> 6)] & (1UL << (ri & 63))) == 0)
                {
                    long n = blockBase + Residues[ri];
                    if (n > max) yield break;
                    yield return n;
                }
            }
        }
    }
}
