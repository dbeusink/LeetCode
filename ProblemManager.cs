using System.Reflection;

namespace LeetCode;

internal class ProblemManager
{
    private readonly Dictionary<int, ProblemHandler> _leetCodeProblemHandlers = [];
    private readonly Dictionary<string, ProblemHandler> _genericProblemHandlers = [];

    public ProblemManager()
    {
        RegisterProblems();
    }

    #region LeetCode problems

    public void SolveAllLeetCodeProblems()
    {
        foreach (var problemId in _leetCodeProblemHandlers.Keys)
        {
            SolveLeetCodeProblem(problemId);
        }
    }

    public void SolveLeetCodeProblem(int leetCodeId)
    {
        if (_leetCodeProblemHandlers.TryGetValue(leetCodeId, out var problemHandler))
        {
            Console.WriteLine($"Solving LeetCode problem \u001b[1;92m'{problemHandler.ProblemId}. {problemHandler.ProblemName}'\u001b[0m");
            TrySolveProblem(problemHandler);
        }
        else
        {
            Console.WriteLine($"LeetCode problem \u001b[1;92m'{leetCodeId}.'\u001b[0m: \u001b[1;93m not found!\u001b[0m");
        }

    }

    #endregion

    #region Generic problems

    public void SolveAllGenericProblems()
    {
        foreach (var problemName in _genericProblemHandlers.Keys)
        {
            SolveGenericProblem(problemName);
        }
    }

    public void SolveGenericProblem(string problemName)
    {
        if (_genericProblemHandlers.TryGetValue(problemName, out var problemHandler))
        {
            Console.WriteLine($"Solving generic problem \u001b[1;92m'{problemHandler.ProblemName}'\u001b[0m");
            TrySolveProblem(problemHandler);
        }
        else
        {
            Console.WriteLine($"Generic problem \u001b[1;92m'{problemName}.'\u001b[0m: \u001b[1;93m not found!\u001b[0m");
        }
    }

    #endregion

    private static void TrySolveProblem(ProblemHandler handler)
    {
        try
        {
            handler.Problem.Solve();
            Console.WriteLine($"Finished solving problem \u001b[1;92m'{handler.ProblemName}'\u001b[0m");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Problem \u001b[1;92m'{handler.ProblemName}'\u001b[0m: \u001b[1;91mException! {ex.Message}\u001b[0m");
        }
    }

    private void RegisterProblems()
    {
        // Auto-discover problems using reflection
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach(var problemType in assembly.GetTypes().Where(x => Attribute.IsDefined(x, typeof(Problem))))
        {
            if (Attribute.GetCustomAttribute(problemType, typeof(Problem)) is Problem p)
            {
                if (p.IsLeetCodeProblem)
                {
                    _leetCodeProblemHandlers.Add(p.ProblemId, new ProblemHandler(p.ProblemId, p.Name, problemType));
                }
                else
                {
                    _genericProblemHandlers.Add(p.Name, new ProblemHandler(-1, p.Name, problemType));
                }
            }
        }

        Console.WriteLine("LeetCode problems discovered:");
        foreach (var chunk in _leetCodeProblemHandlers.Values.Select(x => $"[{x.ProblemId}] {x.ProblemName}").Chunk(5))
        {
            Console.WriteLine($"\u001b[1;96m{string.Join('\t', chunk)}\u001b[0m");
        }

        Console.WriteLine("Generic problems discovered:");
        foreach (var chunk in _genericProblemHandlers.Keys.Chunk(5))
        {
            Console.WriteLine($"\u001b[1;96m{string.Join('\t', chunk)}\u001b[0m");
        }
    }
}