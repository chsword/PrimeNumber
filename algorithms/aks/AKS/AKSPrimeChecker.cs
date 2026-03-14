using PrimeAlgorithm.Contracts;

namespace AKS;

/// <summary>
/// AKS 素性测试 (Agrawal-Kayal-Saxena Primality Test)
/// 2002年由Agrawal、Kayal和Saxena提出的确定性多项式时间素性测试算法。
/// 时间复杂度：Õ(log^6 n)，空间复杂度：O(r) where r = O(log^5 n)
/// </summary>
public class AKSPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "AKS 素性测试 (Agrawal-Kayal-Saxena)";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        // Step 1: Perfect power check — if n = a^b (a>=2, b>=2), composite
        if (IsPerfectPower(n)) return false;

        // Step 2: Find smallest r such that ord_r(n) > (log2 n)^2
        double logN = Math.Log2(n);
        long maxOrd = (long)(logN * logN) + 1;
        long r = FindR(n, maxOrd);

        // Step 3: For a = 2..min(r, n-1): if 1 < gcd(a,n) < n, composite
        long aLimit = Math.Min(r, n - 1);
        for (long a = 2; a <= aLimit; a++)
        {
            long g = Gcd(a, n);
            if (g > 1 && g < n) return false;
        }

        // Step 4: If n <= r, then n is prime
        if (n <= r) return true;

        // Step 5: Polynomial congruence checks
        long phi = EulerPhi(r);
        long polyLimit = (long)(Math.Sqrt(phi) * logN);
        for (long a = 1; a <= polyLimit; a++)
        {
            if (!PolyCheck(a % n, n, r)) return false;
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

    /// <summary>Returns true if n = a^b for some integers a >= 2, b >= 2.</summary>
    private static bool IsPerfectPower(long n)
    {
        for (int b = 2; b <= 62; b++)
        {
            // 2^b > n means no valid a exists
            if (b < 63 && (1L << b) > n) break;

            // Binary search for a with a^b = n
            long lo = 2;
            long hi = (long)Math.Ceiling(Math.Pow(n, 1.0 / b)) + 1;
            while (lo <= hi)
            {
                long mid = lo + (hi - lo) / 2;
                Int128 val = Pow128(mid, b);
                if (val == n) return true;
                if (val < n) lo = mid + 1;
                else hi = mid - 1;
            }
        }
        return false;
    }

    /// <summary>Computes a^b using Int128 to avoid overflow.</summary>
    private static Int128 Pow128(long a, int b)
    {
        Int128 result = 1;
        Int128 a128 = a;
        for (int i = 0; i < b; i++)
            result *= a128;
        return result;
    }

    /// <summary>
    /// Finds smallest r >= 2 such that ord_r(n) > maxOrd (with gcd(n,r)==1).
    /// </summary>
    private static long FindR(long n, long maxOrd)
    {
        for (long r = 2; ; r++)
        {
            if (Gcd(n, r) != 1) continue;

            long nmodr = n % r;
            long cur = nmodr;
            bool ordExceedsLimit = true;
            for (long k = 1; k <= maxOrd; k++)
            {
                if (cur == 1) { ordExceedsLimit = false; break; }
                cur = (long)((Int128)cur * nmodr % r);
            }
            if (ordExceedsLimit) return r;
        }
    }

    /// <summary>Computes Euler's totient φ(n).</summary>
    private static long EulerPhi(long n)
    {
        long result = n;
        long temp = n;
        for (long p = 2; p * p <= temp; p++)
        {
            if (temp % p == 0)
            {
                while (temp % p == 0) temp /= p;
                result -= result / p;
            }
        }
        if (temp > 1) result -= result / temp;
        return result;
    }

    /// <summary>
    /// Checks whether (x + a)^n ≡ x^n + a (mod x^r - 1, n).
    /// </summary>
    private static bool PolyCheck(long a, long n, long r)
    {
        long[] lhs = PolyPow(a, n, r, n);

        // Build expected: x^(n mod r) + a
        long[] rhs = new long[r];
        long nmodr = n % r;
        rhs[nmodr] = (rhs[nmodr] + 1) % n;
        rhs[0] = (rhs[0] + a % n) % n;

        for (long i = 0; i < r; i++)
            if (lhs[i] != rhs[i]) return false;
        return true;
    }

    /// <summary>Computes (x + a)^exp mod (x^r - 1, n) by repeated squaring.</summary>
    private static long[] PolyPow(long a, long exp, long r, long mod)
    {
        long[] result = new long[r];
        result[0] = 1; // polynomial "1"

        long[] baseP = new long[r];
        baseP[0] = a % mod;
        if (r > 1)
            baseP[1] = 1; // polynomial "x + a"
        else
            baseP[0] = (baseP[0] + 1) % mod; // x ≡ 1 (mod x-1)

        while (exp > 0)
        {
            if ((exp & 1) == 1) result = PolyMul(result, baseP, r, mod);
            baseP = PolyMul(baseP, baseP, r, mod);
            exp >>= 1;
        }
        return result;
    }

    /// <summary>Multiplies two polynomials mod (x^r - 1) with coefficients mod n.</summary>
    private static long[] PolyMul(long[] a, long[] b, long r, long n)
    {
        long[] result = new long[r];
        for (long i = 0; i < r; i++)
        {
            if (a[i] == 0) continue;
            for (long j = 0; j < r; j++)
            {
                if (b[j] == 0) continue;
                long k = (i + j) % r;
                result[k] = (result[k] + (long)((Int128)a[i] * b[j] % n)) % n;
            }
        }
        return result;
    }

    private static long Gcd(long a, long b)
    {
        while (b != 0) { long t = b; b = a % b; a = t; }
        return a;
    }
}
