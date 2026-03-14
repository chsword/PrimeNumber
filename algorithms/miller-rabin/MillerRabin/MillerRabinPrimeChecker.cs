using PrimeAlgorithm.Contracts;

namespace MillerRabin;

/// <summary>
/// 米勒-拉宾素性测试（Miller-Rabin Primality Test）
/// 使用确定性证明集合（针对 long 范围内所有数），无伪素数。
/// 时间复杂度（单次）：O(k log² n)，空间复杂度：O(1)
/// </summary>
public class MillerRabinPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "米勒-拉宾素性测试 (Miller-Rabin)";

    // Deterministic witnesses sufficient for all n < 3,317,044,064,679,887,385,961,981
    // which covers the entire long range (max ~9.2 × 10^18)
    private static readonly long[] Witnesses = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        // Write n-1 as 2^r * d
        long d = n - 1;
        int r = 0;
        while (d % 2 == 0) { d /= 2; r++; }

        foreach (var a in Witnesses)
        {
            if (a >= n) continue;
            if (!IsStrongPseudoPrime(n, a, d, r))
                return false;
        }
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        for (long i = 2; i <= max; i++)
        {
            if (IsPrime(i)) yield return i;
        }
    }

    private static bool IsStrongPseudoPrime(long n, long a, long d, int r)
    {
        long x = ModPow(a, d, n);
        if (x == 1 || x == n - 1) return true;
        for (int i = 0; i < r - 1; i++)
        {
            x = ModMul(x, x, n);
            if (x == n - 1) return true;
        }
        return false;
    }

    /// <summary>Computes (base^exp) % mod using fast exponentiation.</summary>
    private static long ModPow(long baseVal, long exp, long mod)
    {
        long result = 1;
        baseVal %= mod;
        while (exp > 0)
        {
            if ((exp & 1) == 1) result = ModMul(result, baseVal, mod);
            baseVal = ModMul(baseVal, baseVal, mod);
            exp >>= 1;
        }
        return result;
    }

    /// <summary>Computes (a * b) % mod without overflow using 128-bit arithmetic.</summary>
    private static long ModMul(long a, long b, long mod)
    {
        return (long)((Int128)a * b % mod);
    }
}
