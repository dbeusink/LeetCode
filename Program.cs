
using LeetCode;

ProblemManager manager = new();
if (args.Length == 1)
{
    switch(args[0])
    {
        case "l":
            manager.SolveAllLeetCodeProblems();
            break;
        case "g":
            manager.SolveAllGenericProblems();
            break;
        default:
            if (int.TryParse(args[0], out int id))
            {
                manager.SolveLeetCodeProblem(id);
            }
            else
            {
                manager.SolveGenericProblem(args[0]);
            }
            break;
    }
}
else
{
    manager.SolveAllLeetCodeProblems();
    manager.SolveAllGenericProblems();
}