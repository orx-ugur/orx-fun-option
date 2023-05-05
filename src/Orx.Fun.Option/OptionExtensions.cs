namespace Orx.Fun.Option;

/// <summary>
/// Extension methods for the option type <see cref="Opt{T}"/>.
/// </summary>
public static class OptionExtensions
{
    // ctors
    /// <summary>
    /// Creates an option of <typeparamref name="T"/> as None variant.
    /// <code>
    /// var noneInt = None&lt;int>();
    /// Assert(noneInt.IsNone);
    /// 
    /// // also:
    /// Opt&lt;string> name = default;
    /// Assert(name.IsNone);
    /// </code>
    /// </summary>
    public static Opt<T> None<T>()
        => new();
    /// <summary>
    /// Creates an option of <typeparamref name="T"/> as Some variant with the given <paramref name="value"/>.
    /// However, if the <paramref name="value"/> is null, it will map into None.
    /// <code>
    /// Opt&lt;double> number = Some(42.5);
    /// Assert(number.IsSome and number.Unwrap() == 42.5);
    /// 
    /// // on the other hand:
    /// string name = null;
    /// Opt&lt;string> optName = Some(name);
    /// Assert(optName.IsNone);
    /// </code>
    /// </summary>
    /// <param name="value">Expectedly non-null value of T.</param>
    public static Opt<T> Some<T>(T? value)
        => new(value);
    /// <summary>
    /// Creates a result of <typeparamref name="T"/> as Some variant with value <paramref name="value"/> if the <paramref name="someCondition"/> holds.
    /// Otherwise, it will return the None variant.
    /// <code>
    /// string team = "secret";
    /// int score = 42;
    /// 
    /// Opt&lt;string> winner = SomeIf(score > 30, team);
    /// Assert(winner == Some(team));
    /// 
    /// Opt&lt;string> loser = SomeIf(score &lt; 40, team);
    /// Assert(loser.IsNone);
    /// </code>
    /// </summary>
    /// <param name="someCondition">Condition that must hold for the return value to be Some(value).</param>
    /// <param name="value">Underlying value of the Some variant to be returned if someCondition holds.</param>
    public static Opt<T> SomeIf<T>(bool someCondition, T? value)
        => someCondition ? new(value) : None<T>();
    /// <summary>
    /// Lazy-in-evaluating-value counterpart of <see cref="SomeIf{T}(bool, T)"/>.
    /// </summary>
    /// <param name="someCondition">Condition that must hold for the return value to be Some(value).</param>
    /// <param name="lazyValue">Underlying value of the Some variant to be evaluated and returned if someCondition holds.</param>
    public static Opt<T> SomeIf<T>(bool someCondition, Func<T> lazyValue)
        => someCondition ? new(lazyValue()) : None<T>();
    // ctors - extension
    /// <summary>
    /// Creates an option of <typeparamref name="T"/> as Some variant with the given <paramref name="value"/>.
    /// However, if the <paramref name="value"/> is null, it will map into None.
    /// <code>
    /// string name = null;
    /// static string? GetName(int id)
    ///     => id == 0 ? "Mr Crabs" : null;
    /// Opt&lt;string> optName = GetName(0).SomeIfNotnull();
    /// Assert.Equal(Some("Mr Crabs"), optName);
    /// 
    /// optName = GetName(42).SomeIfNotnull();
    /// Assert.True(optName.IsNone);
    /// </code>
    /// </summary>
    /// <param name="value">A nullable value of T to be converted to the option type.</param>
    public static Opt<T> SomeIfNotnull<T>(this T? value) where T : class
        => new(value);

    // flatten
    /// <summary>
    /// Flattens the option of option of <typeparamref name="T"/>.
    /// Maps Opt&lt;Opt&lt;T>> to Opt&lt;T> as follows:
    /// <list type="bullet">
    /// <item>None => None&lt;T>(),</item>
    /// <item>Some(None&lt;T>()) => None&lt;T>(),</item>
    /// <item>Some(Some(T)) => Some(T).</item>
    /// </list>
    /// <code>
    /// Assert(None&lt;Opt&lt;char>>().Flatten() == None&lt;char>());
    /// Assert(Some(None&lt;char>()).Flatten() == None&lt;char>());
    /// Assert(Some(Some('c')).Flatten() == Some('c'));
    /// </code>
    /// </summary>
    /// <param name="option">Nested option to flatten.</param>
    public static Opt<T> Flatten<T>(this Opt<Opt<T>> option)
    {
        if (option.IsNone)
            return default;
        else
            return option.Unwrap();
    }


    // map - match with tuples
    /// <summary>
    /// Allows an option of a tuple (t1, t2) to map with a function taking two arguments t1 and t2.
    /// 
    /// <code>
    /// static int Add(int a, int b) => a + b;
    /// 
    /// var numbers = Some((1, 2));
    /// var sum = numbers.Map(Add);
    /// Assert(sum == Some(3));
    /// </code>
    /// 
    /// This is mostly useful in enabling function composition.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument of the map function.</typeparam>
    /// <typeparam name="T2">Type of the second argument of the map function.</typeparam>
    /// <typeparam name="TOut">Type of return value of the map function.</typeparam>
    /// <param name="option">Option to be mapped.</param>
    /// <param name="map">Map function.</param>
    /// <returns></returns>
    public static Opt<TOut> Map<T1, T2, TOut>(this Opt<(T1, T2)> option, Func<T1, T2, TOut> map)
        => option.Map(x => map(x.Item1, x.Item2));
}
