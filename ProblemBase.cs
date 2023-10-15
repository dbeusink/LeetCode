using System.Reflection;
using Xunit.Runners;

public abstract class ProblemBase : IProblem
{
    private readonly object _consoleLock = new();
    private readonly ManualResetEvent _finished = new(false);

    public void Solve()
    {
        using var runner = AssemblyRunner.WithoutAppDomain(Assembly.GetExecutingAssembly().Location);

        runner.OnDiscoveryComplete = OnDiscoveryComplete;
        runner.OnExecutionComplete = OnExecutionComplete;
        runner.OnTestFailed = OnTestFailed;
        runner.OnTestSkipped = OnTestSkipped;
        runner.OnTestPassed = OnTestPassed;

        runner.Start(this.GetType().FullName);

        _finished.WaitOne();
        _finished.Dispose();
    }
    void OnDiscoveryComplete(DiscoveryCompleteInfo info)
    {
        lock (_consoleLock)
        {
            Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }
    }

    void OnExecutionComplete(ExecutionCompleteInfo info)
    {
        lock (_consoleLock)
        {
            if (info.TestsFailed == 0 && info.TestsSkipped == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
            }
            Console.ResetColor();

        }

        _finished.Set();
    }

    void OnTestFailed(TestFailedInfo info)
    {
        lock (_consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage);
            Console.ResetColor();
        }
    }

    void OnTestSkipped(TestSkippedInfo info)
    {
        lock (_consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
            Console.ResetColor();
        }
    }

    void OnTestPassed(TestPassedInfo info)
    {
        lock (_consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[PASS] {info.TestDisplayName} in {Math.Round(info.ExecutionTime, 3)}s");
            Console.ResetColor();
        }
    }
}