---
layout: post
author: tristan-rhodes
title: Expression Parsing
excerpt: Turning bigger strings, into complex things.
featured-image: /assets/expression-parsing/featured-image.jpg
---

In my [last post](/2021/12/19/tokenisation.html), I talked about splitting up sentences of text and using Regex to turn a longer string into an array of objects representing fragments of a sentence. We also created a basic expression parser to convert a sentence containing two parts, a Day and a Time, into a single DayTime object. On top of this, we considered the following scenarios:

```csharp
// Separate two part element context
"Pickup Mon 08:00 dropoff wed 17:00"

// Range elements with different separators
"Open Mon to Fri 08:00 - 18:00"

// Repeating tokens
"Tours 10:00 12:00 14:00 17:00 20:00"

// Repeating complex elements
"Events Tuesday 18:00 Wednesday 15:00 Friday 12:00"
```

## Expression Trees

Selecting exactly two values into a single object is simple, but when things become combinations of conditional, repetitive or optional rules, the patterns cannot be easily configured in a single phase pattern match. We need a better understanding of the pattern structure in order to begin to solve it with code. For this, we are going to need to define our sentences as expression trees, with the root being the complete concept we are parsing.

You read an expression tree from left leaf to right leaf, with each branch node being a syntax rule and each leaf element being a token extracted from the original sentence. We can start by describing our original parsing subject, DayTime:

![Parsed DayTime](/assets/expression-parsing/expression-tree-daytime.png)

This is simple enough, but our sentences are more complex concepts than this, a higher order object can be composed of other higher order concepts, and what is a root for some scenarios may be a child for others. Consider the expression trees for the scenarios above:

<table width="100%">
<tr>
    <th width="25%">Pickup / Dropoff</th>
    <th width="25%">Range seperators</th>
    <th width="25%">Repeating Tokens</th>
    <th width="25%">Repeating Complex Children</th>
</tr>
  <tr>
  <td><a class="example-image-link" href="/assets/expression-parsing/expression-tree-pickup-drop-off.png" data-lightbox="example-1"><img class="example-image" src="/assets/expression-parsing/expression-tree-pickup-drop-off.png" height=100 width=100 alt=""></a></td>
    <td><a class="example-image-link" href="/assets/expression-parsing/expression-tree-ranges.png" data-lightbox="example-1"><img class="example-image" src="/assets/expression-parsing/expression-tree-ranges.png" height=100 width=100 alt=""></a></td>
    <td><a class="example-image-link" href="/assets/expression-parsing/expression-tree-repeating-token.png" data-lightbox="example-1"><img class="example-image" src="/assets/expression-parsing/expression-tree-repeating-token.png" height=100 width=100 alt=""></a></td>
    <td><a class="example-image-link" href="/assets/expression-parsing/expression-tree-repeating-complex.png" data-lightbox="example-1"><img class="example-image" src="/assets/expression-parsing/expression-tree-repeating-complex.png" height=100 width=100 alt=""></a></td>
  </tr>
</table>

### Basic Parsers

In order to begin to process the above scenarios, we're going to need to create some parsers to handle our expression rules, let's stick with those needed just for handling DayTime for now:

* IsToken - A parser that matches a single token. (These would be the blue Token nodes in the tree)
* Then - A parser that matches two consecutive parsers together in a chain.
* Select - A parser that converts the result of one Parser into a different object type. (These would be the purple higher order nodes in the tree)

### Parser Chaining

How do you start to combine these together? We are in a situation where we have a list of tokens we want to provide to a parser, but we also want to put a parser inside another parser and do some magic... We're missing some key concepts. Let's go back to our naive parser implementation:

```csharp
public interface IParser<T>
{
    ParseResult<T> Parse(Token[] tokens);
}

public class ParseResult<T>
{
    public bool Success { get; init; }

    public T Value { get; init; }
}
```

We can't really chain this. Our input is always a Token[] array, and our output is always a result of processing the complete input. But what if an instance of a parser only consumed the tokens that were relevant to itself and its children, and returned the remaining tail? How would we do that?

We can create a Position object that encapsulates our Tokens and Ordinal:

```csharp
public class Position
{
    public Position(Token[] source, int ordinal)
    {
        Source = source;
        Ordinal = ordinal;
    }

    public Token[] Source { get; }

    public int Ordinal { get; }

    public Token Current => !End ? Source[Ordinal] : throw new ApplicationException("At End");

    public bool End => (Ordinal == Source.Length);
}
```
From here, if we change the input to Position and we add a Position to the response, then when we take the a successful Parse attempt that has moved the Ordinal forward, we can feed that relative Position into the next parser in the expression tree, and we've opened up the possibility of chaining and composition. 

```csharp
public interface IParser<T>
{
    ParseResult<T> Parse(Position position);
}

public class ParseResult<T>
{
    public bool Success { get; init; }

    public T Value { get; init; }

    public Position Position { get; }
}
```
Let's look at the Then implementation to give us an idea of how a single Parser instance works. Note how we have a first parser, representing the syntax on the left, and a function to generate the second parser, which we run and use to evaluate the syntax on the right but only if the first parser is successful:

```csharp
public class Then<T, U> : IParser<U>
{
    IParser<T> _first;
    Func<T, IParser<U>> _second;

    public Then(IParser<T> first, Func<T, IParser<U>> second)
    {
        _first = first;
        _second = second;
    }

    public ParseResult<U> Parse(Position position)
    {
        var result = _first.Parse(position);
        position = result.Position;

        if (!result.Success)
            return ParseResult<U>.Failure(position);

        var thenResult = _second(result.Value)
            .Parse(position);
        position = thenResult.Position;

        return thenResult.Success ?
            ParseResult<U>.Successful(position, thenResult.Value) :
            ParseResult<U>.Failure(position);
    }
}
```

Using this pattern, we can build a tree of parsers to represent our expression and run this over our Token array. 

### Parser implementation
Let's take our original naive parser for DayTime, and replace it with our new parser setup.

```csharp
public static IParser<DayTime> DayTimeParser =
    new Then<DayOfWeek, DayTime>(
        new IsToken<DayOfWeek>(),
        dow => new Select<LocalTime, DayTime>(
            new IsToken<LocalTime>(),
            lt => new DayTime { Day = dow, LocalTime = lt }));
```

![DayTime Parser Tests](/assets/expression-parsing/day-time-parser-tests.PNG)

Ok - it works, it's got the Is&lt;DayOfWeek>(), Then(), Is&lt;LocalTime>(), Select&lt;DayTime>() pattern, but this isn't particularly fun to work with. There are lots of new keywords and the code reads like [Polish notation](https://en.wikipedia.org/wiki/Polish_notation).

### Making it fluent

To make things easier, we can create a bunch of static helper methods to configure our root parsers, and a set of extension methods to combine our parsers together:

```csharp
public static class Parsers
{
    public static IParser<T> IsToken<T>() =>
        new IsToken<T>();

    public static IParser<List<T>> ListOf<T>(IParser<T> parser) =>
        new ListOf<T>(parser);

    public static IParser<T> Or<T>(params IParser<T>[] options) =>
        new Or<T>(options);
}

public static class ParserExtensions
{
    public static IParser<U> Then<T, U>(this IParser<T> core, Func<T, IParser<U>> then) =>
        new Then<T, U>(core, then);

    public static IParser<U> Select<T, U>(this IParser<T> core, Func<T, U> select) =>
        new Select<T, U>(core, select);

    public static IParser<T> End<T>(this IParser<T> core) =>
        new End<T>(core);
}
```

Then we can re-write our original implementation using a fluent approach to compose our parser tree, with the result being much cleaner:

```csharp
public static IParser<DayTime> DayTimeFluentParser =
    Parsers.IsToken<DayOfWeek>().Then(dow =>
        Parsers.IsToken<LocalTime>().Select(lt =>
            new DayTime { Day = dow, LocalTime = lt }));
```

But this is only a parser for our basic scenario, right? We've added a whole bunch of code, complexity and fluff, with no new functionality, and we're still left with only this?

### Composition

Well... not quite. Now we can create parsers from other parsers; that's what we did when we created our DayTimeParser from Is&lt;DayOfWeek> and Is&lt;LocalTime> checks, so let's take our pickup / dropoff scenario above, where our DayTimeParser can point to either of our fluent/non-fluent implementations:

```csharp
public static IParser<DayTime> PickupDayTime => Parsers
    .IsToken<PickupFlag>()
        .Then(_ => DayTimeParser);

public static IParser<DayTime> DropOffDayTime => Parsers
    .IsToken<DropoffFlag>()
        .Then(_ => DayTimeParser);

public static IParser<PickupDropoff> PickupDropOff => 
    PickupDayTime.Then(pu => 
        DropOffDayTime.Select(dr => 
            new PickupDropoff { Pickup = pu, DropOff = dr }))
    .End();
```

And that's it, we've got our text with our pickup DayTime and our dropoff DayTime mashed together into a single object, using composition and some tidy fluent snippets.

![Pickup Dropoff Tests](/assets/expression-parsing/pickup-dropoff-tests.PNG)

### Completing the set

We're still pretty limited in terms of what we can do with our original Is/Then/Select parsers, we can't cater to some of the more complex scenarios. We're going to need a few more.

#### Or
A parser that matches one of multiple child parsers of the same result type. Let's say we want to treat two different Tokens the same way; we need something conditional in our expression to indicate that one term must be true. Let's take an expression where we want to use "to" or "-" to indicate that we are matching a term describing a range between two values.

![Range Expression](/assets/expression-parsing/expression-tree-range.png)

```csharp
public static IParser<RangeMarker> RangeMarker =
    Parsers.Or(
        Parsers.IsToken<HypenSymbol>().Select(r => new RangeMarker()),
        Parsers.IsToken<JoiningWord>().Select(r => new RangeMarker())
    );
```

We can then use this as part of a combination to satisfy either condition:

```csharp
public static IParser<Range<LocalTime>> TimeRangeParser =
    Parsers.IsToken<LocalTime>().Then(from =>
        RangeMarker.Then(_ =>
            Parsers.IsToken<LocalTime>()
                .Select(to => new Range<LocalTime> { From = from, To = to })));
```

#### ListOf

A parser that matches a number of items against a single child parser and returns a List of the child parser values:

```csharp
ParseResult<List<DayTime>> result = Parsers
    .ListOf(DayTimeParser)
    .Parse(tokens);
```

![Pickup Dropoff Tests](/assets/expression-parsing/list-of-day-time-parser-tests.PNG)

#### End

A parser that determines if the result is the end of the Token array.
And a parser to validate that we are at the end of our token arrays. This prevents a match that has trailing tokens that are not covered by the complete parsing expression.

```csharp
public static IParser<DayTime> ExplicitDayTimeParser =
    DayTimeParser.End();
```

All of these lovely Parser implementations are available [here](https://github.com/TristanRhodes/TextProcessing/blob/master/TextProcessing/OO/Parsers/Parsers.cs), with the configurations for the origional sentence structures [here](https://github.com/TristanRhodes/TextProcessing/blob/master/TextProcessing/OO/Parsers/ExpressionParsers.cs). There might even be some tests in there somewhere.

### Turtles

And after that... it all gets a bit, Turtles... all the way down...

![Turtles](/assets/expression-parsing/turtles.jpg)

So if you want to take this thought experiment a bit further, try thinking about (or implementing) lists of lists of complex objects.... or lists of lists of lists... or just n+1. Once you solve the problem at one level, you can solve it at any level.

### Code

More code turtles are available [here](https://github.com/TristanRhodes/TextProcessing) where I put together a bunch of different ways to do parsing. We're currently still in the slavishly Object Orientated part, so check out the [OO folder](https://github.com/TristanRhodes/TextProcessing/tree/master/TextProcessing/OO).

### Next Up

So far, we've been good little object orientated boys and girls, we've got our Interface, we have our little implementation classes, we've got good SOLID principles, but it's still pretty heavy... this interface is only a single method.

```csharp
public interface IParser<T>
{
    ParseResult<T> Parse(Position position);
}
```

Does it need to be an interface? What else could it be?

```csharp
public delegate ParseResult<T> Parser<T>(Position position);
```

<p align="center"><iframe src="https://giphy.com/embed/26gR0YFZxWbnUPtMA" width="480" height="270" frameBorder="20" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/tipsyelves-math-26gR0YFZxWbnUPtMA">via GIPHY</a></p></p>


### Credits
Header Image by <a href="https://unsplash.com/@glencarrie?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Glen Carrie</a> on <a href="https://unsplash.com/?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>
  