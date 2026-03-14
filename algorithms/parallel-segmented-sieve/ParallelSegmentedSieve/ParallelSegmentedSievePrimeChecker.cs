using PrimeAlgorithm.Contracts;

namespace ParallelSegmentedSieve;

/// <summary>
/// 并行分段筛法（Parallel Segmented Sieve of Eratosthenes）
/// 在分段筛的基础上，使用多线程并行处理各段，充分利用多核 CPU 性能。
/// 每段大小约为 L1 缓存友好的 32KB，各段独立筛选后合并结果。
/// 时间复杂度：O(n log log n / P)（P 为 CPU 核心数），空间复杂度：O(√n + n/P)
/// </summary>
public class ParallelSegmentedSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "并行分段筛法 (Parallel Segmented Sieve)";

    // Segment size tuned to fit in L1 cache (~32 KB → 32768 booleans)
    private const int SegmentSize = 1 << 15; // 32768

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

        long sqrtMax = (long)Math.Sqrt(max) + 1;

        // Step 1: sieve small primes up to sqrt(max) sequentially
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
                smallPrimes.Add(i);
        }

        // Yield small primes first
        foreach (var p in smallPrimes)
        {
            if (p <= max) yield return p;
        }

        // Step 2: divide remaining range [sqrtMax+1, max] into segments
        long rangeStart = sqrtMax + 1;
        if (rangeStart > max) yield break;

        long totalRange = max - rangeStart + 1;
        int segCount = (int)((totalRange + SegmentSize - 1) / SegmentSize);

        // Allocate result storage per segment (will be sorted at end)
        var segmentResults = new List<long>[segCount];
        for (int s = 0; s < segCount; s++)
            segmentResults[s] = new List<long>();

        var smallPrimesArray = smallPrimes.ToArray();

        // Step 3: process segments in parallel
        Parallel.For(0, segCount, s =>
        {
            long low = rangeStart + (long)s * SegmentSize;
            long high = Math.Min(low + SegmentSize - 1, max);
            int len = (int)(high - low + 1);

            var sieve = new bool[len];
            Array.Fill(sieve, true);

            foreach (var p in smallPrimesArray)
            {
                // Find first multiple of p >= low
                long start = ((low + p - 1) / p) * p;
                if (start == p) start += p; // p itself is prime, skip it
                for (long j = start; j <= high; j += p)
                    sieve[j - low] = false;
            }

            var local = segmentResults[s];
            for (int i = 0; i < len; i++)
            {
                if (sieve[i]) local.Add(low + i);
            }
        });

        // Step 4: yield results in order
        foreach (var seg in segmentResults)
        {
            foreach (var p in seg) yield return p;
        }
    }
}
