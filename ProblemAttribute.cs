[AttributeUsage(AttributeTargets.Class)]
public class Problem : Attribute
{
    public bool IsLeetCodeProblem { get; }
    public int ProblemId { get; }
    public string Name { get; }
    public string Url { get; } = string.Empty;

    public Problem(int problemId, string name, string url)
    {
        (ProblemId, Name, Url) = (problemId, name, url);
        IsLeetCodeProblem = true;
    }

    public Problem(string name)
    {
        Name = name;
        Url = string.Empty;
        IsLeetCodeProblem = false;
    }
}