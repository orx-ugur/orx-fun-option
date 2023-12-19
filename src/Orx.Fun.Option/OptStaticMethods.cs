namespace Orx.Fun.Option;

/// <summary>
/// Static option constructors.
/// </summary>
public static class Opt
{
    // eager
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <returns></returns>
    public static Opt<(T1, T2)> AndAll
        <T1, T2>(
        Opt<T1> opt1, Opt<T2> opt2
        )
    {
        if (opt1.IsSome && opt2.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3)> AndAll
        <T1, T2, T3>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3
        )
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4)> AndAll
        <T1, T2, T3, T4>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3, Opt<T4> opt4
        )
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome && opt4.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap(), opt4.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5)> AndAll
        <T1, T2, T3, T4, T5>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3, Opt<T4> opt4,
        Opt<T5> opt5)
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome && opt4.IsSome
            && opt5.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap(), opt4.Unwrap(),
                opt5.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6)> AndAll
        <T1, T2, T3, T4, T5, T6>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3, Opt<T4> opt4,
        Opt<T5> opt5, Opt<T6> opt6)
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome && opt4.IsSome
            && opt5.IsSome && opt6.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap(), opt4.Unwrap(),
                opt5.Unwrap(), opt6.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <param name="opt7"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6, T7)> AndAll
        <T1, T2, T3, T4, T5, T6, T7>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3, Opt<T4> opt4,
        Opt<T5> opt5, Opt<T6> opt6, Opt<T7> opt7)
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome && opt4.IsSome
            && opt5.IsSome && opt6.IsSome && opt7.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap(), opt4.Unwrap(),
                opt5.Unwrap(), opt6.Unwrap(), opt7.Unwrap()
                ));
        }
        else
            return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <param name="opt7"></param>
    /// <param name="opt8"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6, T7, T8)> AndAll
        <T1, T2, T3, T4, T5, T6, T7, T8>(
        Opt<T1> opt1, Opt<T2> opt2, Opt<T3> opt3, Opt<T4> opt4,
        Opt<T5> opt5, Opt<T6> opt6, Opt<T7> opt7, Opt<T8> opt8)
    {
        if (opt1.IsSome && opt2.IsSome && opt3.IsSome && opt4.IsSome
            && opt5.IsSome && opt6.IsSome && opt7.IsSome && opt8.IsSome
            )
        {
            return new((opt1.Unwrap(), opt2.Unwrap(), opt3.Unwrap(), opt4.Unwrap(),
                opt5.Unwrap(), opt6.Unwrap(), opt7.Unwrap(), opt8.Unwrap()));
        }
        else
            return default;
    }


    // lazy
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <returns></returns>
    public static Opt<(T1, T2)> AndAll
        <T1, T2>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                return new((
                    x1.Unwrap(), x2.Unwrap()
                ));
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3)> AndAll
        <T1, T2, T3>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    return new((
                        x1.Unwrap(), x2.Unwrap(), x3.Unwrap()
                    ));
                }
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4)> AndAll
        <T1, T2, T3, T4>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3, Func<Opt<T4>> opt4
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    var x4 = opt4();
                    if (x4.IsSome)
                    {
                        return new((
                            x1.Unwrap(), x2.Unwrap(), x3.Unwrap(), x4.Unwrap()
                        ));
                    }
                }
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5)> AndAll
        <T1, T2, T3, T4, T5>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3, Func<Opt<T4>> opt4,
        Func<Opt<T5>> opt5
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    var x4 = opt4();
                    if (x4.IsSome)
                    {
                        var x5 = opt5();
                        if (x5.IsSome)
                        {
                            return new((
                                x1.Unwrap(), x2.Unwrap(), x3.Unwrap(), x4.Unwrap(),
                                x5.Unwrap()
                            ));
                        }
                    }
                }
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6)> AndAll
        <T1, T2, T3, T4, T5, T6>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3, Func<Opt<T4>> opt4,
        Func<Opt<T5>> opt5, Func<Opt<T6>> opt6
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    var x4 = opt4();
                    if (x4.IsSome)
                    {
                        var x5 = opt5();
                        if (x5.IsSome)
                        {
                            var x6 = opt6();
                            if (x6.IsSome)
                            {
                                return new((
                                    x1.Unwrap(), x2.Unwrap(), x3.Unwrap(), x4.Unwrap(),
                                    x5.Unwrap(), x6.Unwrap()
                                ));
                            }
                        }
                    }
                }
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <param name="opt7"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6, T7)> AndAll
        <T1, T2, T3, T4, T5, T6, T7>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3, Func<Opt<T4>> opt4,
        Func<Opt<T5>> opt5, Func<Opt<T6>> opt6, Func<Opt<T7>> opt7
        )
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    var x4 = opt4();
                    if (x4.IsSome)
                    {
                        var x5 = opt5();
                        if (x5.IsSome)
                        {
                            var x6 = opt6();
                            if (x6.IsSome)
                            {
                                var x7 = opt7();
                                if (x7.IsSome)
                                {
                                    return new((
                                        x1.Unwrap(), x2.Unwrap(), x3.Unwrap(), x4.Unwrap(),
                                        x5.Unwrap(), x6.Unwrap(), x7.Unwrap()
                                    ));
                                }
                            }
                        }
                    }
                }
            }
        }
        return default;
    }
    /// <summary>
    /// Returns the tuple combining unwrapped values of the optionals if all of them are of Some variant; returns None otherwise.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <param name="opt1"></param>
    /// <param name="opt2"></param>
    /// <param name="opt3"></param>
    /// <param name="opt4"></param>
    /// <param name="opt5"></param>
    /// <param name="opt6"></param>
    /// <param name="opt7"></param>
    /// <param name="opt8"></param>
    /// <returns></returns>
    public static Opt<(T1, T2, T3, T4, T5, T6, T7, T8)> AndAll
        <T1, T2, T3, T4, T5, T6, T7, T8>(
        Func<Opt<T1>> opt1, Func<Opt<T2>> opt2, Func<Opt<T3>> opt3, Func<Opt<T4>> opt4,
        Func<Opt<T5>> opt5, Func<Opt<T6>> opt6, Func<Opt<T7>> opt7, Func<Opt<T8>> opt8)
    {
        var x1 = opt1();
        if (x1.IsSome)
        {
            var x2 = opt2();
            if (x2.IsSome)
            {
                var x3 = opt3();
                if (x3.IsSome)
                {
                    var x4 = opt4();
                    if (x4.IsSome)
                    {
                        var x5 = opt5();
                        if (x5.IsSome)
                        {
                            var x6 = opt6();
                            if (x6.IsSome)
                            {
                                var x7 = opt7();
                                if (x7.IsSome)
                                {
                                    var x8 = opt8();
                                    if (x8.IsSome)
                                    {
                                        return new((
                                            x1.Unwrap(), x2.Unwrap(), x3.Unwrap(), x4.Unwrap(),
                                            x5.Unwrap(), x6.Unwrap(), x7.Unwrap(), x8.Unwrap()
                                        ));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return default;
    }
}
