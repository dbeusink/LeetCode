
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
    public static int CoinChange(int[] coins, int amount)
    {
        Dictionary<int, int> dp = new(); 
        return GetMinimumAmountOfCoins(amount);

        int GetMinimumAmountOfCoins(int n)
        {
            if (n <= 0) return 0;

            int leastCoins = -1;
            int minimumAmountOfCoins;
            int amountAfterSubtraction;
            for(int i=0; i<coins.Length; i++)
            {
                amountAfterSubtraction = n - coins[i];

                // Only calculate least amount of coins if we can subtract this coin; if all coins fail we return -1.
                if (amountAfterSubtraction >= 0)
                {
                    minimumAmountOfCoins = dp.ContainsKey(amountAfterSubtraction) ?
                        dp[amountAfterSubtraction] : // Already solved this subproblem.
                        GetMinimumAmountOfCoins(amountAfterSubtraction); // New subproblem.

                    if (minimumAmountOfCoins != -1 && (++minimumAmountOfCoins < leastCoins || leastCoins == -1))
                    {
                        leastCoins = minimumAmountOfCoins;
                    }
                }
            }

            return dp[n] = leastCoins;
        }
    }
}