using PrimeAlgorithm.Contracts;

namespace SieveOfEratosthenes;

/// <summary>
/// 埃拉托色尼筛法（Sieve of Eratosthenes）
/// 通过标记合数的方式，一次性筛出指定范围内所有质数。
/// 时间复杂度：O(n log log n)，空间复杂度：O(n)
/// </summary>
public class SieveOfEratosthenesPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "埃拉托色尼筛法 (Sieve of Eratosthenes)";

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

        var sieve = new bool[max + 1];
        Array.Fill(sieve, true);
        sieve[0] = sieve[1] = false;

        for (long i = 2; i * i <= max; i++)
        {
            if (sieve[i])
            {
                for (long j = i * i; j <= max; j += i)
                {
                    sieve[j] = false;
                }
            }
        }

        for (long i = 2; i <= max; i++)
        {
            if (sieve[i]) yield return i;
        }
    }
}
