# Orx.Fun.Option

An option type for C# aiming to be explicit while concise.

Complete auto-generated documentation can be found here:
**[sandcastle-documentation](https://orxfun.github.io/orx-fun-option/index.html)**.

## Why?

1. Optional (maybe) values are necessary.
    * Explicit optional arguments
    * Explicit optional returns
    * Continuations without overwhelming null/none checks
2. Built-in nullables do not seem to satisfy the requirements.


**Explicit Optional Arguments**

```csharp
static void Solve(Instance instance, Opt<SolverParameters> parameters = default)
{
	// solve with the parameters if provided;
	// with default parameters otherwise.
}

// caller knows that it is okay not to provide parameters
Solve(instance);
Solve(instance, None<SolverParameters>());

// or solve with parameters
SolverParameters pars = new()
{
	TimeLimit = 60,
};
Solve(instance, Some(pars));
```

**Explicit Optional Returns**

An easy example to demonstrate this is the linq method First:

```csharp
var numbers = new int[] { 1, 3, 5, 7 };
int firstEven = numbers.First(x => x % 2 == 0); // throws!!!
```

First method actually does not (cannot always) return an int. FirstOrDefault certainly does not fix the issue. On the other hand, solution is easy with optionals:
```csharp
var numbers = new int[] { 1, 3, 5, 7 };
Opt<int> firstEven = numbers.FirstOrNone(x => x % 2 == 0);
```

**Continuations**

The following dummy example illustrates the use of Map and FlatMap to enable continuations.

```csharp
/// Returns Some of the user if there exists one with the given id; None otherwise
static Opt<User> GetUserById(int id)
{
	// ...
}

// task to achieve:
// 1. get the first even number
// 2. halve it
// 3. get the user with the number as id, if exists
// 4. return the users's name

var numbers = new int[] { 1, 3, 5, 7 };
Opt<string> name = numbers.FirstOrNone(x => x % 2 == 0)
    .Map(x => x / 2)
    .FlatMap(GetUserById)
    .Map(user => user.Name);


```
