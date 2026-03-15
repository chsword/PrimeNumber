using PrimeAlgorithm.Contracts;

namespace Wheel30SegmentedSieve;

/// <summary>
/// 30-轮分段筛法（Wheel-30 Segmented Sieve）
/// 结合 30-轮（模 30 的 8 个余数类）与分段筛两种技术：
/// - 30-轮跳过所有 2、3、5 的倍数（输出时仅遍历约 26.7% 的候选数）
/// - 分段处理保证良好的缓存局部性（每段约等于 √n）
/// 时间复杂度：O(n log log n)，空间复杂度：O(√n)
/// </summary>
public class Wheel30SegmentedSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "30-轮分段筛法 (Wheel-30 Segmented Sieve)";

    // The 8 residues mod 30 coprime to 30
    private static readonly int[] Residues = [1, 7, 11, 13, 17, 19, 23, 29];

    // Map residue mod 30 → index in Residues, -1 if not coprime to 30
    private static readonly int[] ResidueIndex = BuildResidueIndex();

    private static int[] BuildResidueIndex()
    {
        var map = new int[30];
        Array.Fill(map, -1);
        for (int i = 0; i < Residues.Length; i++)
            map[Residues[i]] = i;
        return map;
    }

    // Gap from residue[i] to residue[(i+1)%8]  {1→7, 7→11, 11→13, 13→17, 17→19, 19→23, 23→29, 29→31}
    private static readonly int[] Gaps = [6, 4, 2, 4, 2, 4, 6, 2];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0) return false;
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

        long sqrtMax = (long)Math.Sqrt((double)max) + 1;

        // Phase 1: find small primes in [7, sqrtMax] with simple Eratosthenes
        var smallSieve = new bool[sqrtMax + 1]; // true = composite
        for (long i = 2; i * i <= sqrtMax; i++)
        {
            if (!smallSieve[i])
                for (long j = i * i; j <= sqrtMax; j += i)
                    smallSieve[j] = true;
        }

        var smallPrimes = new List<long>();
        for (long i = 7; i <= sqrtMax; i++)
        {
            if (!smallSieve[i])
            {
                smallPrimes.Add(i);
                if (i <= max) yield return i;
            }
        }

        if (sqrtMax >= max) yield break;

        long segSize = Math.Max(sqrtMax, 1);
        var seg = new bool[segSize]; // true = composite
        var spArr = smallPrimes.ToArray();

        for (long low = sqrtMax + 1; low <= max; low += segSize)
        {
            long high = Math.Min(low + segSize - 1, max);
            int len = (int)(high - low + 1);

            Array.Fill(seg, false, 0, len);

            // Mark composites using small primes
            foreach (var p in spArr)
            {
                long start = ((low + p - 1) / p) * p;
                if (start == p) start += p;
                for (long j = start; j <= high; j += p)
                    seg[j - low] = true;
            }

            // Output: walk segment using 30-wheel gaps to skip multiples of 2, 3, 5
            // Find first candidate (coprime to 30) that is >= low
            long cur = low;
            while (cur <= high && ResidueIndex[(int)(cur % 30)] < 0) cur++;
            if (cur > high) continue;

            int ri = ResidueIndex[(int)(cur % 30)];
            while (cur <= high)
            {
                if (!seg[cur - low]) yield return cur;
                cur += Gaps[ri];
                ri = (ri + 1) % 8;
            }
        }
    }
}

