namespace PrimeAlgorithm.Contracts;

/// <summary>
/// 质数判断算法的统一接口，所有参赛算法必须实现此接口。
/// </summary>
public interface IPrimeChecker
{
    /// <summary>
    /// 算法名称（中文）
    /// </summary>
    string AlgorithmName { get; }

    /// <summary>
    /// 判断给定正整数是否为质数
    /// </summary>
    /// <param name="n">待判断的正整数</param>
    /// <returns>若为质数则返回 true，否则返回 false</returns>
    bool IsPrime(long n);

    /// <summary>
    /// 获取不超过 max 的所有质数（升序）
    /// </summary>
    /// <param name="max">上限值（包含）</param>
    /// <returns>不超过 max 的所有质数</returns>
    IEnumerable<long> GetPrimesUpTo(long max);
}
