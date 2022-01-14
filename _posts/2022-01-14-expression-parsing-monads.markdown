---
layout: post
author: tristan-rhodes
title: Expression Parsing - Monad Edition
excerpt: Magic with Monads
featured-image: /assets/expression-parsing-monads/featured-image.jpg
---

### Previously

Starting with a lexer system to split a string into a feed of typed Tokens using an ITokeniser interface. We then created a couple of basic parsers out of an IParser<T> interface to process the resulting token arrays.

```csharp
// Converts words into typed concepts
public interface ITokenParser
{
    TokenisationResult Tokenise(string token);
}

// Converts the array of typed objects into higher order objects
public interface IParser<T>
{
    ParseResult<T> Parse(Position position);
}

public static class ParserExtensions
{
    // Extension method to enable us to enter into the IParser interface with just a Token array.
    public static ParseResult<T> Parse<T>(this Parser<T> parser, Token[] tokens) =>
        parser(Position.For(tokens));
}
```

#### Implementations
* Is
* Then
* Or
* Select
* ListOf
* End

Finally we combined these parsers to convert a couple of different patterns into objects:

```csharp
// Separate two part element context => DayTime range
"Pickup Mon 08:00 dropoff wed 17:00"

// Range elements with different separators => Open Days range and Hours Range
"Open Mon to Fri 08:00 - 18:00"

// Repeating tokens => List of tour times
"Tours 10:00 12:00 14:00 17:00 20:00"

// Repeating complex elements => List of event day times
"Events Tuesday 18:00 Wednesday 15:00 Friday 12:00"
```

Now we're going to drop the interface implementations, make them all delegates and take an entirely functional approach. This leads us down the path to Monads.

```csharp
public delegate TokenisationResult TokenParser(string token);
public delegate ParseResult<T> Parser<T>(Position position);
```

### Monads

So, what are monads? Before we start on that, Monads seem to have this logical fallacy around them that once you understand Monads, you cannot explain them to people who don't. But I think I'm safe, because I still don't understand Monads, so here we go...

In the context of functional programming, a Monad is a function that wraps another function, and passes the result of the nested function back through the context of the wrapper. The function that is wrapped has access to all the contexts that wrap it, like a Russian doll knowing everything about its parents, and nothing about its children. They are simple like fractals, but fractals can create incredibly complex results.

The bit I always find myself a bit fuzzy on is what is the exact boundry where something becomes a monad / no-longer a monad. Anyway, let's start with a nested function.

```csharp
[Fact]
public void BasicNestedFunctionTest()
{
    // Function that returns a starting value of 0
    Func<int> baseValue = () => 0;

    // Function that returns a function that adds 1
    // to the result of the basevalue function.
    Func<int> add = () =>
        baseValue() + 1;

    // Run the add function, returning the result of basevalue, + 1
    add()
        .Should().Be(1);
}
```

Ok, this adds 1 to the result of another function. Not too useful, but both the 
baseValue() and add() are Func&lt;int>, meaning they take no parameters and return an int.

This means that we can put one Func&lt;int> inside another Func&lt;int>, and what we have is still a single Func&lt;int> that acts as a bridge between the other two.

```csharp
[Fact]
public void NestedFunctionTest()
{
    Func<int> baseValue = () => 0;

    // A Function that takes a function and returns a function that adds 1 to 
    // the result.
    Func<Func<int>, Func<int>> add = (Func<int> baseValue) =>
        () => baseValue() + 1;

    // A function returning a baseValue() inside an Add(), inside an Add()
    Func<int> result = 
        add(
            add(
                baseValue));

    // Because we ran add() twice, we get a value of 2.
    result()
        .Should().Be(2);
}
```

Now we can start nesting functions and accumulating the result, but we need to be able to do more than just add 1, we need a value parameter. 

```csharp
[Fact]
public void MonadNestingTest()
{
    Func<int> baseValue = () => 0;

    // A Function that takes value and a function and returns a function that 
    // adds the value to the result of the function.
    Func<int, Func<int>, Func<int>> add =
        (int value, Func<int> baseValue) =>
            () => baseValue() + value;

    // A function returning a baseValue, inside an add(1), inside an add(2), 
    // inside an add(3)
    Func<int> result =
        add(3,
            add(2,
                add(1,
                    baseValue)));

    result()
        .Should().Be(6);
}
```

Ok, so now we're composing nested sets of our supplied values. I believe this is now a fully qualified monad, but the configuration is still fiddly and unintuitive. We can tidy this up with some fluent helpers.

```csharp
public static class Beginning
{
    // Static method that returns a function that returns the supplied value. 
    public static Func<int> With(int value) =>
        () => value;
}

public static class MonadExtensions
{ 
    // An extension method on a delegate function that takes a 
    // value and returns a function that adds the value to the 
    // result of the base function.
    public static Func<int> Add(this Func<int> baseValue, int value) =>
        () => baseValue() + value;
}
```

And our updated code looks like this:

```csharp
[Fact]
public void MonadChainFluentTest()
{
    Func<int> total =
        Beginning
            .With(0)
            .Add(1)
            .Add(2)
            .Add(3);

    total()
        .Should().Be(6);
}
```

Much cleaner. So the equation here is

> Monads + Fluent API = Magic

### Wait a second
If you read my last post, this will feel familiar, and it should do, because we were chaining functions that returned IParsers and using extension methods on the interface. This time, we don't have an interface, so we're putting _extension methods onto a Delegate_. 

<p align="center"><iframe src="https://giphy.com/embed/xT0xeJpnrWC4XWblEk" width="480" height="320" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/whoa-hd-tim-and-eric-xT0xeJpnrWC4XWblEk">via GIPHY</a></p></p>

### Putting it together

But what does this mean, and why is it useful to us?

Let's start with one of our original Token Parsers:

```csharp
public class JoiningWordTokenParser : ITokenParser
{
    Regex regex = new Regex(@"^[Tt]o$");

    public TokenisationResult Tokenise(string token)
    {
        return regex.IsMatch(token) ? 
            TokenisationResult.Success(new JoiningWord()) :
            TokenisationResult.Fail();
    }
}
```

This is just one of many parsers that use Regex, so it would make sense to make something that we can re-use that handles our Regex logic. We've got our OO hat on, so let's make a base class!

```csharp
public abstract class BaseRegexTokenParser : ITokenParser
{
    Regex regex;
        
    public BaseRegexTokenParser(string pattern) =>
        regex = new Regex(pattern);

    public TokenisationResult Tokenise(string token)
    {
        var match = regex.Match(token);
        return match.Success ?
            convertMatch(match) :
            TokenisationResult.Fail();
    }

    protected abstract TokenisationResult convertMatch(Match match);
}
```

Ok, great, now we have a base class parser that runs a regex check, and on failure breaks out, and on success runs the convert method. We can now implement our individual parsers that use regex over that, and we only need to implement our convertMatch function!

```csharp
public class RegexJoiningWordParser : BaseRegexTokenParser
{
    public RegexJoiningWordParser() 
        : base(@"^[Tt]o$") { }

    protected override TokenisationResult convertMatch(Match match) =>
        TokenisationResult.Success(new JoiningWord());
}
```

By now, your spider senses should be tingling. This route is the route of great pain, we're going to be creating a _looooot_ of classes that use this base class, and it feels like a trap. What about if we make it flexible by supplying a converter delegate?

```csharp
public class FlexibleRegexTokenParser : ITokenParser
{
    Regex _regex;
    Func<Match, TokenisationResult> _converter;

    // Takes a pattern and a delegate that converts a Regex.Match into a TokenisationResult.
    public FlexibleRegexTokenParser(string pattern, Func<Match, TokenisationResult> converter)
    {
        _regex = new Regex(pattern);
        _converter = converter;
    }

    public TokenisationResult Tokenise(string token)
    {
        var match = _regex.Match(token);
        return match.Success ?
            _converter(match) :
            TokenisationResult.Fail();
    }
}
```

We don't need to make a new implementation each time, we've got something configurable.

```csharp
[Theory]
[InlineData("To")]
[InlineData("to")]
public void FlexibleRegexTokenParser(string value)
{
    var parser = new FlexibleRegexTokenParser(
        @"^[Tt]o$", 
        match => TokenisationResult.Success(new JoiningWord()));

    parser.IsMatch(value)
        .Should().BeTrue();
}
```

What we are doing now is using a delegate in the constructor to handle success, and if we think back to the beginning of the post, we talk about the ITokenParser being replacable with a delegate. So if we turn this interface into a delegate, and put the origional check inside a Regex check delegate...

```csharp
public delegate TokenisationResult TokenParser(string token);

public static TokenParser FromRegex(string pattern, Func<Match, TokenisationResult> resolver)
{
    var regex = new Regex(pattern);
    return (string token) =>
    {
        var match = regex.Match(token);

        if (!match.Success)
            return TokenisationResult.Fail();

        return resolver(match);
    };
}
```

We can implement our whole parser in a static property, by calling a static method, no classes, no interfaces:

```csharp
public static TokenParser JoiningWord = Tokeniser
    .FromRegex(@"^[Tt]o$", match =>
        TokenisationResult.Success(new JoiningWord()));
```

### Well it's neat and all

But what about the origional Monad composition? Well, the Tokenisation system is flat, the main benefit of the changes so far are that we don't have to implement any classes or interfaces and we've accepted functions as first class concepts. 
What happens when we apply this to the IParser<T> implementations?


```csharp
public class IsToken<T> : IParser<T>
{
    public IsToken() { }

    public ParseResult<T> Parse(Position position)
    {
        return position.Current.Is<T>() ?
            ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
            ParseResult<T>.Failure(position);
    }
}
```

What if we want to add a condition? We've got our OO hat on, so let's add a base class!

```csharp
public abstract class IsTokenConditionBase<T> : IParser<T>
{
    public ParseResult<T> Parse(Position position)
    {
        return position.Current.Is<T>() && check(position.Current.As<T>()) ?
            ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
            ParseResult<T>.Failure(position);
    }

    public abstract bool check(T entity);
}
```

Now we've got a base class we can use to implement conditions, let's see what we can do with it.

```csharp
public class NumberOverParser : IsTokenConditionBase<int>
{
    int _value;

    public NumberOverParser(int value) =>
        _value = value;

    public override bool check(int entity) =>
        entity > _value;
}
```

By now, your spider senses should be tingling. This route is the route of great pain, we're going to be creating a _looooot_ of classes that use this base class, and it feels like a trap. What about if we make it flexible by supplying our check as a delegate?

```csharp
public class FlexibleIsTokenCondition<T> : IParser<T>
{
    Func<T, bool> _check;

    public FlexibleIsTokenCondition(Func<T, bool> check) =>
        _check = check;

    public ParseResult<T> Parse(Position position)
    {
        return position.Current.Is<T>() && _check(position.Current.As<T>()) ?
            ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
            ParseResult<T>.Failure(position);
    }
}
```

We don't need to make a new implementation each time, we've got something configurable. We can run that in a test, and it all works.

```csharp
[Theory]
[InlineData("1", false)]
[InlineData("10", false)]
[InlineData("101", true)]
[InlineData("1000", true)]
public void FlexibleParser(string text, bool succeed)
{
    var tokens = Tokenise(text);
    new FlexibleIsTokenCondition<int>(i => i > 100)
        .Parse(tokens)
        .Success
        .Should().Be(succeed);
}
```

What we've just done here to make it more flexible is to put a function inside an object. But if we go back to earlier in this post, we talk about the Interfaces being replacable with a Function, if we make that change now, we will instead be putting a function inside a function, and that Monad composition into reach.

Then we can implement each complete parser in a static method, no classes, no interfaces:

```csharp
public delegate ParseResult<T> Parser<T>(Position position);

public static Parser<T> IsToken<T>() => (Position position) =>
{
    return position.Current.Is<T>() ? 
        ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
        ParseResult<T>.Failure(position);
};

public static Parser<T> IsToken<T>(Func<T, bool> check) => (Position position) =>
{
    return position.Current.Is<T>() && _check(position.Current.As<T>()) ?
        ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
        ParseResult<T>.Failure(position);
};
```

### Functional Parsers

TODO: Stuff

```csharp
public static Parser<DayTime> WeekDayTimeParser =
    Parsers.Then(
        Parsers.IsToken<DayOfWeek>(),
        dow => Parsers.Select(
            Parsers.IsToken<LocalTime>(),
            lt => new DayTime { Day = dow, LocalTime = lt }));
```

### Credits
Header Image by <a href="https://pixabay.com/users/jackmac34-483877/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=970943">jacqueline macou</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=970943">Pixabay</a>
  