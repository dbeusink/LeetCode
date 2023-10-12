internal class ProblemHandler
{
    private Lazy<IProblem> _problem;

    public int ProblemId { get; }
    public string ProblemName { get; }
    public IProblem Problem => _problem.Value;

    public ProblemHandler(int problemId, string problemName, Type type)
    {
        (ProblemId, ProblemName) = (problemId, problemName);
        _problem = new Lazy<IProblem>(() => GetProblemInstance(type), true);
    }

    private IProblem GetProblemInstance(Type type)
    {
        if (Activator.CreateInstance(type) is IProblem p)
        {
            return p;
        }

        throw new InvalidOperationException($"Could not case type {type} to 'IProblem'");
    }
}