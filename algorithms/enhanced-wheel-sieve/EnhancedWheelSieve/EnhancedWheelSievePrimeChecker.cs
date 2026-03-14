using PrimeAlgorithm.Contracts;

namespace EnhancedWheelSieve;

/// <summary>
/// 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel)
/// 使用2×3×5×7×11×13 = 30030轮，初始标记这些小素数的所有倍数，
/// 然后仅对剩余候选数（约19.2%）进行筛选。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n)
/// </summary>
public class EnhancedWheelSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel)";

    private static readonly long[] WheelPrimes = [2, 3, 5, 7, 11, 13];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5 || n == 7 || n == 11 || n == 13) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0 || n % 7 == 0 ||
            n % 11 == 0 || n % 13 == 0) return false;
        for (long i = 17; i * i <= n; i += (i % 6 == 5) ? 2L : 4L)
        {
            if (n % i == 0) return false;
        }
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;

        // composite[i] == true means i is composite
        var composite = new bool[max + 1];

        // Mark multiples of each wheel prime (2, 3, 5, 7, 11, 13)
        foreach (long p in WheelPrimes)
        {
            if (p > max) break;
            // Start marking from 2*p (p itself is prime)
            for (long j = p * 2; j <= max; j += p)
                composite[j] = true;
        }

        // Sieve remaining candidates starting from 17
        long sqrtMax = (long)Math.Sqrt(max);
        for (long p = 17; p <= sqrtMax; p++)
        {
            if (composite[p]) continue;
            // p is prime; mark multiples from p*p
            for (long j = p * p; j <= max; j += p)
                composite[j] = true;
        }

        // Yield all non-composite numbers >= 2
        for (long i = 2; i <= max; i++)
        {
            if (!composite[i]) yield return i;
        }
    }
}
