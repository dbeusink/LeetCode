namespace LeetCode;

internal class ProblemHandler
{
    private readonly Lazy<IProblem> _problem;

    public int ProblemId { get; }
    public string ProblemName { get; }
    public IProblem Problem => _problem.Value;

    public ProblemHandler(int problemId, string problemName, Type type)
    {
        (ProblemId, ProblemName) = (problemId, problemName);
        _problem = new Lazy<IProblem>(() => CreateProblemInstance(type), true);
    }

    private static IProblem CreateProblemInstance(Type type)
    {
        if (Activator.CreateInstance(type) is IProblem p)
        {
            return p;
        }

        throw new InvalidOperationException($"Could not cast type '{type}' to 'IProblem'");
    }
}