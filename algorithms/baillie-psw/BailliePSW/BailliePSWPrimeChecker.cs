using PrimeAlgorithm.Contracts;

namespace BailliePSW;

/// <summary>
/// Baillie-PSW 素性测试（Baillie-PSW Primality Test）
/// 结合 Miller-Rabin 基-2 强伪素数测试与强 Lucas 伪素数测试（Selfridge 方法 A）。
/// 对 long 范围内所有数均无已知假阳性。
/// 时间复杂度（单次检测）：O(log² n)，空间复杂度：O(1)
/// </summary>
public class BailliePSWPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "Baillie-PSW 素性测试";

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5 || n == 7) return true;
        if ((n & 1) == 0 || n % 3 == 0 || n % 5 == 0 || n % 7 == 0) return false;

        // Phase 1: Strong Miller-Rabin test with base 2
        if (!MillerRabinBase2(n)) return false;

        // Phase 2: Strong Lucas probable prime test (Selfridge method A)
        return StrongLucasProbablePrime(n);
    }

    /// <inheritdoc />
    public IEnumerable<long> GetPrimesUpTo(long max)
    {
        for (long i = 2; i <= max; i++)
        {
            if (IsPrime(i)) yield return i;
        }
    }

    /// <summary>Strong Miller-Rabin primality test using base 2.</summary>
    private static bool MillerRabinBase2(long n)
    {
        // Write n-1 = 2^r * d (d odd)
        long d = n - 1;
        int r = 0;
        while ((d & 1) == 0) { d >>= 1; r++; }

        long x = ModPow(2L, d, n);
        if (x == 1 || x == n - 1) return true;
        for (int i = 0; i < r - 1; i++)
        {
            x = ModMul(x, x, n);
            if (x == n - 1) return true;
        }
        return false;
    }

    /// <summary>
    /// Strong Lucas probable prime test using Selfridge's method A:
    /// find the first D in 5, -7, 9, -11, 13, ... with Jacobi(D, n) = -1,
    /// then set P = 1 and Q = (1 - D) / 4.
    /// </summary>
    private static bool StrongLucasProbablePrime(long n)
    {
        // Guard: perfect squares never satisfy Jacobi(D,n)=-1 for any D.
        long sqrtN = (long)Math.Sqrt((double)n);
        if (sqrtN * sqrtN == n) return false;

        // Find D using Selfridge's method A
        long D = FindD(n, out bool isComposite);
        if (isComposite) return false;

        long P = 1;
        long Q = (1 - D) / 4;

        // Write n+1 = 2^s * d (d odd)
        long delta = n + 1;
        int s = 0;
        long d = delta;
        while ((d & 1) == 0) { d >>= 1; s++; }

        // Compute (U_d, V_d) mod n
        var (u, v) = ComputeLucasUV(d, P, Q, D, n);

        // U_d ≡ 0 (mod n) is sufficient
        if (u == 0) return true;

        // Check V_{2^j * d} ≡ 0 (mod n) for j = 0, 1, ..., s-1
        long Qmod = ((Q % n) + n) % n;
        long qk = ModPow(Qmod, d, n);

        for (int j = 0; j < s; j++)
        {
            if (v == 0) return true;
            // V_{2k} = V_k^2 - 2 * Q^k  (mod n)
            v = (long)(((Int128)ModMul(v, v, n) + n - ModMul(2L, qk, n)) % n);
            qk = ModMul(qk, qk, n);
        }

        return false;
    }

    /// <summary>
    /// Find the first D in the sequence 5, -7, 9, -11, ... with Jacobi(D, n) = -1.
    /// Sets <paramref name="isComposite"/> to true when a non-trivial proper factor of n is detected.
    /// </summary>
    private static long FindD(long n, out bool isComposite)
    {
        long absD = 5;
        int sign = 1;

        for (int attempt = 0; attempt < 40; attempt++)
        {
            long Dval = absD * sign;
            long j = Jacobi(Dval, n);

            if (j == -1)
            {
                isComposite = false;
                return Dval;
            }

            if (j == 0)
            {
                // Jacobi(D, n) = 0 iff gcd(|D|, n) > 1.
                // If gcd is a proper factor (1 < g < n), n is composite.
                // If gcd = n (n divides |D|), skip this D and continue searching.
                long g = GCD(absD, n);
                if (g > 1 && g < n)
                {
                    isComposite = true;
                    return Dval;
                }
                // g == n: |D| is a multiple of n — skip this candidate.
            }

            absD += 2;
            sign = -sign;
        }

        // Fallback (should not occur for reasonable inputs)
        isComposite = false;
        return 5;
    }

    private static long GCD(long a, long b)
    {
        while (b != 0) { (a, b) = (b, a % b); }
        return a;
    }

    /// <summary>
    /// Compute (U_k, V_k) mod n for the Lucas sequence with parameters P, Q, D
    /// using the binary (double-and-add) method.
    /// </summary>
    private static (long u, long v) ComputeLucasUV(long k, long P, long Q, long D, long n)
    {
        if (k == 0) return (0, 2 % n);

        long Pmod = ((P % n) + n) % n;
        long Dmod = ((D % n) + n) % n;
        long Qmod = ((Q % n) + n) % n;

        long u = 1;
        long v = Pmod;
        long qk = Qmod;

        // Find most-significant bit position of k
        int msb = 63;
        while (msb > 0 && ((k >> msb) & 1) == 0) msb--;

        // Process bits from (msb-1) down to 0
        for (int i = msb - 1; i >= 0; i--)
        {
            // Double: (U_{2m}, V_{2m}, Q^{2m})
            long u2 = ModMul(u, v, n);
            long v2 = (long)(((Int128)ModMul(v, v, n) + n - ModMul(2L, qk, n)) % n);
            qk = ModMul(qk, qk, n);

            if (((k >> i) & 1) == 1)
            {
                // Add 1: U_{2m+1} = (P*U_{2m} + V_{2m}) / 2 mod n
                //         V_{2m+1} = (D*U_{2m} + P*V_{2m}) / 2 mod n
                long newU = (long)(((Int128)ModMul(Pmod, u2, n) + v2) % n);
                long newV = (long)(((Int128)ModMul(Dmod, u2, n) + ModMul(Pmod, v2, n)) % n);
                qk = ModMul(qk, Qmod, n);

                // Divide by 2 mod n (n is odd: if odd, add n to make even, then shift)
                if ((newU & 1) != 0) newU += n;
                newU >>= 1;
                if ((newV & 1) != 0) newV += n;
                newV >>= 1;

                u = newU;
                v = newV;
            }
            else
            {
                u = u2;
                v = v2;
            }
        }

        return (u, v);
    }

    /// <summary>Jacobi symbol (a | n). Returns -1, 0, or 1.</summary>
    private static long Jacobi(long a, long n)
    {
        if (n <= 0 || (n & 1) == 0) return 0;
        a = ((a % n) + n) % n;
        long result = 1;
        while (a != 0)
        {
            while ((a & 1) == 0)
            {
                a >>= 1;
                long r = n % 8;
                if (r == 3 || r == 5) result = -result;
            }
            (a, n) = (n, a);
            if (a % 4 == 3 && n % 4 == 3) result = -result;
            a %= n;
        }
        return n == 1 ? result : 0;
    }

    /// <summary>Computes (base^exp) % mod using fast binary exponentiation.</summary>
    private static long ModPow(long baseVal, long exp, long mod)
    {
        long result = 1;
        baseVal %= mod;
        if (baseVal < 0) baseVal += mod;
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
