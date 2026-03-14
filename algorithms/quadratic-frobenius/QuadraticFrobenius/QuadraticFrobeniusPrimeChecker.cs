using PrimeAlgorithm.Contracts;

namespace QuadraticFrobenius;

/// <summary>
/// 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test)
/// 由Grantham (2000)提出，强于米勒-拉宾测试。
/// 在Z_n[x]/(x²-x-c)中计算x^(n+1)，验证弗罗贝尼乌斯条件。
/// 时间复杂度：O(log² n)，空间复杂度：O(1)
/// </summary>
public class QuadraticFrobeniusPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        // Quick trial division up to 100
        for (long p = 5; p <= 100; p += (p % 6 == 5) ? 2L : 4L)
        {
            if (n == p) return true;
            if (n % p == 0) return false;
        }

        // Find parameter c (with b=1 fixed) such that Jacobi(1 + 4c, n) = -1
        long c = FindC(n);
        if (c < 0) return false; // Jacobi == 0 means n has a factor

        // f(x) = x^2 - x - c  (b=1)
        // Frobenius condition: x^n ≡ b - x = 1 - x (mod f, n)
        // In (r0, r1) representation: (1, n-1)
        (long r0, long r1) = PolyPow(0L, 1L, (Int128)n, 1L, c, n);
        if (r0 != 1 || r1 != n - 1) return false;

        // Norm condition (follows from Frobenius but verify for extra confidence):
        // x^(n+1) should equal -c mod n
        long expectedNorm = (n - c % n) % n;
        (long s0, long s1) = PolyPow(0L, 1L, (Int128)(n + 1), 1L, c, n);
        if (s0 != expectedNorm || s1 != 0) return false;

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

    /// <summary>
    /// Finds smallest c >= 1 such that Jacobi(1 + 4c, n) == -1.
    /// Returns -1 if Jacobi == 0 (n is composite with a small factor).
    /// </summary>
    private static long FindC(long n)
    {
        for (long c = 1; ; c++)
        {
            long disc = 1 + 4 * c;
            if (disc >= n) disc %= n;
            int j = Jacobi(disc, n);
            if (j == -1) return c;
            if (j == 0) return -1; // gcd(disc, n) > 1 → n is composite
        }
    }

    /// <summary>Computes the Jacobi symbol (a/n).</summary>
    private static int Jacobi(long a, long n)
    {
        if (n <= 0 || n % 2 == 0) return 0;
        a %= n;
        if (a < 0) a += n;
        int result = 1;
        while (a != 0)
        {
            while (a % 2 == 0)
            {
                a /= 2;
                long nmod8 = n % 8;
                if (nmod8 == 3 || nmod8 == 5) result = -result;
            }
            (a, n) = (n, a);
            if (a % 4 == 3 && n % 4 == 3) result = -result;
            a %= n;
        }
        return n == 1 ? result : 0;
    }

    /// <summary>
    /// Multiplies two elements in Z_n[x]/(x^2 - b*x - c).
    /// Elements represented as (r0, r1) = r0 + r1*x.
    /// x^2 = b*x + c, so:
    ///   real part = r0*s0 + r1*s1*c (mod n)
    ///   x part   = r0*s1 + r1*s0 + r1*s1*b (mod n)
    /// </summary>
    private static (long r0, long r1) PolyMul(
        long r0, long r1, long s0, long s1, long b, long c, long n)
    {
        long r1s1 = (long)((Int128)r1 * s1 % n);
        long new_r0 = (long)(((Int128)r0 * s0 % n + (Int128)r1s1 * c % n) % n);
        long new_r1 = (long)(((Int128)r0 * s1 % n + (Int128)r1 * s0 % n + (Int128)r1s1 * b % n) % n);
        if (new_r0 < 0) new_r0 += n;
        if (new_r1 < 0) new_r1 += n;
        return (new_r0, new_r1);
    }

    /// <summary>Computes (r0 + r1*x)^exp in Z_n[x]/(x^2 - b*x - c) by repeated squaring.</summary>
    private static (long r0, long r1) PolyPow(
        long r0, long r1, Int128 exp, long b, long c, long n)
    {
        long acc0 = 1, acc1 = 0; // identity = 1
        while (exp > 0)
        {
            if ((exp & 1) == 1)
                (acc0, acc1) = PolyMul(acc0, acc1, r0, r1, b, c, n);
            (r0, r1) = PolyMul(r0, r1, r0, r1, b, c, n);
            exp >>= 1;
        }
        return (acc0, acc1);
    }
}
