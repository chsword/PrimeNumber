using PrimeAlgorithm.Contracts;

namespace LinearSieve;

/// <summary>
/// 欧拉线性筛（Linear Sieve / Euler's Sieve）
/// 每个合数只被其最小质因子筛去，从而实现真正的 O(n) 线性筛。
/// 时间复杂度：O(n)，空间复杂度：O(n)
/// </summary>
public class LinearSievePrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "欧拉线性筛 (Linear Sieve)";

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

        // Linear sieve stores smallest prime factor (spf) for each number
        var spf = new int[max + 1];    // smallest prime factor
        var primes = new List<long>();

        for (long i = 2; i <= max; i++)
        {
            if (spf[i] == 0)
            {
                // i is prime
                spf[i] = (int)i;
                primes.Add(i);
            }

            // Mark composites: for each prime p <= spf[i], mark i*p
            foreach (var p in primes)
            {
                if (p > spf[i] || i * p > max) break;
                spf[i * p] = (int)p;
            }
        }

        foreach (var p in primes)
            yield return p;
    }
}
