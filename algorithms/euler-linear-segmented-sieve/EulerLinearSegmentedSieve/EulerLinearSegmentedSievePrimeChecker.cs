using PrimeAlgorithm.Contracts;

namespace EulerLinearSegmentedSieve;

/// <summary>
/// 欧拉线性分段筛法（Euler Linear Segmented Sieve）
/// 将欧拉线性筛与分段思想结合：先用欧拉线性筛求出 √n 以内的所有小素数，
/// 再用分段筛将大范围划分为若干段逐一处理，显著减少内存占用并提升缓存命中率。
/// 欧拉线性筛保证每个合数只被其最小质因子筛去（生成小素数部分 O(√n)）。
/// 时间复杂度：O(n log log n)，空间复杂度：O(√n)（分段部分）
/// </summary>
public class EulerLinearSegmentedSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "欧拉线性分段筛法 (Euler Linear Segmented Sieve)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if ((n & 1) == 0) return false;
        if (n == 3) return true;
        if (n % 3 == 0) return false;
        // Trial division using 6k±1 candidates: all primes > 3 are of the form 6k±1
        for (long i = 5; i * i <= n; i += (i % 6 == 5) ? 2L : 4L)
        {
            if (n % i == 0) return false;
        }
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;

        long sqrtMax = (long)Math.Sqrt((double)max) + 1;

        // Phase 1: Euler linear sieve to find all primes up to sqrtMax
        // Uses the property that each composite is marked exactly once (by its smallest prime factor)
        var spf = new int[sqrtMax + 1]; // smallest prime factor
        var smallPrimesList = new List<int>();

        for (int i = 2; i <= sqrtMax; i++)
        {
            if (spf[i] == 0)
            {
                spf[i] = i;
                smallPrimesList.Add(i);
            }
            foreach (var p in smallPrimesList)
            {
                if (p > spf[i] || (long)i * p > sqrtMax) break;
                spf[i * p] = p;
            }
        }

        // Yield small primes
        foreach (var p in smallPrimesList)
        {
            if (p <= max) yield return (long)p;
        }

        if (sqrtMax >= max) yield break;

        // Phase 2: segmented sieve using the small primes from Phase 1
        long segSize = Math.Max(sqrtMax, 1);
        var seg = new bool[segSize]; // true = composite
        var spArr = smallPrimesList.ToArray();

        for (long low = sqrtMax + 1; low <= max; low += segSize)
        {
            long high = Math.Min(low + segSize - 1, max);
            int len = (int)(high - low + 1);

            Array.Fill(seg, false, 0, len);

            foreach (var p in spArr)
            {
                long start = ((low + p - 1) / p) * p;
                if (start == p) start += p;
                for (long j = start; j <= high; j += p)
                    seg[j - low] = true;
            }

            for (long i = low; i <= high; i++)
            {
                if (!seg[i - low]) yield return i;
            }
        }
    }
}
