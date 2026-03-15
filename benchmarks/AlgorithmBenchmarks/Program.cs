using AKS;
using BailliePSW;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BitwiseSieve;
using BitwiseSegmentedSieve;
using EnhancedWheelSieve;
using EulerLinearSegmentedSieve;
using Fermat;
using LinearSieve;
using MillerRabin;
using ParallelSegmentedSieve;
using PrimeAlgorithm.Contracts;
using QuadraticFrobenius;
using SegmentedSieve;
using SieveOfAtkin;
using SieveOfEratosthenes;
using SieveOfPritchard;
using SieveOfSundaram;
using SieveOfZakiya;
using SolovayStrassen;
using TrialDivision;
using Wheel30SegmentedSieve;
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
        new ParallelSegmentedSievePrimeChecker(),
        new BitwiseSegmentedSievePrimeChecker(),
        new SieveOfZakiyaPrimeChecker(),
        new Wheel30SegmentedSievePrimeChecker(),
        new EulerLinearSegmentedSievePrimeChecker(),
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

    [Benchmark]
    [BenchmarkCategory("ParallelSegmentedSieve")]
    public int ParallelSegmentedSieve_GetPrimesUpTo()
        => Checkers[16].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("BitwiseSegmentedSieve")]
    public int BitwiseSegmentedSieve_GetPrimesUpTo()
        => Checkers[17].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("SieveOfZakiya")]
    public int SieveOfZakiya_GetPrimesUpTo()
        => Checkers[18].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("Wheel30SegmentedSieve")]
    public int Wheel30SegmentedSieve_GetPrimesUpTo()
        => Checkers[19].GetPrimesUpTo(Max).Count();

    [Benchmark]
    [BenchmarkCategory("EulerLinearSegmentedSieve")]
    public int EulerLinearSegmentedSieve_GetPrimesUpTo()
        => Checkers[20].GetPrimesUpTo(Max).Count();
}

/// <summary>
/// Simple timing runner used by CI to compare algorithm performance.
/// Outputs one line per algorithm: "AlgorithmName\tElapsedMs"
/// </summary>
internal static class SimpleRunner
{
    private const long BenchMax = 1_000_000;

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
        new ParallelSegmentedSievePrimeChecker(),
        new BitwiseSegmentedSievePrimeChecker(),
        new SieveOfZakiyaPrimeChecker(),
        new Wheel30SegmentedSievePrimeChecker(),
        new EulerLinearSegmentedSievePrimeChecker(),
    ];

    internal static void Run()
    {
        Console.WriteLine("# PrimeAlgorithm Performance Results");
        Console.WriteLine($"# Benchmark: GetPrimesUpTo({BenchMax:N0})");
        Console.WriteLine("# Format: AlgorithmName<TAB>ElapsedMs");
        Console.WriteLine();

        var results = new List<(string Name, double ElapsedMs)>();
        // Tracks the slowest completed time; timeout = max(2× slowest, MinTimeoutMs).
        // The floor ensures that typical algorithms (up to a few hundred ms) always finish,
        // while truly extreme outliers (e.g. algorithms taking minutes) are skipped.
        const double MinTimeoutMs = 3000;
        double worstMs = 0;

        foreach (var checker in Checkers)
        {
            // Timeout is 2× the current slowest, but never below MinTimeoutMs.
            double timeoutMs = Math.Max(worstMs * 2.0, MinTimeoutMs);

            var sw = System.Diagnostics.Stopwatch.StartNew();
            // Run on a background task so we can enforce the timeout.
            // Note: IPrimeChecker does not accept CancellationToken, so a timed-out task
            // continues executing in the background but holds no shared mutable state and
            // will be reclaimed when the process exits at the end of the benchmark run.
            var task = Task.Run(() => _ = checker.GetPrimesUpTo(BenchMax).Count());
            bool timedOut = !task.Wait(TimeSpan.FromMilliseconds(timeoutMs));
            sw.Stop();
            double elapsed = sw.Elapsed.TotalMilliseconds;

            if (timedOut)
            {
                Console.WriteLine($"{checker.AlgorithmName}\t超时/Timeout (>{timeoutMs:F0}ms)");
            }
            else
            {
                if (elapsed > worstMs) worstMs = elapsed;
                results.Add((checker.AlgorithmName, elapsed));
                Console.WriteLine($"{checker.AlgorithmName}\t{elapsed:F2}");
            }
        }

        // Rank by elapsed time (ascending = faster is better)
        results.Sort((a, b) => a.ElapsedMs.CompareTo(b.ElapsedMs));
        Console.WriteLine();
        Console.WriteLine("# Ranking (fastest first):");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"#{i + 1}\t{results[i].Name}\t{results[i].ElapsedMs:F2}ms");
        }
    }
}
