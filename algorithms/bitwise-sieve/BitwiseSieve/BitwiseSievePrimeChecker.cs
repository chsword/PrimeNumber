using PrimeAlgorithm.Contracts;

namespace BitwiseSieve;

/// <summary>
/// 位压缩筛法（Bitwise Sieve）
/// 使用位数组代替布尔数组，将内存占用压缩为原来的 1/8，同时只处理奇数节省一半空间。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n/8)
/// </summary>
public class BitwiseSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "位压缩筛法 (Bitwise Sieve)";

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
        yield return 2;
        if (max < 3) yield break;

        // Only represent odd numbers: index k represents 2k+1
        // For number n (odd): index = (n-1)/2
        // Numbers 3,5,7,9,... => indices 1,2,3,4,...
        long oddCount = (max - 1) / 2; // index of max (or slightly below)
        int arraySize = (int)((oddCount + 63) / 64);
        var bits = new ulong[arraySize];

        // Mark composites (odd multiples of odd primes)
        // Start from 3 (index 1)
        for (long i = 1; 2 * i + 1 <= (long)Math.Sqrt(max) + 1; i++)
        {
            if (IsBitClear(bits, i))
            {
                long prime = 2 * i + 1;
                // First odd multiple of prime that is composite: prime*prime
                long start = prime * prime;
                if (start > max) break;
                long startIdx = (start - 1) / 2;
                // Step is prime (skipping even multiples)
                for (long j = startIdx; j <= oddCount; j += prime)
                    SetBit(bits, j);
            }
        }

        for (long i = 1; i <= oddCount; i++)
        {
            if (IsBitClear(bits, i))
                yield return 2 * i + 1;
        }
    }

    private static bool IsBitClear(ulong[] bits, long index)
        => (bits[index >> 6] & (1UL << (int)(index & 63))) == 0;

    private static void SetBit(ulong[] bits, long index)
        => bits[index >> 6] |= 1UL << (int)(index & 63);
}
