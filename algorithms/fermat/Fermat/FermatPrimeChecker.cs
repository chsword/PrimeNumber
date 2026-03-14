using PrimeAlgorithm.Contracts;

namespace Fermat;

/// <summary>
/// 费马素性测试 (Fermat Primality Test)
/// 基于费马小定理：若p为素数且gcd(a,p)=1，则a^(p-1) ≡ 1 (mod p)。
/// 使用固定的12个基，对long范围内的合数（包括卡迈克尔数）有效检测。
/// 时间复杂度：O(k log² n)，空间复杂度：O(1)
/// </summary>
public class FermatPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "费马素性测试 (Fermat Primality Test)";

    private static readonly long[] Bases = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        foreach (long a in Bases)
        {
            if (a >= n) continue;
            // Fermat test: a^(n-1) ≡ 1 (mod n)
            if (ModPow(a, n - 1, n) != 1) return false;
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

    /// <summary>Computes (a * b) % mod without overflow using Int128.</summary>
    private static long ModMul(long a, long b, long mod)
    {
        return (long)((Int128)a * b % mod);
    }
}
