using PrimeAlgorithm.Contracts;

namespace SieveOfZakiya;

/// <summary>
/// 扎基亚质数筛法（Sieve of Zakiya, SoZ7）
/// 基于 30-轮（2×3×5=30）的质数筛法，仅考虑模 30 余数属于
/// {1,7,11,13,17,19,23,29} 的整数（占 8/30 ≈ 26.7%），
/// 大幅减少候选数和内存占用。利用轮增量数组跳过不可能的候选数。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n×8/30) ≈ O(n/4)
/// </summary>
public class SieveOfZakiyaPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "扎基亚质数筛法 (Sieve of Zakiya, SoZ7)";

    // The 8 residues mod 30 coprime to 30
    private static readonly int[] Residues = [1, 7, 11, 13, 17, 19, 23, 29];

    // Map residue value → index in Residues array (-1 if not coprime to 30)
    private static readonly int[] ResidueIndex = BuildResidueIndex();

    private static int[] BuildResidueIndex()
    {
        var map = new int[30];
        Array.Fill(map, -1);
        for (int i = 0; i < Residues.Length; i++)
            map[Residues[i]] = i;
        return map;
    }

    // Gap to add to move from residue index i to residue index (i+1)%8
    // e.g. Residues = {1,7,11,13,17,19,23,29} → gaps = {6,4,2,4,2,4,6,2}
    private static readonly int[] Gaps = BuildGaps();

    private static int[] BuildGaps()
    {
        var gaps = new int[8];
        for (int i = 0; i < 8; i++)
        {
            int cur = Residues[i];
            int nxt = Residues[(i + 1) % 8];
            gaps[i] = (nxt - cur + 30) % 30;
        }
        return gaps;
    }

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0) return false;
        // Walk candidates using the gap table
        long i = 7;
        int ri = 1; // index of 7 in Residues
        while (i * i <= n)
        {
            if (n % i == 0) return false;
            i += Gaps[ri];
            ri = (ri + 1) % 8;
        }
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;
        if (max >= 2) yield return 2;
        if (max >= 3) yield return 3;
        if (max >= 5) yield return 5;
        if (max < 7) yield break;

        // isComposite[b * 8 + ri] represents candidate 30*b + Residues[ri]
        long blocks = max / 30 + 1;
        var isComposite = new bool[blocks * 8];

        long sqrtMax = (long)Math.Sqrt((double)max);

        // For each prime p (candidate not yet marked composite and p <= sqrt(max)),
        // mark composites p*q for all candidates q >= p.
        // Since both p and q are coprime to 30, p*q is also coprime to 30,
        // so p*q always maps to a valid residue slot.
        for (long b = 0; b * 30 + Residues[0] <= sqrtMax + 30; b++)
        {
            for (int ri = 0; ri < 8; ri++)
            {
                long p = 30 * b + Residues[ri];
                if (p < 7) continue;
                if (p > sqrtMax) goto doneOuter;
                if (isComposite[b * 8 + ri]) continue;

                // Mark composites: iterate q from p (using wheel gaps), compute p*q
                long q = p;
                int qi = ri;
                while (true)
                {
                    long composite = p * q;
                    if (composite > max) break;

                    long cb = composite / 30;
                    int cri = ResidueIndex[(int)(composite % 30)];
                    isComposite[cb * 8 + cri] = true;

                    q += Gaps[qi];
                    qi = (qi + 1) % 8;
                }
            }
        }
        doneOuter:;

        // Yield all unmarked candidates in [7, max]
        for (long b = 0; b < blocks; b++)
        {
            for (int ri = 0; ri < 8; ri++)
            {
                long n = 30 * b + Residues[ri];
                if (n < 7 || n > max) continue;
                if (!isComposite[b * 8 + ri]) yield return n;
            }
        }
    }
}
