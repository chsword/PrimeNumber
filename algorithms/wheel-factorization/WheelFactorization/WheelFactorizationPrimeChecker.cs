using PrimeAlgorithm.Contracts;

namespace WheelFactorization;

/// <summary>
/// 轮式因式分解法（Wheel Factorization, Wheel-30）
/// 使用模 30 的轮（跳过 2/3/5 的倍数），只检查 8 种余数：1,7,11,13,17,19,23,29。
/// 时间复杂度：O(n√n / ln n)，空间复杂度：O(1)
/// </summary>
public class WheelFactorizationPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "轮式因式分解法 (Wheel Factorization, Wheel-30)";

    // Offsets within a 30-cycle that are coprime to 2, 3, 5
    private static readonly int[] WheelOffsets = [4, 2, 4, 2, 4, 6, 2, 6];

    /// <inheritdoc />
    public bool IsPrime(long n)
    {
        if (n < 2) return false;
        if (n == 2 || n == 3 || n == 5) return true;
        if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0) return false;

        long i = 7;
        int wi = 0;
        while (i * i <= n)
        {
            if (n % i == 0) return false;
            i += WheelOffsets[wi];
            wi = (wi + 1) & 7;
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
