using PrimeAlgorithm.Contracts;

namespace TrialDivision;

/// <summary>
/// 试除法（Trial Division）
/// 对每个待判断数逐一用小于其平方根的数尝试整除，若无法整除则为质数。
/// 时间复杂度：O(n√n)，空间复杂度：O(1)
/// </summary>
public class TrialDivisionPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "试除法 (Trial Division)";

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
        for (long i = 2; i <= max; i++)
        {
            if (IsPrime(i)) yield return i;
        }
    }
}
