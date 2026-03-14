using PrimeAlgorithm.Contracts;

namespace SieveOfAtkin;

/// <summary>
/// 阿特金筛法（Sieve of Atkin）
/// 通过特定二次方程的解来标记质数，是一种比埃氏筛更现代的算法。
/// 时间复杂度：O(n / log log n)，空间复杂度：O(n)
/// </summary>
public class SieveOfAtkinPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "阿特金筛法 (Sieve of Atkin)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;
        return GetPrimesUpTo(n).Any(p => p == n);
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        if (max < 2) yield break;
        if (max >= 2) yield return 2;
        if (max >= 3) yield return 3;
        if (max < 5) yield break;

        var isPrime = new bool[max + 1];

        // For each (x, y) pair, apply the three quadratic forms
        for (long x = 1; x * x <= max; x++)
        {
            for (long y = 1; y * y <= max; y++)
            {
                long n1 = 4 * x * x + y * y;
                if (n1 <= max && (n1 % 12 == 1 || n1 % 12 == 5))
                    isPrime[n1] ^= true;

                long n2 = 3 * x * x + y * y;
                if (n2 <= max && n2 % 12 == 7)
                    isPrime[n2] ^= true;

                long n3 = 3 * x * x - y * y;
                if (x > y && n3 <= max && n3 % 12 == 11)
                    isPrime[n3] ^= true;
            }
        }

        // Eliminate composites by sieving out perfect-square multiples
        for (long r = 5; r * r <= max; r++)
        {
            if (isPrime[r])
            {
                long r2 = r * r;
                for (long k = r2; k <= max; k += r2)
                    isPrime[k] = false;
            }
        }

        for (long i = 5; i <= max; i++)
        {
            if (isPrime[i]) yield return i;
        }
    }
}
