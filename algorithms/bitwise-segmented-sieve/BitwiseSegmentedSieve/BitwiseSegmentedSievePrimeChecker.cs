using PrimeAlgorithm.Contracts;

namespace BitwiseSegmentedSieve;

/// <summary>
/// 位压缩分段筛法（Bitwise Segmented Sieve）
/// 将位压缩筛法（只存奇数，每个数占 1 位）与分段筛相结合：
/// 使用约 L1 缓存大小的段，每段用 ulong 位数组表示奇数，极大降低缓存失效率。
/// 时间复杂度：O(n log log n)，空间复杂度：O(√n + n/16)（bit 级别）
/// </summary>
public class BitwiseSegmentedSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "位压缩分段筛法 (Bitwise Segmented Sieve)";

    // Each segment covers this many odd numbers; tune to ~16 KB worth of bits
    // 16 KB = 16384 bytes = 131072 bits → 131072 odd numbers per segment
    private const int BitsPerSegment = 1 << 17; // 131072

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
        yield return 2;
        if (max < 3) yield break;

        long sqrtMax = (long)Math.Sqrt(max) + 1;

        // Step 1: sieve small primes up to sqrt(max) with simple bit sieve
        // Index k represents odd number (2k+1): k=0→1, k=1→3, k=2→5, ...
        long smallOddCount = (sqrtMax - 1) / 2;
        int smallWords = (int)((smallOddCount + 63) / 64);
        var smallBits = new ulong[smallWords + 1];

        for (long k = 1; 2 * k + 1 <= sqrtMax; k++) // k=1 → 3
        {
            if (IsBitClear(smallBits, k))
            {
                long p = 2 * k + 1;
                long pSq = p * p;
                long startK = (pSq - 1) / 2;
                for (long j = startK; j <= smallOddCount; j += p)
                    SetBit(smallBits, j);
            }
        }

        var smallPrimes = new List<long>();
        for (long k = 1; k <= smallOddCount; k++)
        {
            if (IsBitClear(smallBits, k))
                smallPrimes.Add(2 * k + 1);
        }

        // Yield small primes (we already yielded 2 above)
        foreach (var p in smallPrimes)
        {
            if (p <= max) yield return p;
        }

        // Step 2: process remaining odd numbers in segments
        // Segment k covers odd numbers from (2*segStart+1) to (2*(segStart+BitsPerSegment-1)+1)
        long maxOddK = (max - 1) / 2;           // index of largest odd ≤ max
        long firstSegK = (sqrtMax | 1) == sqrtMax ? (sqrtMax - 1) / 2 + 1 : (sqrtMax + 1) / 2; // first odd k > sqrtMax

        if (firstSegK > maxOddK) yield break;

        int words = (BitsPerSegment + 63) / 64;
        var segBits = new ulong[words];
        var spArr = smallPrimes.ToArray();

        for (long segStart = firstSegK; segStart <= maxOddK; segStart += BitsPerSegment)
        {
            long segEnd = Math.Min(segStart + BitsPerSegment - 1, maxOddK);
            int len = (int)(segEnd - segStart + 1);
            int lenWords = (len + 63) / 64;

            // Reset used words
            Array.Clear(segBits, 0, lenWords);

            // Mark composites using small primes
            foreach (var p in spArr)
            {
                // Odd index of p: kp = (p-1)/2
                // First odd multiple of p at or after segStart: find k such that 2k+1 ≡ 0 (mod p), k >= segStart
                long firstOddMultiple = ((2 * segStart + 1 + p - 1) / p) * p;
                if ((firstOddMultiple & 1) == 0) firstOddMultiple += p; // must be odd multiple
                long firstK = (firstOddMultiple - 1) / 2;
                // firstK >= segStart is guaranteed by construction; the +p step is a defensive
                // guard for the extremely rare edge case where rounding pushes firstK below segStart.
                if (firstK < segStart) firstK += p;

                for (long j = firstK; j <= segEnd; j += p)
                    SetBit(segBits, j - segStart);
            }

            // Yield unmarked (prime) candidates
            for (int i = 0; i < len; i++)
            {
                if (IsBitClear(segBits, i))
                    yield return 2 * (segStart + i) + 1;
            }
        }
    }

    private static bool IsBitClear(ulong[] bits, long index)
        => (bits[index >> 6] & (1UL << (int)(index & 63))) == 0;

    private static void SetBit(ulong[] bits, long index)
        => bits[index >> 6] |= 1UL << (int)(index & 63);
}
