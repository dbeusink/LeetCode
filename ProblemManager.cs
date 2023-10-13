using System.Diagnostics;
using System.Reflection;

internal class ProblemManager
{
    private readonly Dictionary<int, ProblemHandler> _problemHandlers = new();

    public ProblemManager(bool includeNonLeetCode = false)
    {
        RegisterProblems(includeNonLeetCode);
    }

    public void SolveAllProblems()
    {
        foreach (var problemId in _problemHandlers.Keys)
        {
            SolveProblem(problemId);
        }
    }

    public void SolveProblem(int problemId)
    {
        if (_problemHandlers.TryGetValue(problemId, out var problemHandler))
        {
            Console.WriteLine($"Problem \u001b[1;92m'{problemId}. {problemHandler.ProblemName}'\u001b[0m: Solving problem...");
            try
            {
                problemHandler.Problem.Solve();
                Console.WriteLine($"Problem \u001b[1;92m'{problemId}. {problemHandler.ProblemName}'\u001b[0m: Ended.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Problem \u001b[1;92m'{problemId}. {problemHandler.ProblemName}'\u001b[0m: \u001b[1;91mException! {ex.Message}\u001b[0m");
            }
        }
        else
        {
            Console.WriteLine($"Problem \u001b[1;92m'{problemId}.'\u001b[0m: \u001b[1;93m not found!\u001b[0m");
        }
    }

    private void RegisterProblems(bool includeNonLeetCode)
    {
        // Auto-discover puzzles using reflection
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach(var problemType in assembly.GetTypes().Where(x => Attribute.IsDefined(x, typeof(Problem))))
        {
            if (Attribute.GetCustomAttribute(problemType, typeof(Problem)) is Problem p &&
                (includeNonLeetCode || p.IsLeetCodeProblem))
            {
                _problemHandlers.Add(p.ProblemId, new ProblemHandler(p.ProblemId, p.Name, p.IsLeetCodeProblem, problemType));
            }
        }

        Console.WriteLine("Problems discovered:");
        foreach (var chunk in _problemHandlers.Values.Select(x => $"[{x.ProblemId}] {x.ProblemName}").Chunk(5))
        {
            Console.WriteLine($"\u001b[1;96m{string.Join('\t', chunk)}\u001b[0m");
        }
    }
}