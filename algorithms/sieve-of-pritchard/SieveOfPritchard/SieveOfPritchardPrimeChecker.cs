using PrimeAlgorithm.Contracts;

namespace SieveOfPritchard;

/// <summary>
/// 普里查德筛法 (Sieve of Pritchard / Wheel Sieve)
/// 由Paul Pritchard于1982年提出的轮筛法，利用210-轮（2×3×5×7）跳过已知合数。
/// 时间复杂度：O(n / log log n)，空间复杂度：O(n)
/// </summary>
public class SieveOfPritchardPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "普里查德筛法 (Sieve of Pritchard)";

    // Gaps between consecutive numbers coprime to 2*3*5*7 = 210 (48 increments, cyclic)
    private static readonly int[] Wheel210 =
    [
        10, 2, 4, 2, 4, 6, 2, 6, 4, 2, 4, 6, 6, 2, 6, 4,
         2, 6, 4, 6, 8, 4, 2, 4, 2, 4, 8, 6, 4, 6, 2, 4,
         6, 2, 6, 6, 4, 2, 4, 6, 2, 6, 4, 2, 4, 2, 10, 2
    ];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5 || n == 7) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0 || n % 7 == 0) return false;
        for (long i = 11; i * i <= n; )
        {
            if (n % i == 0) return false;
            // Advance through the wheel: 6k±1 pattern is a subset; use 2,4 alternation
            i += (i % 6 == 5) ? 2L : 4L;
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

        // Mark multiples of the wheel primes 2, 3, 5, 7
        foreach (long p in (long[])[2, 3, 5, 7])
        {
            if (p > max) break;
            for (long j = p * 2; j <= max; j += p)
                sieve[j] = false;
        }

        long limit = (long)Math.Sqrt(max);

        // Sieve remaining candidates using 210-wheel for outer iteration
        long candidate = 11;
        int wi = 0;

        while (candidate <= limit)
        {
            if (sieve[candidate])
            {
                // Mark all multiples of candidate starting from candidate^2
                for (long j = candidate * candidate; j <= max; j += candidate)
                    sieve[j] = false;
            }
            candidate += Wheel210[wi];
            wi = (wi + 1) % 48;
        }

        // Yield all primes
        for (long i = 2; i <= max; i++)
        {
            if (sieve[i]) yield return i;
        }
    }
}
