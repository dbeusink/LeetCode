[AttributeUsage(AttributeTargets.Class)]
public class Problem : Attribute
{
    public int ProblemId { get; }
    public string Name { get; }
    public string Url { get; }

    public Problem(int problemId, string name, string url)
    {
        (ProblemId, Name, Url) = (problemId, name, url);
    }
}