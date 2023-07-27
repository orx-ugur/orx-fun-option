using System.Runtime.CompilerServices;

namespace Orx.Fun.Option;

/// <summary>
/// Option type which can be either of the two variants: Some(value-of-<typeparamref name="T"/>) or None.
/// </summary>
/// <typeparam name="T">Any T.</typeparam>
public readonly struct Opt<T> : IEquatable<Opt<T>>
{
    // data
    readonly T? Val;
    /// <summary>
    /// Returns whether the option has Some value or not.
    /// <code>
    /// var someInt = Some(12);
    /// Assert(noneInt.IsNone);
    /// </code>
    /// </summary>
    public readonly bool IsSome;
    /// <summary>
    /// Returns whether the option is None or not.
    /// <code>
    /// var noneInt = None&lt;int>();
    /// Assert(noneInt.IsNone);
    /// </code>
    /// </summary>
    public bool IsNone => !IsSome;


    // ctor
    /// <summary>
    /// Option type of <typeparamref name="T"/>: either None or Some value.
    /// Parameterless ctor returns None; better use <see cref="OptionExtensions.Some{T}(T)"/> or <see cref="OptionExtensions.None{T}"/> to construct options by adding `using static OptRes.Ext`.
    /// </summary>
    public Opt()
    {
        Val = default;
        IsSome = false;
    }
    internal Opt(T? value)
    {
        Val = value;
        if (typeof(T).IsClass)
            IsSome = value != null;
        else
            IsSome = true;
    }


    // throw
    /// <summary>
    /// Returns the option back when <see cref="IsSome"/>; throws a NullReferenceException when <see cref="IsNone"/>.
    /// Can be called without breaking the flow of chained operations.
    /// <code>
    /// var interestRate = GetOptionalUser(input)
    ///     .ThrowIfNone("failed to get the user")
    ///     .Map(user => ComputeInterestRate(user))
    ///     .Unwrap();
    /// </code>
    /// </summary>
    public Opt<T> ThrowIfNone()
    {
        if (IsNone)
            throw new NullReferenceException($"ThrowIfNone is called on None<{typeof(T).Name}>().");
        else
            return this;
    }
    /// <summary>
    /// Returns the option back when <see cref="IsSome"/>; throws a NullReferenceException when <see cref="IsNone"/>.
    /// Appends the <paramref name="errorMessage"/> to the exception if the message <see cref="IsSome"/>.
    /// Can be called without breaking the flow of chained operations.
    /// <code>
    /// var interestRate = GetOptionalUser(input)
    ///     .ThrowIfNone("failed to get the user")
    ///     .Map(user => ComputeInterestRate(user))
    ///     .Unwrap();
    /// </code>
    /// </summary>
    /// <param name="errorMessage">Optional message to append to the exception message.</param>
    public Opt<T> ThrowIfNone(string errorMessage)
    {
        if (IsNone)
            throw new NullReferenceException(errorMessage);
        else
            return this;
    }
    /// <summary>
    /// Returns the option back when <see cref="IsSome"/>; throws a custom exception when <see cref="IsNone"/>.
    /// Exception thrown when IsNone is created by the provided method <paramref name="getException"/>.
    /// Can be called without breaking the flow of chained operations.
    /// <code>
    /// var interestRate = GetOptionalUser(input)
    ///     .ThrowIfNone(() => new ArithmeticException("sth went wrong"))
    ///     .Map(user => ComputeInterestRate(user))
    ///     .Unwrap();
    /// </code>
    /// </summary>
    /// <param name="getException">Method to be called to create the exception if the option is of None variant.</param>
    public Opt<T> ThrowIfNone<E>(Func<E> getException) where E : Exception
    {
        if (IsNone)
            throw getException();
        else
            return this;
    }


    // validate
    /// <summary>
    /// Returns back None if IsNone.
    /// Otherwise, returns Some(value) if <paramref name="validationCriterion"/>(value) holds; None if it does not hold.
    /// Especially useful in fluent input validation.
    /// <code>
    /// static Opt&lt;Account> MaybeParseAccount(..) { }
    /// static bool IsAccountNumberValid(int number) { }
    /// static bool DoesAccountExist(string code) { }
    /// 
    /// var account = MaybeParseAccount(..)
    ///                 .Validate(acc => IsAccountNumberValid(acc.Number))
    ///                 .Validate(acc => DoesAccountExist(acc.Code));
    /// // account will be Some(account) only if:
    /// // - MaybeParseAccount returns Some(account), and further,
    /// // - both IsAccountNumberValid and DoesAccountExist validation checks return true.
    /// </code>
    /// </summary>
    /// <param name="validationCriterion">Condition on the underlying value that should hold to get a Some, rather than None.</param>
    public Opt<T> SomeIf(Func<T, bool> validationCriterion)
        => IsNone || Val == null ? this : (validationCriterion(Val) ? this : new Opt<T>());


    // unwrap
    /// <summary>
    /// Returns the underlying value when <see cref="IsSome"/>; or throws when <see cref="IsNone"/>.
    /// Must be called shyly, as it is not necessary to unwrap until the final result is achieved due to Map, FlatMap and TryMap methods.
    /// <code>
    /// Opt&lt;int> optAge = "42".ParseIntOrNone();
    /// if (optAge.IsSome) {
    ///     int age = optAge.Unwrap(); // use the uwrapped age
    /// } else { // handle the None case
    /// }
    /// </code>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Unwrap()
        => (IsSome && Val != null) ? Val : throw new NullReferenceException("Cannot Unwrap None.");
    /// <summary>
    /// Similar to <see cref="Unwrap()"/> method except that the <paramref name="errorMessageIfNone"/> is appended to the error message if <see cref="IsNone"/>.
    /// </summary>
    /// <param name="errorMessageIfNone">Error message to append to the exception message that will be thrown if None.</param>
    public T Unwrap(string errorMessageIfNone)
        => (IsSome && Val != null) ? Val : throw new NullReferenceException(string.Format("Cannot Unwrap None. {0}", errorMessageIfNone));
    /// <summary>
    /// Returns the underlying value when <see cref="IsSome"/>; or returns the <paramref name="fallbackValue"/> when <see cref="IsNone"/>.
    /// This is a safe way to unwrap the optional, by explicitly handling the None variant.
    /// Use the lazy <see cref="UnwrapOr(Func{T})"/> variant if the computation of the fallback value is expensive.
    /// <code>
    /// Assert(Some(42).UnwrapOr(7) == 42);
    /// Assert(None&lt;int>().UnwrapOr(7) == 7);
    /// </code>
    /// </summary>
    /// <param name="fallbackValue">Fallback value that will be returned if the option is None.</param>
    public T UnwrapOr(T fallbackValue)
        => (IsSome && Val != null) ? Val : fallbackValue;
    /// <summary>
    /// Returns the underlying value when <see cref="IsSome"/>; or returns <paramref name="lazyFallbackValue"/>() when <see cref="IsNone"/>.
    /// This is a safe way to unwrap the optional, by explicitly handling the None variant.
    /// Use the eager <see cref="UnwrapOr(T)"/> variant if the fallback value is cheap or readily available.
    /// <code>
    /// static int GetCapacity(IEnumerable&lt;T> collection, Opt&lt;int> givenCapacity) {
    ///     // capacity will be either the givenCapacity, or the number of elements in the collection.
    ///     // note that, collection.Count() might be expensive requiring linear search.
    ///     // lazy call avoids this call when givenCapacity.IsSome.
    ///     return givenCapacity.UnwrapOr(() => collection.Count());
    /// }
    /// </code>
    /// </summary>
    /// <param name="lazyFallbackValue">Function to be called lazily to create the return value if the option is None.</param>
    public T UnwrapOr(Func<T> lazyFallbackValue)
        => (IsSome && Val != null) ? Val : lazyFallbackValue();
    /// <summary>
    /// (async version)
    /// <inheritdoc cref="UnwrapOr(Func{T})"/>
    /// </summary>
    /// <param name="lazyFallbackValue">Function to be called lazily to create the return value if the option is None.</param>
    public Task<T> UnwrapOrAsync(Func<Task<T>> lazyFallbackValue)
        => (IsSome && Val != null) ? Task.FromResult(Val) : lazyFallbackValue();


    // nullable
    /// <summary>
    /// Converts the option to nullable of T.
    /// </summary>
    /// <returns></returns>
    public T? AsNullable()
        => Val;


    // match
    /// <summary>
    /// Maps into <paramref name="whenSome"/>(Unwrap()) whenever IsSome; and into <paramref name="whenNone"/> otherwise.
    /// <code>
    /// Opt&lt;User> user = GetOptionalUser(..);
    /// string greeting = user.Match(u => $"Welcome back {u.Name}", "Hello");
    /// greeting = user.Match(whenSome: u => $"Welcome back {u.Name}", whenNone: "Hello");
    /// </code>
    /// </summary>
    /// <param name="whenSome">Mapping function (T -> TOut) that will be called with Unwrapped value to get the return value when Some.</param>
    /// <param name="whenNone">Return value when None.</param>
    public TOut Match<TOut>(Func<T, TOut> whenSome, TOut whenNone)
        => (IsSome && Val != null) ? whenSome(Val) : whenNone;
    /// <summary>
    /// Maps into <paramref name="whenSome"/>(Unwrap()) whenever IsSome; and into lazy <paramref name="whenNone"/>() otherwise.
    /// Similar to <see cref="Match{TOut}(Func{T, TOut}, TOut)"/> except that None variant is evaluated only when IsNone.
    /// <code>
    /// // assuming QueryAnonymousGreeting() is expensive.
    /// Opt&lt;User> user = GetOptionalUser(..);
    /// string greeting = user.Match(u => $"Welcome back {u.Name}", () => QueryAnonymousGreeting());
    /// </code>
    /// </summary>
    /// <param name="whenSome">Mapping function (T -> TOut) that will be called with Unwrapped value to get the return value when Some.</param>
    /// <param name="whenNone">Function to be called lazily to get the return value when None.</param>
    public TOut Match<TOut>(Func<T, TOut> whenSome, Func<TOut> whenNone)
        => (IsSome && Val != null) ? whenSome(Val) : whenNone();
    /// <summary>
    /// (async version) <inheritdoc cref="Match{TOut}(Func{T, TOut}, Func{TOut})"/>
    /// </summary>
    /// <param name="whenSome">Mapping function (T -> TOut) that will be called with Unwrapped value to get the return value when Some.</param>
    /// <param name="whenNone">Function to be called lazily to get the return value when None.</param>
    public Task<TOut> MatchAsync<TOut>(Func<T, Task<TOut>> whenSome, Func<Task<TOut>> whenNone)
        => (IsSome && Val != null) ? whenSome(Val) : whenNone();
    /// <summary>
    /// Executes <paramref name="whenSome"/>(Unwrap()) if IsSome; <paramref name="whenNone"/>() otherwise.
    /// <code>
    /// static Greet(Opt&lt;User> user) {
    ///     user.MatchDo(
    ///         whenSome: u => Console.WriteLine($"Welcome back {u.Name}"),
    ///         whenNone: Console.WriteLine("Hello")
    ///     );
    /// }
    /// </code>
    /// </summary>
    /// <param name="whenSome">Action to be called lazily when Some.</param>
    /// <param name="whenNone">Action to be called lazily when None.</param>
    public void MatchDo(Action<T> whenSome, Action whenNone)
    {
        if (IsSome && Val != null)
            whenSome(Val);
        else
            whenNone();
    }


    // do
    /// <summary>
    /// Runs <paramref name="action"/>(Unwrap()) only if IsSome; and returns itself back.
    /// <code>
    /// // the logging call will only be made if the result of GetOptionalUser is Some of a user.
    /// // Since Do returns back the option, it can still be assigned to var 'user'.
    /// Opt&lt;User> user = GetOptionalUser().Do(u => Log.Info($"User '{u.Name}' grabbed"));
    /// </code>
    /// </summary>
    /// <param name="action">Action that will be called with the underlying value when Some.</param>
    public Opt<T> Do(Action<T> action)
    {
        if (IsSome && Val != null)
            action(Val);
        return this;
    }
    // do-if-none
    /// <summary>
    /// Runs <paramref name="actionOnNone"/>() only if IsNone; and returns itself back.
    /// Counterpart of <see cref="Do(Action{T})"/> for the None variant.
    /// <code>
    /// // the logging call will only be made if the result of GetOptionalUser is None.
    /// // Since DoIfNone returns back the option, it can still be assigned to var 'user'.
    /// Opt&lt;User> user = GetOptionalUser().DoIfNone(() => Log.Warning("User could not be read"));
    /// </code>
    /// </summary>
    /// <param name="actionOnNone">Action that will be called when None.</param>
    public Opt<T> DoIfNone(Action actionOnNone)
    {
        if (IsNone)
            actionOnNone();
        return this;
    }


    // map
    /// <summary>
    /// Returns None when IsNone; Some(<paramref name="map"/>(Unwrap())) when IsSome.
    /// <code>
    /// // session will be None if the user is None; Some of a session for the user when Some.
    /// Opt&lt;Session> session = GetOptionalUser.Map(user => NewSession(user.Secrets));
    /// </code>
    /// </summary>
    /// <param name="map">Mapper function (T -> TOut) to be called with the underlying value when Some.</param>
    public Opt<TOut> Map<TOut>(Func<T, TOut> map)
        => (IsSome && Val != null) ? new(map(Val)) : default;
    /// <summary>
    /// (async version) <inheritdoc cref="Map{TOut}(Func{T, TOut})"/>
    /// </summary>
    /// <param name="map">Mapper function (T -> TOut) to be called with the underlying value when Some.</param>
    public async Task<Opt<TOut>> MapAsync<TOut>(Func<T, Task<TOut>> map)
        => (IsSome && Val != null) ? new(await map(Val)) : new Opt<TOut>();


    // flat-map
    /// <summary>
    /// Returns None when IsNone; <paramref name="map"/>(val) when IsSome flattening the result.
    /// Shorthand combining Map and Flatten calls.
    /// <code>
    /// static Opt&lt;User> GetOptionalUser() {
    ///     // method that tries to get the user, which can be omitted.
    ///     ...
    /// }
    /// static Opt&lt;string> GetNickname(User user) {
    ///     // method that tries to get the nickname of the passed-in user; which is optional
    ///     ...
    /// }
    /// Opt&lt;string> nickname = GetOptionalUser().FlatMap(GetNickname);
    /// // equivalent to both below:
    /// nickname = GetOptionalUser().FlatMap(user => GetNickname(user));
    /// nickname = GetOptionalUser().Map(user => GetNickname(user) /*Opt&lt;Opt&lt;string>>*/).Flatten();
    /// </code>
    /// </summary>
    /// <param name="map">Function (T -> Opt&lt;TOut>) mapping the underlying value to option of TOut if IsSome.</param>
    public Opt<TOut> FlatMap<TOut>(Func<T, Opt<TOut>> map)
        => (IsSome && Val != null) ? map(Val) : default;
    /// <summary>
    /// (async version) <inheritdoc cref="FlatMap{TOut}(Func{T, Opt{TOut}})"/>
    /// </summary>
    /// <param name="map">Function (T -> Opt&lt;TOut>) mapping the underlying value to option of TOut if IsSome.</param>
    public async Task<Opt<TOut>> FlatMap<TOut>(Func<T, Task<Opt<TOut>>> map)
        => (IsSome && Val != null) ? (await map(Val)) : default;


    // logical combinations
    /// <summary>
    /// Combines two options: this and <paramref name="other"/> as follows:
    /// <list type="bullet">
    /// <item>returns Some of a tuple of both values if both options are Some;</item>
    /// <item>returns None otherwise.</item>
    /// </list>
    /// 
    /// <code>
    /// var combined = Some(12).And(Some(true));
    /// Assert.Equal(Some((12, true)), combined);
    /// 
    /// combined = Some(12).And(None&lt;bool>());
    /// Assert.True(combined.IsNone);
    /// 
    /// combined = None&lt;int>().And(Some(true));
    /// Assert.True(combined.IsNone);
    /// 
    /// combined = None&lt;int>().And(None&lt;bool>());
    /// Assert.True(combined.IsNone);
    /// </code>
    /// </summary>
    /// <param name="other">Other option to combine with.</param>
    /// <returns></returns>
    public Opt<(T, T2)> And<T2>(Opt<T2> other)
    {
        if (IsSome && other.IsSome && Val != null && other.Val != null)
            return new((Val, other.Val));
        else
            return new();
    }
    /// <summary>
    /// Combines two options: this and <paramref name="other"/> as follows:
    /// <list type="bullet">
    /// <item>returns this if this is Some;</item>
    /// <item>returns <paramref name="other"/> otherwise.</item>
    /// </list>
    /// 
    /// <para>In other words, this is a flattened alternative to <see cref="UnwrapOr(T)"/>.</para>
    /// 
    /// <code>
    /// var or = Some(12).Or(Some(13));
    /// Assert.Equal(Some(12), or);
    /// 
    /// or = Some(12).Or(None&lt;int>());
    /// Assert.Equal(Some(12), or);
    /// 
    /// or = None&lt;int>().Or(Some(13));
    /// Assert.Equal(Some(13), or);
    /// 
    /// or = None&lt;int>().Or(None&lt;bool>());
    /// Assert.True(or.IsNone);
    /// </code>
    /// </summary>
    /// <param name="other">Other option to combine with.</param>
    /// <returns></returns>
    public Opt<T> Or(Opt<T> other)
    {
        if (IsSome)
            return this;
        else
            return other;
    }
    /// <summary>
    /// (lazy version) <inheritdoc cref="Or(Opt{T})"/>
    /// </summary>
    /// <param name="other">Other option to combine with.</param>
    /// <returns></returns>
    public Opt<T> Or(Func<Opt<T>> other)
    {
        if (IsSome)
            return this;
        else
            return other();
    }


    // compose
    /// <summary>
    /// Simply returns Some&lt;T> function: val => Some(val).
    /// Useful for composing functions of Opt&lt;T> type.
    /// </summary>
    /// <returns></returns>
    public static Func<T, Opt<T>> Pure() => OptionExtensions.Some;


    // common
    /// <summary>
    /// Returns the text representation of the option.
    /// </summary>
    public override string ToString()
        => (!IsSome || Val == null) ? "None" : string.Format("Some({0})", Val);
    /// <summary>
    /// Returns whether this option is equal to the <paramref name="other"/>.
    /// </summary>
    /// <param name="other">Other optional to compare to.</param>
    public override bool Equals(object? other)
        => other != null && (other is Opt<T>) && (Equals(other));
    /// <summary>
    /// Returns true if both values are <see cref="IsNone"/>; or both <see cref="IsSome"/> and their unwrapped values are equal; false otherwise.
    /// </summary>
    /// <param name="other">Other optional to compare to.</param>
    public bool Equals(Opt<T> other)
    {
        if (IsNone)
            return other.IsNone;
        else if (other.IsNone)
            return false;
        else
        {
            if (Val != null && other.Val != null)
                return Val.Equals(other.Val);
            else
                return false;
        }
    }
    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    public override int GetHashCode()
        => Val == null ? int.MinValue : Val.GetHashCode();
    /// <summary>
    /// Returns true if both values are <see cref="IsSome"/> and their unwrapped values are equal; false otherwise.
    /// <code>
    /// AssertEqual(None&lt;int>() == None&lt;int>(), false);
    /// AssertEqual(None&lt;int>() == Some(42), false);
    /// AssertEqual(Some(42) == None&lt;int>(), false);
    /// AssertEqual(Some(42) == Some(7), false);
    /// AssertEqual(Some(42) == Some(42), true);
    /// </code>
    /// </summary>
    /// <param name="left">Lhs of the equality operator.</param>
    /// <param name="right">Rhs of the equality operator.</param>
    public static bool operator ==(Opt<T> left, Opt<T> right)
        => left.IsSome && right.IsSome && left.Val != null && right.Val != null && left.Val.Equals(right.Val);
    /// <summary>
    /// Returns true if either value is <see cref="IsNone"/> or their unwrapped values are not equal; false otherwise.
    /// <code>
    /// AssertEqual(None&lt;int>() != None&lt;int>(), true);
    /// AssertEqual(None&lt;int>() != Some(42), true);
    /// AssertEqual(Some(42) != None&lt;int>(), true);
    /// AssertEqual(Some(42) != Some(7), true);
    /// AssertEqual(Some(42) != Some(42), false);
    /// </code>
    /// </summary>
    /// <param name="left">Lhs of the inequality operator.</param>
    /// <param name="right">Rhs of the inequality operator.</param>
    public static bool operator !=(Opt<T> left, Opt<T> right)
        => !(left == right);
}
