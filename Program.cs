
ProblemManager manager = new(false);
if (args.Length == 1 && int.TryParse(args[0], out int id))
{
    manager.SolveProblem(id);
}
else
{
    manager.SolveAllProblems();
}