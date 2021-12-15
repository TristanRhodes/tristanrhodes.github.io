---
layout: post
author: tristan-rhodes
title: Tokenisation
excerpt: Turning strings, into things.
featured-image: /assets/tokenisation/featured-image.jpg
---
## Tokens

In my [last post](/2021/12/11/regex-redemption.html), I talked about using Regex to match the pattern of a WeekDay or ClockTime string and unpacking this into a computer usable object. This process is called tokenisation, and the WeekDay and ClockTime are both instances of a token. Last time we only looked at converting a single token, but how do we begin to process a more complicated string of tokens, like taking a string made of a Day token and a Time token building a DayTime token?

```csharp
"Monday 08:30am"
"tue 18:30"
"thurs 12:30pm"
```

Well, to start with, let's add the logical concept of composition. A token can be composed of other tokens, so what we need is to have a Tokeniser that uses our Day tokenising logic and Time tokenising logic, runs them together and mashes them into a DayTime object.

### Token Processing

Token processing is composed of three concepts, a Token base object to represent a converted object, an ITokeniser interface that implements handling for detecting a string match and performing a conversion into a Token, and the TokenProcessor itself which runs the registered ITokeniser interfaces against each string and executes the relevant conversion.

#### ITokeniser
This interface takes a string, and returns a Tokenised version of it, the result indicates whether the tokenisation succeeded, and if so, what the value was.

```csharp
public interface ITokeniser<T>
{
    Tokenised<T> Tokenise(string token);
}
```

#### Tokenised
This is the response from the above interface, it represents a tokenisation attempt on a string.

```csharp
public class Tokenised<T>
{
    T _value;
    private Tokenised(string text)
    {
        Text = text;
        Success = false;
    }

    public Tokenised(string text, T value) 
    {
        Text = text;
        _value = value;
        Success = true;
    }

    public string Text { get; }

    public T Value => 
        Success ? _value : throw new ApplicationException("Did not succeed");

    public bool Success { get; }

    public override string ToString() =>
        $"{this.GetType().Name}: {Value}";

    public static Tokenised<T> Failed(string text) => 
        new Tokenised<T>(text);
}
```

With this in place, we now need to take the logic from the original post for matching and parsing DayOfWeek and LocalTime and implement our ITokeniser interface.

#### WeekDay

```csharp
public class WeekDayTokeniser : ITokeniser<DayOfWeek>
{
    Regex regex = new Regex("^(?<Monday>[Mm]on(day)?)|(?<Tuesday>[Tt]ue(sday)?)|(?<Wednesday>[Ww]ed(nesday)?)|(?<Thursday>[Tt]hu(rs(day)?)?)|(?<Friday>[Ff]ri(day)?)|(?<Saturday>[Ss]at(urday)?)|(?<Sunday>[Ss]un(day)?)$");

    public Tokenised<DayOfWeek> Tokenise(string token)
    {
        var match = regex.Match(token);
        if (!match.Success)
            return Tokenised<DayOfWeek>.Failed(token);

        if (match.Groups["Monday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Monday);

        if (match.Groups["Tuesday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Tuesday);

        if (match.Groups["Wednesday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Wednesday);

        if (match.Groups["Thursday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Thursday);

        if (match.Groups["Friday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Friday);

        if (match.Groups["Saturday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Saturday);

        if (match.Groups["Sunday"].Success)
            return new Tokenised<DayOfWeek>(token, DayOfWeek.Sunday);

        return Tokenised<DayOfWeek>.Failed(token);
    }
}
```

#### ClockTime

```csharp
public class ClockTimeTokeniser : ITokeniser<LocalTime>
{
    Regex regex = new Regex(@"^(((?<hr>[01]?\d|2[0-3]):(?<min>[0-5]\d|60))|((?<hr>([0]?\d)|1[0-2]):(?<min>[0-5]\d|60)((?<am>am)|(?<pm>pm))))?$");

    public Tokenised<LocalTime> Tokenise(string token)
    {
        var match = regex.Match(token);
        if (!match.Success)
            return Tokenised<LocalTime>.Failed(token);

        var hour = int.Parse(match.Groups["hr"].Value);
        var min = int.Parse(match.Groups["min"].Value);
        var am = match.Groups["am"].Success;
        var pm = match.Groups["pm"].Success;
        var twentyFourHr = !am && !pm;

        if (twentyFourHr)
        {
            return new Tokenised<LocalTime>(token, new LocalTime(hour, min));
        }
        if (am)
        {
            return new Tokenised<LocalTime>(token, new LocalTime(hour == 12 ? 0 : hour, min));
        }
        else if (pm)
        {
            return new Tokenised<LocalTime>(token, new LocalTime(hour == 12 ? 12 : hour + 12, min));
        }

        return Tokenised<LocalTime>.Failed(token);
    }
}
```
#### DayTime
Now to support our higher order concept, we create the DayTime ITokeniser implementation that uses the two tokenisers above.

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

public class DayTimeTokeniser : ITokeniser<DayTime>
{
    WeekDayTokeniser _weekDayTokeniser = new WeekDayTokeniser();
    ClockTimeTokeniser _timeTokeniser = new ClockTimeTokeniser();

    public Tokenised<DayTime> Tokenise(string token)
    {
        var parts = token.Split(' ');
        if (parts.Length != 2)
            return Tokenised<DayTime>.Failed(token);

        var weekDay = _weekDayTokeniser.Tokenise(parts[0]);
        if (!weekDay.Success)
            return Tokenised<DayTime>.Failed(token);

        var time = _timeTokeniser.Tokenise(parts[1]);
        if (!time.Success)
            return Tokenised<DayTime>.Failed(token);

        return new Tokenised<DayTime>(token, 
            new DayTime(weekDay.Value, time.Value));
    }
}
```

##### Note:
It's worth taking a closer look at the updated ClockTime regex as this now selects for &lt;12 hrs with am/pm suffix or &lt;24 hrs for no suffix, it's a bit more complex, but there's no need for number validation against the pattern and a chance of a false positive that then causes an error when converting.

### Array of Tokens
Now we've got our tokeniser and we've got our two token types, we can generate a token list from a string, and test that the tokens generated correctly. Here it is in action.

![Token Tests](/assets/tokenisation/TokenTests.PNG)

### DayTimeToken

This is good progress, but we're still missing the last step, we need to turn that whole string into a single DayTime object. We need a new class, and bit of conversion logic:

```csharp
public class DayTimeToken : Token
{
    public DayTimeToken(string value, DayOfWeek day, LocalTime localTime)
        : base(value)
    {
        Day = day;
        LocalTime = localTime;
    }

    public DayOfWeek Day { get; }
    public LocalTime LocalTime { get; set; }
}

public DayTimeToken Convert(string text)
{
    var parts = text.Split(" ");

    var tokens = _tokenProcessor
        .Tokenise(parts);

    if (tokens.Length != 2)
        throw new ArgumentException("Unexpected token array length");

    var weekDay = tokens[0] as WeekDayToken;
    var clockTime = tokens[1] as ClockTimeToken;

    if (weekDay is null)
        throw new ApplicationException("No WeekDay token");

    if (clockTime is null)
        throw new ApplicationException("No ClockTime token");

    return new DayTimeToken($"{weekDay.Value} {clockTime.Value}", weekDay.DayOfWeek, clockTime.LocalTime);
}
```

Now this runs nicely, we can split our string, convert it into an array of tokens, and convert those tokens into a higher order object and wrap that functionality in a tidy method.

![Token Tests](/assets/tokenisation/ConvertedTokensTest.PNG)

### Next Steps

We're catering here for DayTime, But there's something still missing. This feels quite naive, and not really extensible. We can only convert an exact match of day time, and there is no flexibility to compose more complex patterns. To really make our system extensible, we will need to be able to build out support for more complex patterns:

```csharp
// Seperate start / end elements
"Pickup Mon 08:00 dropoff wed 17:00"

// Range elements
"Open Mon to Fri 08:00 to 18:00"

// Repeating elements
"Tours Monday to Friday 10:00 12:00 14:00 17:00 20:00"
```

So what can we do about this? How do we make our system more flexible when dealing with complex expressions? The first step, we're going to make a Token contain a list of other Tokens. This means that we can turn our Tokens into a tree that represents a syntax structure, or syntax tree.

Something still needs to build that though, and that is a SyntaxProcessor. 

### Credits

Header Image by <a href="https://pixabay.com/users/fantasycoins-1910023/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1146135">Tim west</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1146135">Pixabay</a>