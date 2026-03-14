using PrimeAlgorithm.Contracts;

namespace SegmentedSieve;

/// <summary>
/// 分段筛法（Segmented Sieve of Eratosthenes）
/// 将筛区间分成大小为 √n 的段，逐段筛选，大幅减少缓存失效，提升内存局部性。
/// 时间复杂度：O(n log log n)，空间复杂度：O(√n)
/// </summary>
public class SegmentedSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "分段筛法 (Segmented Sieve)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;
        if (n == 3) return true;
        if (n % 3 == 0) return false;
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

        long sqrtMax = (long)Math.Sqrt(max) + 1;

        // Step 1: find small primes up to sqrt(max) using simple sieve
        var smallSieve = new bool[sqrtMax + 1];
        Array.Fill(smallSieve, true);
        smallSieve[0] = smallSieve[1] = false;
        for (long i = 2; i * i <= sqrtMax; i++)
        {
            if (smallSieve[i])
                for (long j = i * i; j <= sqrtMax; j += i)
                    smallSieve[j] = false;
        }

        var smallPrimes = new List<long>();
        for (long i = 2; i <= sqrtMax; i++)
        {
            if (smallSieve[i])
            {
                smallPrimes.Add(i);
                if (i <= max) yield return i;
            }
        }

        // Step 2: process segments
        long segSize = Math.Max(sqrtMax, 1);
        var segment = new bool[segSize];

        for (long low = sqrtMax + 1; low <= max; low += segSize)
        {
            long high = Math.Min(low + segSize - 1, max);
            Array.Fill(segment, true, 0, (int)(high - low + 1));

            foreach (var p in smallPrimes)
            {
                // First multiple of p in [low, high]
                long start = ((low + p - 1) / p) * p;
                if (start == p) start += p;
                for (long j = start; j <= high; j += p)
                    segment[j - low] = false;
            }

            for (long i = low; i <= high; i++)
            {
                if (segment[i - low]) yield return i;
            }
        }
    }
}
