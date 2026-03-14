using AKS;
using BailliePSW;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BitwiseSieve;
using EnhancedWheelSieve;
using Fermat;
using LinearSieve;
using MillerRabin;
using PrimeAlgorithm.Contracts;
using QuadraticFrobenius;
using SegmentedSieve;
using SieveOfAtkin;
using SieveOfEratosthenes;
using SieveOfPritchard;
using SieveOfSundaram;
using SolovayStrassen;
using TrialDivision;
using WheelFactorization;

// Usage:
//   dotnet run -c Release                 -> full BenchmarkDotNet run
//   dotnet run -c Release -- --simple     -> quick timing run (used by CI)
if (args.Contains("--simple"))
{
    SimpleRunner.Run();
}
else
{
    BenchmarkRunner.Run<PrimeBenchmarks>(
        DefaultConfig.Instance.AddJob(Job.ShortRun));
}

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 3)]
public class PrimeBenchmarks
{
    private static readonly IPrimeChecker[] Checkers =
    [
        new SieveOfEratosthenesPrimeChecker(),
        new TrialDivisionPrimeChecker(),
        new SieveOfSundaramPrimeChecker(),
        new SieveOfAtkinPrimeChecker(),
        new MillerRabinPrimeChecker(),
        new SegmentedSievePrimeChecker(),
        new LinearSievePrimeChecker(),
        new BitwiseSievePrimeChecker(),
        new WheelFactorizationPrimeChecker(),
        new SolovayStrassenPrimeChecker(),
        new BailliePSWPrimeChecker(),
        new AKSPrimeChecker(),
        new FermatPrimeChecker(),
        new SieveOfPritchardPrimeChecker(),
        new QuadraticFrobeniusPrimeChecker(),
        new EnhancedWheelSievePrimeChecker(),
    ];

    [Params(100_000, 1_000_000)]
    public long Max { get; set; }

    [Benchmark]
    [BenchmarkCategory("SieveOfEratosthenes")]
    public int SieveOfEratosthenes_GetPrimesUpTo()
        => Checkers[0].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("TrialDivision")]
    public int TrialDivision_GetPrimesUpTo()
        => Checkers[1].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SieveOfSundaram")]
    public int SieveOfSundaram_GetPrimesUpTo()
        => Checkers[2].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SieveOfAtkin")]
    public int SieveOfAtkin_GetPrimesUpTo()
        => Checkers[3].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("MillerRabin")]
    public int MillerRabin_GetPrimesUpTo()
        => Checkers[4].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SegmentedSieve")]
    public int SegmentedSieve_GetPrimesUpTo()
        => Checkers[5].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("LinearSieve")]
    public int LinearSieve_GetPrimesUpTo()
        => Checkers[6].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("BitwiseSieve")]
    public int BitwiseSieve_GetPrimesUpTo()
        => Checkers[7].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("WheelFactorization")]
    public int WheelFactorization_GetPrimesUpTo()
        => Checkers[8].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SolovayStrassen")]
    public int SolovayStrassen_GetPrimesUpTo()
        => Checkers[9].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("BailliePSW")]
    public int BailliePSW_GetPrimesUpTo()
        => Checkers[10].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("AKS")]
    public int AKS_GetPrimesUpTo()
        => Checkers[11].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("Fermat")]
    public int Fermat_GetPrimesUpTo()
        => Checkers[12].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SieveOfPritchard")]
    public int SieveOfPritchard_GetPrimesUpTo()
        => Checkers[13].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("QuadraticFrobenius")]
    public int QuadraticFrobenius_GetPrimesUpTo()
        => Checkers[14].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("EnhancedWheelSieve")]
    public int EnhancedWheelSieve_GetPrimesUpTo()
        => Checkers[15].GetPrimesUpTo(Max).Count();
}

/// <summary>
/// Simple timing runner used by CI to compare algorithm performance.
/// Outputs one line per algorithm: "AlgorithmName\tElapsedMs"
/// </summary>
internal static class SimpleRunner
{
    private const long BenchMax = 1_000_000;
    private const int Warmup = 1;
    private const int Iterations = 3;

    private static readonly IPrimeChecker[] Checkers =
    [
        new SieveOfEratosthenesPrimeChecker(),
        new TrialDivisionPrimeChecker(),
        new SieveOfSundaramPrimeChecker(),
        new SieveOfAtkinPrimeChecker(),
        new MillerRabinPrimeChecker(),
        new SegmentedSievePrimeChecker(),
        new LinearSievePrimeChecker(),
        new BitwiseSievePrimeChecker(),
        new WheelFactorizationPrimeChecker(),
        new SolovayStrassenPrimeChecker(),
        new BailliePSWPrimeChecker(),
        new AKSPrimeChecker(),
        new FermatPrimeChecker(),
        new SieveOfPritchardPrimeChecker(),
        new QuadraticFrobeniusPrimeChecker(),
        new EnhancedWheelSievePrimeChecker(),
    ];

    internal static void Run()
    {
        Console.WriteLine("# PrimeAlgorithm Performance Results");
        Console.WriteLine($"# Benchmark: GetPrimesUpTo({BenchMax:N0})");
        Console.WriteLine("# Format: AlgorithmName<TAB>MedianMs");
        Console.WriteLine();

        var results = new List<(string Name, double MedianMs)>();

        foreach (var checker in Checkers)
        {
            // warmup
            for (int i = 0; i < Warmup; i++)
                _ = checker.GetPrimesUpTo(BenchMax).Count();

            var timings = new double[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                _ = checker.GetPrimesUpTo(BenchMax).Count();
                sw.Stop();
                timings[i] = sw.Elapsed.TotalMilliseconds;
            }

            Array.Sort(timings);
            var median = timings[Iterations / 2];
            results.Add((checker.AlgorithmName, median));
            Console.WriteLine($"{checker.AlgorithmName}\t{median:F2}");
        }

        // Rank by median time (ascending = faster is better)
        results.Sort((a, b) => a.MedianMs.CompareTo(b.MedianMs));
        Console.WriteLine();
        Console.WriteLine("# Ranking (fastest first):");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"#{i + 1}\t{results[i].Name}\t{results[i].MedianMs:F2}ms");
        }
    }
}
