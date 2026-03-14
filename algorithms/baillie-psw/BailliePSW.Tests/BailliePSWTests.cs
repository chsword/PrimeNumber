using PrimeAlgorithm.Contracts;
using BailliePSW;
using Xunit;

namespace BailliePSW.Tests;

public class BailliePSWTests
{
    private readonly IPrimeChecker _checker = new BailliePSWPrimeChecker();

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    [InlineData(7, true)]
    [InlineData(9, false)]
    [InlineData(11, true)]
    [InlineData(13, true)]
    [InlineData(25, false)]
    [InlineData(97, true)]
    [InlineData(100, false)]
    [InlineData(999983, true)]
    [InlineData(1000000, false)]
    public void IsPrime_ReturnsCorrectResult(long n, bool expected)
    {
        Assert.Equal(expected, _checker.IsPrime(n));
    }

    [Theory]
    [InlineData(4)]       // 2^2
    [InlineData(9)]       // 3^2
    [InlineData(25)]      // 5^2
    [InlineData(49)]      // 7^2
    [InlineData(121)]     // 11^2
    [InlineData(169)]     // 13^2
    [InlineData(1000000)] // 1000^2
    public void IsPrime_PerfectSquare_ReturnsFalse(long n)
    {
        Assert.False(_checker.IsPrime(n));
    }

    [Theory]
    [InlineData(104729)]       // known prime
    [InlineData(15485863)]     // 1,000,000th prime
    [InlineData(1000000007)]   // large prime
    [InlineData(998244353)]    // NTT-friendly prime
    public void IsPrime_LargePrimes_ReturnsTrue(long n)
    {
        Assert.True(_checker.IsPrime(n));
    }

    [Theory]
    [InlineData(561)]    // smallest Carmichael number
    [InlineData(1105)]   // second Carmichael number
    [InlineData(1729)]   // Hardy-Ramanujan number, Carmichael
    [InlineData(8911)]   // Carmichael number
    public void IsPrime_CarmichaelNumbers_ReturnsFalse(long n)
    {
        Assert.False(_checker.IsPrime(n));
    }

    [Fact]
    public void GetPrimesUpTo_10_ReturnsCorrectPrimes()
    {
        var primes = _checker.GetPrimesUpTo(10).ToList();
        Assert.Equal([2L, 3L, 5L, 7L], primes);
    }

    [Fact]
    public void GetPrimesUpTo_100_Returns25Primes()
    {
        var primes = _checker.GetPrimesUpTo(100).ToList();
        Assert.Equal(25, primes.Count);
    }

    [Fact]
    public void GetPrimesUpTo_1_ReturnsEmpty()
    {
        var primes = _checker.GetPrimesUpTo(1).ToList();
        Assert.Empty(primes);
    }

    [Fact]
    public void GetPrimesUpTo_2_ReturnsSinglePrime()
    {
        var primes = _checker.GetPrimesUpTo(2).ToList();
        Assert.Equal([2L], primes);
    }

    [Fact]
    public void AlgorithmName_IsNotEmpty()
    {
        Assert.False(string.IsNullOrWhiteSpace(_checker.AlgorithmName));
    }
}
