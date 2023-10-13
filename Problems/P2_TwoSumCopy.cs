using Xunit;

[Problem(2,"Add Two Numbers", "https://leetcode.com/problems/add-two-numbers/")]
public class P2_AddTwoNumbers : ProblemBase
{
    [Theory]
    [InlineData(new int[]{9}, new int[]{1,9,9,9,9,9,9,9,9,9}, new int[]{0,0,0,0,0,0,0,0,0,0,1})]
    [InlineData(new int[]{2,4,3}, new int[]{5,6,4}, new int[]{7,0,8})]
    [InlineData(new int[]{0}, new int[]{0}, new int[]{0})]
    [InlineData(new int[]{9,9,9,9,9,9,9}, new int[]{9,9,9,9}, new int[]{8,9,9,9,0,0,0,1})]
    public void Test(int[] l1, int[] l2, int[] expected)
    {
        // Arrange
        ListNode n1 = ListNode.FromSequence(l1);
        ListNode n2 = ListNode.FromSequence(l2);
        ListNode nE = ListNode.FromSequence(expected);

        // Act
        ListNode? result = AddTwoNumbers(n1, n2);

        // Assert;
        Assert.NotNull(result);
        Assert.Equal(nE.val, result.val);

        ListNode? currentExpected = nE;
        ListNode? currentResult = result;
        while(currentExpected?.next != null || currentResult?.next != null)
        {
            Assert.NotNull(currentExpected);
            Assert.NotNull(currentResult);
            Assert.Equal(currentExpected.val, currentResult.val);
            currentExpected = currentExpected.next;
            currentResult = currentResult.next;
        }
    }

    private ListNode? AddTwoNumbers(ListNode l1, ListNode l2) 
    {
        int[] values1 = GetValues(l1).ToArray();
        int[] values2 = GetValues(l2).ToArray();
        Console.WriteLine($"l1: {string.Join("", values1)}");
        Console.WriteLine($"l2: {string.Join("", values2)}");

        int[] largest = values1.Length >= values2.Length ? values1 : values2;
        int[] smallest = values1.Length < values2.Length ? values1 : values2;
        Stack<int> resultStack = new();
        int remainer = 0;
        for(int i=largest.Length-1; i>=0; i--)
        {
            int altIndex = i - (largest.Length - smallest.Length); // 9 - (10 - 1) = 0;
            int sum = largest[i] + (altIndex >= 0 ? smallest[altIndex] : 0) + remainer;
            if (sum > 9)
            {
                resultStack.Push(sum - 10);
                remainer = 1;
            }
            else
            {
                resultStack.Push(sum);
                remainer = 0;
            }
        }

        if (remainer > 0)
        {
            resultStack.Push(remainer);
        }

        int[] result = ReadResult().ToArray();
        Console.WriteLine($"result: {string.Join("", result)}");
        ListNode resultNode = GetNodeFromSequence(result.Reverse());

        // Local functions
        IEnumerable<int> ReadResult()
        {
            while (resultStack.TryPop(out int result))
            {
                yield return result;
            }
        };

        return resultNode;
    }

    private ListNode GetNodeFromSequence(IEnumerable<int> values)
    {
        if (!values.Any())
        {
            throw new InvalidOperationException("Cannot construct ListNode from empty sequence!");
        }

        var startNode = new ListNode(values.First());
        var currentNode = startNode;
        foreach(var value in values.Skip(1))
        {
            var node = new ListNode(value);
            currentNode.next = node;
            currentNode = node;
        }

        return startNode;
    }

    private IEnumerable<int> GetValues(ListNode node)
    {
        ListNode currentNode = node;
        var stack = new Stack<int>();
        stack.Push(currentNode.val);
        while(currentNode.next != null)
        {
            currentNode = currentNode.next;
            stack.Push(currentNode.val);
        }

        while(stack.TryPop(out int digit))
        {
            yield return digit;
        }
    }
}

public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }

    public static ListNode FromSequence(params int[] values)
    {
        if (values.Length <= 0)
        {
            throw new InvalidOperationException("Cannot construct ListNode from empty sequence!");
        }

        var startNode = new ListNode(values[0]);
        var currentNode = startNode;
        foreach(var value in values.Skip(1))
        {
            var node = new ListNode(value);
            currentNode.next = node;
            currentNode = node;
        }

        return startNode;
    }
}