
[Problem(1,"Two Sum", "https://leetcode.com/problems/two-sum/")]
public class P1_TwoSum : ProblemBase
{
    [Theory]
    [InlineData(new int[]{2,7,11,15}, 9, new int[]{0,1})]
    [InlineData(new int[]{3,2,4}, 6, new int[]{1,2})]
    [InlineData(new int[]{3,3}, 6, new int[]{0,1})]
    public void Test(int[] numbers, int target, int[] expected)
    {
        int[] result = TwoSum(numbers, target);
        Assert.Equal(expected, result);
    }

    private int[] TwoSum(int[] nums, int target)
    {
        for(int i=0; i<nums.Length; i++)
        {
            for(int j=0; j<nums.Length; j++)
            {
                if (i != j && nums[i] + nums[j] == target)
                {
                    return new int[]{i,j};
                }
            }
        }

        return new int[]{-1,-1};
    }
}