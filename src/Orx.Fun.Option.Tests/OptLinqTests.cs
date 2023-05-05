namespace Orx.Fun.Option.Tests;

public class OptLinqTests
{
    [Fact]
    public void FirstOrNone()
    {
        int[] col = Array.Empty<int>();

        Assert.Throws<InvalidOperationException>(() => col.First());                // not good!
        Assert.True(col.FirstOrNone().IsNone);                                      // better

        col = new int[] { 0, 1, 2 };
        Assert.Throws<InvalidOperationException>(() => col.First(x => x > 10));     // not good!
        Assert.True(col.FirstOrNone(x => x > 10).IsNone);                           // better

        col = new int[] { 0, 1, 2 };
        Assert.Equal(0, col.FirstOrDefault());      // is it the first, or is it default?
        Assert.Equal(col.FirstOrNone(), Some(0));   // we know it is the first
    }

    [Fact]
    public void LastOrNone()
    {
        int[] col = Array.Empty<int>();

        Assert.Throws<InvalidOperationException>(() => col.Last());                 // not good!
        Assert.True(col.LastOrNone().IsNone);                                       // better

        col = new int[] { 0, 1, 2 };
        Assert.Throws<InvalidOperationException>(() => col.Last(x => x > 10));      // not good!
        Assert.True(col.LastOrNone(x => x > 10).IsNone);                            // better

        col = new int[] { 0, 1, 2, 0 };
        Assert.Equal(0, col.LastOrDefault());      // is it the last, or is it default?
        Assert.Equal(col.LastOrNone(), Some(0));   // we know it is the last
    }


    [Fact]
    public void DictGet()
    {
        Dictionary<string, int> dict = new()
        {
            { "Good", 42 },
            { "Okay", 12 },
        };

        Assert.True(dict.Get("Good").IsSome);
        Assert.Equal(Some(42), dict.Get("Good"));
        Assert.Equal(42, dict.Get("Good").Unwrap());
        Assert.Equal(42, dict.Get("Good").UnwrapOr(100));

        Assert.True(dict.Get("Absent").IsNone);
        Assert.Equal(None<int>(), dict.Get("Absent"));
        Assert.Equal(100, dict.Get("Absent").UnwrapOr(100));
    }
}
