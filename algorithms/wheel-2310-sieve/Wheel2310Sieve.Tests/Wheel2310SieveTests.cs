using PrimeAlgorithm.Contracts;
using Wheel2310Sieve;
using Xunit;

namespace Wheel2310Sieve.Tests;

public class Wheel2310SieveTests
{
    private readonly IPrimeChecker _checker = new Wheel2310SievePrimeChecker();

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    [InlineData(7, true)]
    [InlineData(8, false)]
    [InlineData(9, false)]
    [InlineData(10, false)]
    [InlineData(11, true)]
    [InlineData(12, false)]
    [InlineData(13, true)]
    [InlineData(25, false)]
    [InlineData(49, false)]
    [InlineData(97, true)]
    [InlineData(100, false)]
    [InlineData(121, false)]
    [InlineData(169, false)]  // 13² — coprime to 2310 but composite
    [InlineData(209, false)]  // 11×19 — coprime to 210 but composite
    [InlineData(2309, true)]  // 2309 is prime and coprime to 2310
    [InlineData(2311, true)]  // first prime in block 1
    [InlineData(999983, true)]
    [InlineData(1000000, false)]
    public void IsPrime_ReturnsCorrectResult(long n, bool expected)
    {
        Assert.Equal(expected, _checker.IsPrime(n));
    }

    [Theory]
    [InlineData(4)]
    [InlineData(9)]
    [InlineData(25)]
    [InlineData(49)]
    [InlineData(121)]
    [InlineData(169)]
    [InlineData(1000000)]
    public void IsPrime_PerfectSquare_ReturnsFalse(long n)
    {
        Assert.False(_checker.IsPrime(n));
    }

    [Theory]
    [InlineData(104729)]
    [InlineData(15485863)]
    [InlineData(1000000007)]
    [InlineData(998244353)]
    public void IsPrime_LargePrimes_ReturnsTrue(long n)
    {
        Assert.True(_checker.IsPrime(n));
    }

    [Fact]
    public void GetPrimesUpTo_0_ReturnsEmpty()
    {
        Assert.Empty(_checker.GetPrimesUpTo(0).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_1_ReturnsEmpty()
    {
        Assert.Empty(_checker.GetPrimesUpTo(1).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_2_ReturnsSinglePrime()
    {
        Assert.Equal([2L], _checker.GetPrimesUpTo(2).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_10_ReturnsCorrectPrimes()
    {
        Assert.Equal([2L, 3L, 5L, 7L], _checker.GetPrimesUpTo(10).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_11_IncludesEleven()
    {
        Assert.Equal([2L, 3L, 5L, 7L, 11L], _checker.GetPrimesUpTo(11).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_13_IncludesThirteen()
    {
        Assert.Equal([2L, 3L, 5L, 7L, 11L, 13L], _checker.GetPrimesUpTo(13).ToList());
    }

    [Fact]
    public void GetPrimesUpTo_100_Returns25Primes()
    {
        Assert.Equal(25, _checker.GetPrimesUpTo(100).Count());
    }

    [Fact]
    public void GetPrimesUpTo_1000_Returns168Primes()
    {
        Assert.Equal(168, _checker.GetPrimesUpTo(1000).Count());
    }

    [Fact]
    public void GetPrimesUpTo_1000000_Returns78498Primes()
    {
        Assert.Equal(78498, _checker.GetPrimesUpTo(1_000_000).Count());
    }

    [Fact]
    public void GetPrimesUpTo_ResultsAreAscending()
    {
        var primes = _checker.GetPrimesUpTo(1000).ToList();
        for (int i = 1; i < primes.Count; i++)
            Assert.True(primes[i] > primes[i - 1]);
    }

    [Fact]
    public void GetPrimesUpTo_MatchesSieveOfEratosthenes()
    {
        // Cross-check against the 25 known primes up to 100
        long[] expected = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47,
                           53, 59, 61, 67, 71, 73, 79, 83, 89, 97];
        Assert.Equal(expected, _checker.GetPrimesUpTo(100).ToArray());
    }

    [Fact]
    public void AlgorithmName_IsNotEmpty()
    {
        Assert.False(string.IsNullOrWhiteSpace(_checker.AlgorithmName));
    }
}
