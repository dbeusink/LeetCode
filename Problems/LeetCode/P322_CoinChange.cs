
namespace LeetCode;

[Problem(322,"Coin Change", "https://leetcode.com/problems/coin-change/")]
public class P322_CoinChange : ProblemBase
{
    [Theory]
    [InlineData(new int[]{1,2,5}, 11, 3)]
    [InlineData(new int[]{2}, 3, -1)]
    [InlineData(new int[]{1}, 0, 0)]
    [InlineData(new int[]{186,419,83,408}, 6249, 20)]
    public void Test(int[] coins, int amount, int expected)
    {
       // Arrange
       // Act
       int result = CoinChange(coins, amount);

       // Assert
       Assert.Equal(expected, result);
    }

    // First experience with DP (using the top-down/memoization approach).
    // Used resources: https://www.geeksforgeeks.org/what-is-memoization-a-complete-tutorial/
    // 
    public static int CoinChange(int[] coins, int amount) => CoinChange(coins, amount, [ ]);
    public static int CoinChange(int[] coins, int amount, Dictionary<int, int> memo)
    {
        if (memo.TryGetValue(amount, out int cachedResult)) return cachedResult;
        if (amount < 0) return -1;
        if (amount == 0) return 0;

        int leastAmount = -1;
        foreach(var coin in coins)
        {
            int candidate = CoinChange(coins, amount - coin, memo);
            if (candidate >= 0 && (leastAmount == -1 || candidate + 1 < leastAmount))
            {
                leastAmount = candidate + 1;
            }
        }

        memo[amount] = leastAmount;
        return leastAmount;
    }
}