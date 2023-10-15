
[Problem("Stars")]
public class P0_Starts : ProblemBase
{
    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(45)]
    public void Test(int rows)
    {
        // Arrange
        // Act
        var result = GetStarLines(rows).ToArray();

        // Assert
        Assert.Equal(rows, result.Length);
        for(int i=0; i< result.Length; i++)
        {
            Console.WriteLine(result[i]);
            Assert.Equal(result[i].Where(x => x == '*').Count(), i*2+1);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(45)]
    public void Test_UsingPrimitives(int rows)
    {
        // Arrange
        // Act
        var result = GetStarLinesUsingPrimitives(rows).ToArray();

        // Assert
        Assert.Equal(rows, result.Length);
        for(int i=0; i< result.Length; i++)
        {
            Console.WriteLine(result[i]);
            Assert.Equal(result[i].Where(x => x == '*').Count(), i*2+1);
        }
    }

    [Fact]
    public void Test_CanHandleNegatives()
    {
        var result = GetStarLines(-1).ToArray();

        Assert.Empty(result);
    }

    [Fact]
    public void Test_UsingPrimitives_CanHandleNegatives()
    {
        var result = GetStarLinesUsingPrimitives(-1).ToArray();

        Assert.Empty(result);
    }

    private IEnumerable<string> GetStarLines(int rows) 
    {
        if (rows < 0)
        {
            yield break;
        }

        for(int i=0; i<rows; i++)
        {
            yield return new string(Enumerable.Repeat(' ', rows-i-1)
                .Concat(Enumerable.Repeat('*', i*2+1))
                .Concat(Enumerable.Repeat(' ', rows-i-1))
                .ToArray());
        }
    }

    private IEnumerable<string> GetStarLinesUsingPrimitives(int rows) 
    {
        if (rows < 0)
        {
            yield break;
        }

        int width = rows*2+1;
        for(int i=0; i<rows; i++)
        {
            char[] chars = new char[width];
            for(int j=0; j<width; j++)
            {
                chars[j] = (j >= rows-i && j < width-rows+i) ? '*' : ' ';
            }

            yield return new string(chars.AsSpan());
        }
    }
}