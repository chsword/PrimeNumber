using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using PrimeAlgorithm.Contracts;
using SieveOfEratosthenes;
using TrialDivision;

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
