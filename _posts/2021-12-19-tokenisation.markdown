---
layout: post
author: tristan-rhodes
title: Tokenisation
excerpt: Turning strings, into things.
featured-image: /assets/tokenisation/featured-image.jpg
---
## Tokens

In my [last post](/2021/12/11/regex-redemption.html), I talked about using Regex to match the pattern of a WeekDay or ClockTime string and unpacking this into a meaningful object. This is known as Parsing, and we can see plenty of examples in the .Net framework, int.Parse(), Guid.Parse(), plus others. But how do we deal with parsing more complicated bodies of text that are made up of multiple words / parts?

```csharp
"Monday 08:30am"
"tue 18:30"
"thurs 12:30pm"
```

We're going to tackle this in two phases firstly we're going to split our text into parts and convert them into tokens (Tokenisation/Scanning phase), then we're going to check the tokens match a correct pattern and turn that into an object (Parsing phase).

![Parsing](/assets/tokenisation/ParsingProcess.png)

### Tokeniser
To start with, we're going to need a Tokeniser for converting a complete string into tokens. This is going to need a split pattern and a number of ITokenProcessors to identify and convert the individual string fragments.

```csharp
public class Tokeniser
{
    Regex _splitPattern;
    IList<ITokenProcessor> _tokenisers;

    public Tokeniser(string splitPattern, params ITokenProcessor[] tokenisers)
    {
        _splitPattern = new Regex(splitPattern);
        _tokenisers = tokenisers.ToList();
    }

    public IEnumerable<Token> Tokenise(string inputString)
    {
        var parts = _splitPattern.Split(inputString);
        foreach(var part in parts)
        {
            var match = _tokenisers
                .Where(t => t.IsMatch(part))
                .Select(t => t.Tokenise(part))
                .FirstOrDefault();

            yield return match == null ?
                Token.Create(part) :
                match;
        }
    }
}

public interface ITokenProcessor
{
    bool IsMatch(string token);
    Token Tokenise(string token);
}
```

We will now need to port the logic from the original post for matching and parsing DayOfWeek and LocalTime and implement our ITokenProcessor interface, then drop them all into a token processor...

![Token Tests](/assets/tokenisation/ConvertedTokensTest.PNG)

Now we have an array of Tokens generated from a string, with tokens that match a specific Tokeniser rule being captured and converted, note that I've also included an integer converter. But we still need something that checks if these tokens are a recognizable configuration, and then converts this into a meaningful object.

### "Parsing" a complete DayTime

Our DayTime object is composed of two parts, the Day and the Time. All we need now is to create a simple parser that takes an array of Tokens[] and populates the fields in a DayTime object.

```csharp
public class DayTime
{
    public DayTime(DayOfWeek day, LocalTime time)
    {
        Day = day;
        LocalTime = time;
    }

    public DayOfWeek Day { get; }
    public LocalTime LocalTime { get; }
}

public class SimpleDayTimeParser : IParser<DayTime>
{
    public bool IsMatch(Token[] tokens)
    {
        if (tokens.Length != 2) return false;
        if (!tokens[0].Is<DayOfWeek>()) return false;
        if (!tokens[1].Is<LocalTime>()) return false;
        return true;
    }

    public DayTime Parse(Token[] tokens)
    {
        if (!IsMatch(tokens))
            throw new ApplicationException("Bad Match");

        return new DayTime(
            tokens[0].As<DayOfWeek>(),
            tokens[1].As<LocalTime>());
    }
}
```

And that's it, we can convert our two part string into a higher order object.

![Parsed DayTime](/assets/tokenisation/ParsedDayTime.PNG)

### Expression Trees and Parsers
Our "parser" implementation is a bit of an overstatement here, this is the most naive logic we can get. It doesn't have any flexibility for supporting more complex structures and is only going to support a very explicit match. If we wanted to extend this to support more complex patterns, we're going to be quite limited. Consider trying to solve the following patterns:

```csharp
// Separate two part element context
"Pickup Mon 08:00 dropoff wed 17:00"

// Range elements
"Open Mon to Fri 08:00 to 18:00"

// Repeating tokens
"Tours 10:00 12:00 14:00 17:00 20:00"

// Repeating complex elements
"Events Tuesday 18:00 Wednesday 15:00 Friday 12:00"
```

To solve these problems, we're going to need a parser with the ability to process an expression tree, and we'll get to that next. 

All the code is available in a small sandbox project I'm building to go along with a couple of posts [here](https://github.com/TristanRhodes/TextProcessing).

### Note
You can do your parsing in one step, using an approach known as scannerless parsing (We'll get to [Sprache](https://github.com/sprache/Sprache) later), but there are a few benefits to splitting it out. It's easier to work in data sanitisation at the Tokenisation phase, and keep the actual syntax rules clear of all the more fuzzy token patterns, but as with all things, how you do it will depend on what you need to achieve.

### Credits

Header Image by <a href="https://pixabay.com/users/fantasycoins-1910023/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1146135">Tim west</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1146135">Pixabay</a>