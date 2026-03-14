using PrimeAlgorithm.Contracts;

namespace SieveOfSundaram;

/// <summary>
/// 孙德兰筛法（Sieve of Sundaram）
/// 通过排除形如 i+j+2ij 的数，从剩余数中生成所有奇质数（再加上 2）。
/// 时间复杂度：O(n log n)，空间复杂度：O(n)
/// </summary>
public class SieveOfSundaramPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "孙德兰筛法 (Sieve of Sundaram)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;
        // For a single number, use the sieve on range [n, n]
        return GetPrimesUpTo(n).Any(p => p == n);
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;
        yield return 2;
        if (max < 3) yield break;

        // We want all odd primes of the form 2k+1 up to max.
        // k ranges from 1 to (max-1)/2
        long limit = (max - 1) / 2;
        var isComposite = new bool[limit + 1];

        // Mark i+j+2ij for 1 <= i <= j, i+j+2ij <= limit
        for (long i = 1; i <= limit; i++)
        {
            // j >= i and i+j+2ij <= limit => j(1+2i) <= limit-i => j <= (limit-i)/(1+2i)
            long jMax = (limit - i) / (1 + 2 * i);
            for (long j = i; j <= jMax; j++)
            {
                isComposite[i + j + 2 * i * j] = true;
            }
        }

        for (long k = 1; k <= limit; k++)
        {
            if (!isComposite[k])
                yield return 2 * k + 1;
        }
    }
}
