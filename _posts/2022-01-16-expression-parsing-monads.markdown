---
layout: post
author: tristan-rhodes
title: Expression Parsing - Functional Edition
excerpt: Magic with Monads (and Functors)
featured-image: /assets/expression-parsing-monads/featured-image.jpg
---

### Previously

In my [previous post](/2022/01/07/expression-parsing.html), we created a basic set of object orientated parser implementations to handle converting some simple expressions.

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

### In Action

![Pickup Dropoff Tests](/assets/expression-parsing/pickup-dropoff-tests.PNG)

### The System

The system is two phase, with an initial Lexer/Tokeniser that splits a string into identified parts, and a combinatorial parser system that matches a complex pattern and converts this into a higher order object. We've been following an Object Orientated style so far with interfaces and implementation classes. 

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

### Going Functional

Now we're going to drop the interface implementations, make them all delegates and take an entirely functional approach.

```csharp
public delegate TokenisationResult TokenParser(string token);
public delegate ParseResult<T> Parser<T>(Position position);
```

### Disclaimer

While I use functional programming regularly, I don't come from a functional background, so I may be butchering some terms. I've tried to be as accurate as possible, but the concepts feel kind of fuzzy and share many similar attributes with each other, so I may blur the boundaries a bit.

### Main Players

There are a couple of concepts that we need words for in order to properly describe whats going on. 

* [Functors](https://en.wikipedia.org/wiki/Functor_(functional_programming))
* [Monads](https://en.wikipedia.org/wiki/Monad_(functional_programming))
* [Combinators / Combinatory Logic](https://en.wikipedia.org/wiki/Combinatory_logic)

Now, if you click on any of those links, you'll be confronted with a lot of mathsy looking stuff that's heavy on symbols and steeped in category theory. Monads also seem to have this logical fallacy around them that once you understand Monads, you cannot explain them to people who don't. But I think I'm safe, because I still don't understand them...

### Functor
Wikipedia: 
> In functional programming, a functor is a design pattern inspired by the definition from category theory, that allows for a generic type to apply a function inside without changing the structure of the generic type.

Me: 
> A function that turns one thing into a another thing.

### Combinator
Wikipedia: 
> A combinator is a higher-order function that uses only function application and earlier defined combinators to define a result from its arguments.

Me: 
> One or more functions used to generate a result inside another function.

### Monads
Wikipedia:
> In functional programming, a monad is a type that wraps another type and gives some form of quality to the underlying type. In addition to wrapping a type, monads define two functions: one to wrap a value in a monad, and another to compose together functions that output monads (these are known as monadic functions).

Me:
> A function that you wrap in another function of the same type to decorate / extend its behavior.

Final Note: All monads are functors, but not all functors are monads.

### Functional Flow

<p align="center"><iframe src="https://giphy.com/embed/0Av9l0VIc01y1isrDw" width="480" height="360" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/drinkdroplet-hero-captain-planet-savetheplanet-0Av9l0VIc01y1isrDw">via GIPHY</a></p></p>

Let's start with two functions, baseValue() which returns 0, and add(), which is a function that runs baseValue() and returns the result + 1.

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

    // Run the add function, returning the result of basevalue() + 1
    add()
        .Should().Be(1);
}
```

When we run add, we get 1 added to the result of another function. Not too useful, but both the baseValue() and add() are Func&lt;int>, meaning they are functions that take no parameters and return an int.

This means that, as they have the same function footprint, we can apply a combinator pattern and put one Func&lt;int> inside another Func&lt;int>, and what we have is still a single Func&lt;int> that acts as a bridge between the other two.

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

I believe this is now a fully qualified Monad. We have a bridge that can nest and chain our functions and total the result, but we need to be able to do more than just add 1, we need a value parameter. 

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

Now let's say we want to take our number and turn it into a monetary string, so what we want is a Func&lt;string> that is going to be wrapping/converting a Func&lt;int>, this is where we're going to apply our combinator pattern against a functor. (I believe that while a Monad always chains functions, a Functor is just a converter of types, and those types _can_ be functions.)

```csharp
[Fact]
public void MonadNestingFunctorTest()
{
    Func<int> baseValue = () => 0;

    // A Function that takes a value and a function and returns a function that 
    // adds the value to the result of the function.
    Func<int, Func<int>, Func<int>> add =
        (int value, Func<int> baseValue) =>
            () => baseValue() + value;

    // Function that takes a Func<int> and returns a Func<string> that runs the Func<int>,
    // turns the result into a string and prefixes it with the &pound; symbol
    Func<Func<int>, Func<string>> asGBPString =
        (Func<int> baseValue) =>
            () => $"&pound;" + baseValue();

    // A function returning a baseValue, inside an add(1), inside an add(2), 
    // inside an add(3), inside an asString()
    Func<string> price =
        asGBPString(
            add(3,
                add(2,
                    add(1,
                        baseValue))));

    price()
        .Should().Be("&pound;6");
}
```

So now we're composing nested additions with our values and converting the result into a formatted string, but the configuration is still fiddly and unintuitive. We can tidy this up with some fluent helpers.

```csharp
public static class Beginning
{
    public static Func<int> With(int value) =>
        () => value;
}

public static class MonadExtensions
{
    public static Func<int> Add(this Func<int> baseValue, int value) =>
        () => baseValue() + value;

    public static Func<string> AsGBPString(this Func<int> baseValue) =>
        () => $"&pound;" + baseValue();
}
```

And our updated code makes our nesting come out as a flat chain:

```csharp
[Fact]
public void MonadChainFluentTest()
{
    Func<string> price =
        Beginning
            .With(0)
            .Add(1)
            .Add(2)
            .Add(3)
            .AsGBPString();

    price()
        .Should().Be("&pound;6");
}
```

Much cleaner. So the equation here is

> Functions + Fluent API = Magic

### Wait a second
If this feels familiar to any C# developers, it should, because this pattern is the backbone of Linq and the Lambda based composable query system. Linq extends and chains IEnumerable, and in my last post we were chaining functions that returned IParsers and using extension methods on the interface. This time, we don't have an interface, so we're putting _extension methods onto a Delegate_. 

<p align="center"><iframe src="https://giphy.com/embed/xT0xeJpnrWC4XWblEk" width="480" height="320" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/whoa-hd-tim-and-eric-xT0xeJpnrWC4XWblEk">via GIPHY</a></p></p>

### Putting it together

But what does this mean, and why is it useful to us?

Let's go back to OO thinking and start with one of our original IParser<T> implementations, IsToken:

```csharp
public class IsToken<T> : IParser<T>
{
    public ParseResult<T> Parse(Position position)
    {
        return position.Current.Is<T>() ?
            ParseResult<T>.Successful(position.Next(), position.Current.As<T>()) :
            ParseResult<T>.Failure(position);
    }
}
```

What if we want to add a condition? We've got our OO hat on, so let's create a base class!

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

Now we've got a base class we can use to implement conditions, let's create a check for ints over a certain value.

```csharp
public class IsIntegerOver : IsTokenConditionBase<int>
{
    int _value;

    public IsIntegerOver(int value) =>
        _value = value;

    public override bool check(int entity) =>
        entity > _value;
}
```

By now, your spider senses should be tingling. This route is the route to great pain, we're going to be creating a _looooot_ of base classes and classes that use configurations of them and it feels like a trap. What about if we make it flexible by supplying our check as a delegate?

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

What we've just done here to make it more flexible is to put a function inside an object. Going back to earlier in this post, we talk about the Interface being replaceable with a Function, if we make that change now, we will instead be putting a function inside a function, and that brings functional composition of functors and monads into reach.

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

And now we can implement each complete parser in a declarative static property, no interfaces, no classes, no inheritance, no new().
        
```csharp
[Theory]
[InlineData("1", false)]
[InlineData("10", false)]
[InlineData("101", true)]
[InlineData("1000", true)]
public void DelegateParser(string text, bool succeed)
{
    var tokens = Tokenise(text);
    Parsers
        .IsToken<int>(i => i > 100)
        .Parse(tokens)
        .Success
        .Should().Be(succeed);
}
```

### Functional Parsers

I've migrated all the original OO implementations of the [Parsers](https://github.com/TristanRhodes/TextProcessing/blob/master/TextProcessing/Functional/Parsers/Parsers.cs) and [Token Parsers](https://github.com/TristanRhodes/TextProcessing/blob/master/TextProcessing/Functional/Tokenisers/TokenParsers.cs).

There are a couple of interesting additions worth noting. This FromRegex() method can be used to fluently build a Parser by combining a regex pattern and a function that processes the successful Regex Match.

```csharp
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

This enables us to separate the Regex match step from the actual processing of the successful match, and use them together via a combinator.

```csharp
public static TokenParser JoiningWord = Tokeniser
    .FromRegex(@"^[Tt]o$", match =>
        TokenisationResult.Success(new JoiningWord()));
```

### Monad Parsers vs Functor Parsers

If we have a closer look at the original Parser implementations, we'll see that they aren't all specifically Monads or Functors. I'm not confident enough to accurately categorize them, but have a think about where they sit relative to the functional concepts described above:

* Is - Parser&lt;T&gt; IsToken&lt;T&gt;()
* Then - Parser&lt;U&gt; Then&lt;T, U&gt;(Parser&lt;T&gt; first, Func&lt;T, Parser&lt;U&gt;&gt; second)
* Or - Parser&lt;T&gt; Or&lt;T&gt;(params Parser&lt;T&gt;[] children)
* Select - Parser&lt;U&gt; Select&lt;T, U&gt;(Parser&lt;T&gt; child, Func&lt;T, U&gt; converter)
* ListOf - Parser&lt;List&lt;T&gt;&gt; ListOf&lt;T&gt;(Parser&lt;T&gt; child)
* End - Parser&lt;T&gt; End&lt;T&gt;(Parser&lt;T&gt; child)

### Functionally Parsing DayTime

Now we can go back to the starting scenario from the previous post which was initially solved with the OO approach:

```csharp
public static IParser<DayTime> DayTimeParser =
    new Then<DayOfWeek, DayTime>(
        new IsToken<DayOfWeek>(),
        dow => new Select<LocalTime, DayTime>(
            new IsToken<LocalTime>(),
            lt => new DayTime { Day = dow, LocalTime = lt }));
```

This can now be assembled with functions:

```csharp
public static Parser<DayTime> WeekDayTimeParser =
    Parsers.Then(
        Parsers.IsToken<DayOfWeek>(),
        dow => Parsers.Select(
            Parsers.IsToken<LocalTime>(),
            lt => new DayTime { Day = dow, LocalTime = lt }));
```

And we can apply our fluent API on top of this:

```csharp
public static Parser<DayTime> DayTimeFluentParser =
    Parsers.IsToken<DayOfWeek>().Then(dow =>
        Parsers.IsToken<LocalTime>().Select(lt =>
            new DayTime { Day = dow, LocalTime = lt }));
```

And now we can do the same for all of the other [combinations of parsers](https://github.com/TristanRhodes/TextProcessing/blob/master/TextProcessing/Functional/Parsers/ExpressionParsers.cs) that we have, pure functions and functional combinators.

### Code

The code is all available [here](https://github.com/TristanRhodes/TextProcessing) where I put together a bunch of different ways to do parsing. We've just covered the functional part, so check out the [Functional folder](https://github.com/TristanRhodes/TextProcessing/tree/master/TextProcessing/Functional).

### Note

It's interesting to note that when you apply the fluent API, both the OO and Functional code is now identical on the surface. The OO pattern is still packaging object => delegate => object => delegate, but at least it's tidy. In the new functional implementation it's just delegates all the way.

The reason they are so similar is that I cheated in my last post and the "Object Orientated Approach" is actually a hybrid, but I find it helps bridge the gap between the OO and functional concepts.

### Credits
Header Image by <a href="https://pixabay.com/users/jackmac34-483877/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=970943">jacqueline macou</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=970943">Pixabay</a>
  