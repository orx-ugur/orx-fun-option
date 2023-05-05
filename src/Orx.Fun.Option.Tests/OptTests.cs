namespace Orx.Fun.Option.Tests;

public class OptTests
{
    record User(string UserName);
    record Role(string RoleName);
    record View(string ViewName);

    [Fact]
    public void DefaultCtors()
    {
        Opt<int> opt = default; // instead use: None<int>()
        Assert.True(opt.IsNone);
        Assert.Throws<NullReferenceException>(() => opt.Unwrap());

        opt = new Opt<int>(); // instead use: None<int>()
        Assert.True(opt.IsNone);
        Assert.Throws<NullReferenceException>(() => opt.Unwrap());
    }

    [Fact]
    public void Ctors()
    {
        Opt<int> opt = Some(42);
        Assert.True(opt.IsSome);
        Assert.Equal(Some(42), opt);
        Assert.Equal(42, opt.Unwrap());

        opt = None<int>();
        Assert.True(opt.IsNone);
        Assert.Throws<NullReferenceException>(() => opt.Unwrap());
    }

    [Fact]
    public void SomeIfCtor()
    {
        // chained validation
        // we want a number to be Some if
        // * it is nonnegative
        // * even
        // * and divisible by 3

        static void AssertValidator(Func<int, Opt<int>> validator)
        {
            var res = validator(-1);
            Assert.True(res.IsNone);

            res = validator(3);
            Assert.True(res.IsNone);

            res = validator(4);
            Assert.True(res.IsNone);

            res = validator(6);
            Assert.Equal(Some(6), res);
        }

        static Opt<int> ValidateLambdas(int number)
        {
            return Some(number)
                .SomeIf(num => num >= 0)
                .SomeIf(num => num % 2 == 0)
                .SomeIf(num => num % 3 == 0);
        }
        AssertValidator(ValidateLambdas);

        // composed - lambdas
        var validateLambdasComposed = Opt<int>.Pure()
            .SomeIf(num => num >= 0)
            .SomeIf(num => num % 2 == 0)
            .SomeIf(num => num % 3 == 0);
        AssertValidator(validateLambdasComposed);

        // pointfree
        static bool IsNonnegative(int num) => num >= 0;
        static bool IsEven(int num) => num % 2 == 0;
        static bool IsDivisableBy3(int num) => num % 3 == 0;
        var validatePointfree = Opt<int>.Pure()
            .SomeIf(IsNonnegative)
            .SomeIf(IsEven)
            .SomeIf(IsDivisableBy3);
        AssertValidator(validatePointfree);
    }

    [Fact]
    public void SomeIfNotnullCtor()
    {
        // consider an external method which can return a nullable of T?
        // say the method returns 'value', then
        // value.SomeIfNotnull() converts T? into Opt<T>
        // which is None if value happened to be null.

        static string? GetNickName(string name)
            => name == "Akasha" ? "QoP" : null;

        Opt<string> nickSome = GetNickName("Akasha").SomeIfNotnull();
        Assert.Equal(Some("QoP"), nickSome);
        Assert.Equal("QoP", nickSome.Unwrap());

        Opt<string> nickNone = GetNickName("Tinker").SomeIfNotnull();
        Assert.Equal(None<string>(), nickNone);
        Assert.True(nickNone.IsNone);
    }

    [Fact]
    public void NullGuard()
    {
        // Possible null reference return.
#pragma warning disable CS8603
        static string GetNullString() => null;
#pragma warning restore CS8603

        string str = GetNullString();
        Opt<string> opt = Some(str); // null references cannot be Some(T)
        Assert.True(opt.IsNone);
        Assert.Throws<NullReferenceException>(() => opt.Unwrap());
    }

    [Fact]
    public void EqualityChecks()
    {
        Assert.Equal(None<int>(), None<int>());
        Assert.NotEqual(None<int>(), Some(12));
        Assert.Equal(Some(42), Some(42));
        Assert.NotEqual(Some(12), Some(42));
    }

    [Fact]
    public void Unwrap()
    {
        Opt<int> opt = Some(42);
        int value = opt.Unwrap(); // dangerous call! -> the only method that can throw
        Assert.Equal(42, value);

        opt = None<int>();
        Assert.Throws<NullReferenceException>(() => opt.Unwrap());
    }

    [Fact]
    public void UnwrapOr()
    {
        Opt<int> opt = Some(42);
        int value = opt.UnwrapOr(10);
        Assert.Equal(42, value);

        opt = None<int>();
        value = opt.UnwrapOr(10);
        Assert.Equal(10, value);

        // lazy version in case computation of fallback value is demanding
        opt = None<int>();
        value = opt.UnwrapOr(() => 10);
        Assert.Equal(10, value);
    }

    [Fact]
    public void ThrowIfNone()
    {
        // just a shorthand for:
        // if (opt.IsNone)
        //     throw new NullReferenceException("error!");

        Opt<int> opt = Some(42);
        int value = opt.ThrowIfNone("error!").Unwrap();
        Assert.Equal(42, value);

        // why does it throw NullReferenceException?
        // - not to introduce yet another exception type
        // - but see below to throw the exception you want
        opt = None<int>();
        Assert.Throws<NullReferenceException>(() => opt.ThrowIfNone("error!"));

        Assert.Throws<DivideByZeroException>(
            () => opt.ThrowIfNone(() => new DivideByZeroException("mission abort")));
    }

    [Fact]
    public void Match()
    {
        Opt<string> name = Some("Merlin");
        string greeting = name.Match(name => $"Welcome {name}", "Welcome guest");
        // equivalently:
        greeting = name.Match(
            whenSome: name => $"Welcome {name}",
            whenNone: "Welcome guest");
        Assert.Equal("Welcome Merlin", greeting);

        name = None<string>();
        greeting = name.Match(name => $"Welcome {name}", "Welcome guest");
        Assert.Equal("Welcome guest", greeting);

        // or match none lazily
        greeting = name.Match(name => $"Welcome {name}", () => "Welcome guest");
        Assert.Equal("Welcome guest", greeting);
    }

    [Fact]
    public void MatchDo()
    {
        // ...Do for the side effect!
        // bad example for testing.
        // for instance, might be handy to log an error when None, or log info when Some.
        string greeting = string.Empty;
        Opt<string> name = Some("Merlin");
        name.MatchDo(
            whenSome: name => greeting = $"Welcome {name}",
            whenNone: () => greeting = "Welcome guest");
        Assert.Equal("Welcome Merlin", greeting);

        // similarly the following individual variants exist
        name.Do(name => greeting = $"Welcome {name}");
        name.DoIfNone(() => greeting = "Welcome guest");
        Assert.Equal("Welcome Merlin", greeting);
    }

    [Fact]
    public void MapBasic()
    {
        // string -> int
        static int CountLetterA(string name)
            => name.Count(c => c == 'a' || c == 'A');

        Opt<string> starName = Some("Aldebaran");
        Opt<int> countA = starName.Map(CountLetterA);
        Assert.Equal(countA, Some(3));

        starName = None<string>();
        countA = starName.Map(CountLetterA);
        Assert.Equal(countA, None<int>());

        // alternatively
        countA = starName.Map(name => CountLetterA(name)); // or
        countA = starName.Map(name => name.Count(c => c == 'a' || c == 'A')); // or
    }

    [Fact]
    public void FlowWithMap()
    {
        // task:
        // * maybe get user by its username
        // * get whether it is authenticated or not
        // * get the role associated with the user if authenticated
        // * get views that the role is authorized to see

        // setup: available functions.
        static Opt<User> GetUser(string username)
            => username == "Pixie" ? Some(new User("Pixie")) : None<User>();
        static bool IsAuthenticated(User user)
            => true;
        static Role GetRole(User user, bool isAuthenticated)
            => isAuthenticated ? new Role("Admin") : new Role("Guest");
        static View[] GetAuthorizedViews(Role role)
        {
            if (role.RoleName == "Guest")
                return new View[] { new View("Home") };
            else
                return new View[] { new View("Home"), new View("Account") };
        }

        // just map
        var views = GetUser("Pixie")
            .Map(user => (user, IsAuthenticated(user)))
            .Map(x => GetRole(x.user, x.Item2))
            .Map(GetAuthorizedViews);
        Assert.True(views.IsSome);
        Assert.Equal(views.Unwrap(), new View[] { new("Home"), new("Account") });

        views = GetUser("Witch")
            .Map(user => (user, IsAuthenticated(user)))
            .Map(x => GetRole(x.user, x.Item2))
            .Map(GetAuthorizedViews);
        Assert.True(views.IsNone);

        // just map - local func
        Func<string, Opt<View[]>> getViewsForUser = username =>
        {
            return GetUser(username)
                .Map(user => (user, IsAuthenticated(user)))
                .Map(x => GetRole(x.user, x.Item2))
                .Map(GetAuthorizedViews);
        };
        Assert.Equal(getViewsForUser("Pixie").Unwrap(), new View[] { new("Home"), new("Account") });
        Assert.True(getViewsForUser("Witch").IsNone);

        // map with cached fun's to avoid manual tuples
        var isAuth = IsAuthenticated;
        getViewsForUser = username =>
        {
            return GetUser(username)        // Opt<User>
                .Map(isAuth.Cached())       // Opt<(User, bool)>
                .Map(GetRole)               // Opt<Role>
                .Map(GetAuthorizedViews);   // Opt<View[]>
        };
        Assert.Equal(getViewsForUser("Pixie").Unwrap(), new View[] { new("Home"), new("Account") });
        Assert.True(getViewsForUser("Witch").IsNone);

        // same but point-free!
        var getUser = GetUser;
        getViewsForUser = getUser.Map(isAuth.Cached()).Map(GetRole).Map(GetAuthorizedViews);

        Assert.Equal(getViewsForUser("Pixie").Unwrap(), new View[] { new("Home"), new("Account") });
        Assert.True(getViewsForUser("Witch").IsNone);
    }

    [Fact]
    public void FlatMap()
    {
        // setup
        static Opt<User> QueryUser(string username)
            => SomeIf<User>(username == "jdoe" || username == "foo", () => new User(username));
        static Opt<string> GetMiddleName(User user)
            => user.UserName == "jdoe" ? None<string>() : Some("Middle");

        // much more nested than desired; FlatMap to automatically bypass the None track
        Opt<Opt<string>> _ = QueryUser("jdoe").Map(GetMiddleName);

        Opt<string> nobodyMiddle = QueryUser("nobody").FlatMap(GetMiddleName);
        Assert.True(nobodyMiddle.IsNone);

        Opt<string> jdoeMiddle = QueryUser("jdoe").FlatMap(GetMiddleName);
        Assert.True(jdoeMiddle.IsNone);

        Opt<string> fooMiddle = QueryUser("foo").FlatMap(GetMiddleName);
        Assert.Equal(Some("Middle"), fooMiddle);
    }

    [Fact]
    public void Flatten()
    {
        // just a backup method to rescue when we make a naive mistake to end up with Opt<Opt<T>>.
        // see FlatMap & Compose in above test to avoid this.

        // setup
        static Opt<User> QueryUser(string username)
            => SomeIf<User>(username == "jdoe" || username == "foo", () => new User(username));
        static Opt<string> GetMiddleName(User user)
            => user.UserName == "jdoe" ? None<string>() : Some("Middle");

        Opt<Opt<string>> tooNested = QueryUser("nobody").Map(GetMiddleName);
        Opt<string> nobodyMiddle = tooNested.Flatten();
        Assert.True(nobodyMiddle.IsNone);

        tooNested = QueryUser("jdoe").Map(GetMiddleName);
        Opt<string> jdoeMiddle = tooNested.Flatten();
        Assert.True(jdoeMiddle.IsNone);

        tooNested = QueryUser("foo").Map(GetMiddleName);
        Opt<string> fooMiddle = tooNested.Flatten();
        Assert.Equal(Some("Middle"), fooMiddle);
    }

    [Fact]
    public void FlowWithFlatMap()
    {
        // setup
        static Opt<string> GetUsername(string userEntry)
            => SomeIf<string>(!string.IsNullOrEmpty(userEntry), userEntry);
        static bool ValidUsername(string userName)
            => userName.Length >= 3;
        static Opt<User> QueryUser(string username)
            => SomeIf<User>(username == "jdoe" || username == "foo", () => new User(username));
        static Role GetRoleOf(User user)
            => new Role("Admin");

        // tests
        static void TestFun(Func<string, Opt<Role>> getRole)
        {
            var leadingToNone = new string[]
            {
                "",     // fails GetUsername's internal check
                "x",    // fails ValidUsername check
                "bar",  // QueryUser returns None
            };
            Assert.True(leadingToNone.Select(entry => getRole(entry)).All(role => role.IsNone));

            var role = getRole("jdoe");
            Assert.Equal(Some(new Role("Admin")), role);
        }

        // transform using opt methods, almost pointfree
        var getRole = (string userEntry) =>
        {
            return GetUsername(userEntry)
                .SomeIf(ValidUsername)
                .FlatMap(QueryUser)
                .Map(GetRoleOf);
        };
        TestFun(getRole);


        // transform using composition method, pointfree
        var getUsername = GetUsername;
        var pntfreeGetRole = getUsername.SomeIf(ValidUsername).FlatMap(QueryUser).Map(GetRoleOf);
        TestFun(pntfreeGetRole);
    }

    [Fact]
    public void FlowWithFlatMapAndCached()
    {
        // setup
        static Opt<string> GetUsername(string userEntry)
            => SomeIf<string>(!string.IsNullOrEmpty(userEntry), userEntry);
        static bool ValidUsername(string userName)
            => userName.Length >= 3;
        static Opt<User> QueryUser(string username)
            => SomeIf<User>(username == "jdoe" || username == "foo", () => new User(username));
        static Role GetRoleOf(User user)
            => new Role("Admin");
        static string Greeting(User user, Role role)
            => $"{user.UserName}, welcome as {role.RoleName}!";

        // tests
        static void TestFun(Func<string, Opt<string>> getGreeting)
        {
            var leadingToNone = new string[]
            {
                "",     // fails GetUsername's internal check
                "x",    // fails ValidUsername check
                "bar",  // QueryUser returns None
            };
            Assert.True(leadingToNone.Select(entry => getGreeting(entry)).All(role => role.IsNone));

            var role = getGreeting("jdoe");
            Assert.Equal(Some("jdoe, welcome as Admin!"), role);
        }


        // transform using opt methods, almost pointfree
        var getRoleOf = GetRoleOf;
        var getGreeting = (string userEntry) =>
        {
            return GetUsername(userEntry)
                .SomeIf(ValidUsername)
                .FlatMap(QueryUser)
                .Map(getRoleOf.Cached())
                .Map(Greeting);
        };
        TestFun(getGreeting);

        // transform using composition method, pointfree
        var getUsername = GetUsername;
        var pntfreeGetGreeting = getUsername
            .SomeIf(ValidUsername)
            .FlatMap(QueryUser)
            .Map(getRoleOf.Cached())
            .Map(Greeting);
        TestFun(pntfreeGetGreeting);
    }

    [Fact]
    public void LogicalAnd()
    {
        var combined = Some(12).And(Some(true));
        Assert.Equal(Some((12, true)), combined);

        combined = Some(12).And(None<bool>());
        Assert.True(combined.IsNone);

        combined = None<int>().And(Some(true));
        Assert.True(combined.IsNone);

        combined = None<int>().And(None<bool>());
        Assert.True(combined.IsNone);
    }
}
