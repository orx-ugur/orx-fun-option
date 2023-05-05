using static Orx.Fun.Option.OptionExtensions;

namespace Orx.Fun.Option;

/// <summary>
/// Extension methods for linq methods using the option type <see cref="Opt{T}"/>.
/// </summary>
public static class OptionExtensionsLinq
{
    // first/last or none
    /// <summary>
    /// Returns Some of the first element of the <paramref name="collection"/> if it is non-empty; None otherwise.
    /// <code>
    /// Assert(Array.Empty&lt;Title>().FirstOrNone().IsNone);
    /// Assert((new int[2] { 1, 2 }).FirstOrNone() == Some(1));
    /// </code>
    /// </summary>
    /// <param name="collection">Collection.</param>
    public static Opt<T> FirstOrNone<T>(this IEnumerable<T> collection)
    {
        foreach (var item in collection)
            return Some(item);
        return None<T>();
    }
    /// <summary>
    /// Returns Some of the first element of the <paramref name="collection"/> satisfying the <paramref name="filter"/> if any; None otherwise.
    /// <code>
    /// Assert(Array.Empty&lt;int>().FirstOrNone(x => x > 2).IsNone);
    /// Assert((new int[2] { 1, 2 }).FirstOrNone(x => x > 2).IsNone);
    /// Assert((new int[2] { 1, 2 }).FirstOrNone(x => x > 1) == Some(2));
    /// </code>
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="filter">Predicate to filter the items of the collection.</param>
    public static Opt<T> FirstOrNone<T>(this IEnumerable<T> collection, Func<T, bool> filter)
    {
        foreach (var item in collection)
            if (filter(item))
                return Some(item);
        return None<T>();
    }
    /// <summary>
    /// Returns Some of the last element of the <paramref name="collection"/> if it is non-empty; None otherwise.
    /// <code>
    /// Assert(Array.Empty&lt;Title>().FirstOrNone().IsNone);
    /// Assert((new int[2] { 1, 2 }).LastOrNone() == Some(2));
    /// </code>
    /// </summary>
    /// <param name="collection">Collection.</param>
    public static Opt<T> LastOrNone<T>(this IEnumerable<T> collection)
    {
        bool hasAny = collection.GetEnumerator().MoveNext();
        return hasAny ? Some(collection.Last()) : None<T>();
    }
    /// <summary>
    /// Returns Some of the last element of the <paramref name="collection"/> satisfying the <paramref name="filter"/> if any; None otherwise.
    /// <code>
    /// Assert(Array.Empty&lt;int>().LastOrNone(x => x > 2).IsNone);
    /// Assert((new int[2] { 2, 1 }).LastOrNone(x => x > 2).IsNone);
    /// Assert((new int[2] { 2, 1 }).LastOrNone(x => x > 1) == Some(2));
    /// </code>
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="filter">Predicate to filter the items of the collection.</param>
    public static Opt<T> LastOrNone<T>(this IEnumerable<T> collection, Func<T, bool> filter)
        => LastOrNone(collection.Where(filter));


    // dictionary
    /// <summary>
    /// Gets Some of the value of the key-value pair in the <paramref name="dictionary"/> with the given <paramref name="key"/>; None if it doesn't exist.
    /// <code>
    /// var dict = new Dictionary&lt;string, int>()
    /// {
    ///     { "Good", 42 },
    ///     { "Okay", 12 },
    /// };
    /// 
    /// Assert.True(dict.Get("Good").IsSome);
    /// Assert.Equal(Some(42), dict.Get("Good"));
    /// Assert.Equal(42, dict.Get("Good").Unwrap());
    /// Assert.Equal(42, dict.Get("Good").UnwrapOr(100));
    /// 
    /// Assert.True(dict.Get("Absent").IsNone);
    /// Assert.Equal(None&lt;int>(), dict.Get("Absent"));
    /// Assert.Equal(100, dict.Get("Absent").UnwrapOr(100));
    /// </code>
    /// </summary>
    /// <param name="dictionary">Dictionary to check the key.</param>
    /// <param name="key">Key of the dictionary item to grab.</param>
    public static Opt<V> Get<K, V>(this Dictionary<K, V> dictionary, K key) where K : notnull
    {
        if (dictionary.TryGetValue(key, out var val))
            return Some(val);
        else
            return None<V>();
    }


    // array
    /// <summary>
    /// Returns Some of the element at the given <paramref name="index"/> of the <paramref name="array"/> if it is a valid index; None otherwise.
    /// <code>
    /// Assert(Array.Empty&lt;Title>().Get(1).IsNone);
    /// Assert((new int[2] { 1, 2 }).Get(2) == Some(1));
    /// Assert((new int[2] { 1, 2 }).Get(0) == Some(1));
    /// </code>
    /// </summary>
    /// <param name="array">Array.</param>
    /// <param name="index">Index of the element to return.</param>
    public static Opt<T> Get<T>(this T[] array, int index)
    {
        if (index >= 0 && index < array.Length)
            return new(array[index]);
        else
            return None<T>();
    }
}
