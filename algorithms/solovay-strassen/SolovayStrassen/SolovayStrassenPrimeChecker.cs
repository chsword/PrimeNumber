using PrimeAlgorithm.Contracts;

namespace SolovayStrassen;

/// <summary>
/// 索洛维-斯特拉森素性测试（Solovay-Strassen Primality Test）
/// 基于欧拉准则：若 p 为奇质数且 gcd(a,p)=1，则 a^((p-1)/2) ≡ (a/p) (mod p)
/// 其中 (a/p) 为雅可比符号。使用确定性证明集合使其对 long 范围内所有数均正确。
/// 时间复杂度（单次）：O(k log² n)，空间复杂度：O(1)
/// </summary>
public class SolovayStrassenPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "索洛维-斯特拉森素性测试 (Solovay-Strassen)";

    // Deterministic witnesses sufficient for all n < 3.3 × 10^24 (covers entire long range)
    private static readonly long[] Witnesses = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0) return false;

        foreach (var a in Witnesses)
        {
            if (a >= n) continue;
            long jacobi = ((Jacobi(a, n) % n) + n) % n;
            long euler = ModPow(a, (n - 1) / 2, n);
            if (jacobi == 0 || euler != jacobi)
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

    /// <summary>Computes the Jacobi symbol (a/n) for odd n > 0.</summary>
    private static long Jacobi(long a, long n)
    {
        if (n <= 0 || n % 2 == 0) return 0;
        a %= n;
        if (a < 0) a += n;
        long result = 1;
        while (a != 0)
        {
            while (a % 2 == 0)
            {
                a /= 2;
                long mod8 = n % 8;
                if (mod8 == 3 || mod8 == 5) result = -result;
            }
            (a, n) = (n, a);
            if (a % 4 == 3 && n % 4 == 3) result = -result;
            a %= n;
        }
        return n == 1 ? result : 0;
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
        => (long)((Int128)a * b % mod);
}
