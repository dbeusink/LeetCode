
namespace LeetCode;

[Problem("CanSum")]
public class P0_CanSum : ProblemBase
{
    [Theory]
    [InlineData(7, new int[]{5, 4, 3, 7}, true)]
    [InlineData(300, new int[]{57, 4, 3, 7}, true)]
    public void Test(int sum, int[] numbers, bool expected)
    {
        // Arrange
        // Act
        var result = CanSum(sum, numbers);

        // Assert
        Assert.Equal(expected, result);
    }

    bool CanSum(int sum, int[] numbers) => CanSum(sum, numbers, new());

    bool CanSum(int sum, int[] numbers, Dictionary<int, bool> memo)
    {
        if (memo.TryGetValue(sum, out bool cachedResult)) return cachedResult;
        if (sum == 0) return true;
        if (sum < 0) return false;

        for(int i=0; i<numbers.Length; i++)
        {
            if (CanSum(sum - numbers[i], numbers, memo))
            {
                memo[sum] = true;
                return true;
            }
        }

        memo[sum] = false;
        return false;
    }
}