using PrimeAlgorithm.Contracts;
using SolovayStrassen;
using Xunit;

namespace SolovayStrassen.Tests;

public class SolovayStrassenTests
{
    private readonly IPrimeChecker _checker = new SolovayStrassenPrimeChecker();

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
