---
layout: post
author: tristan-rhodes
title: Expression Parsing
excerpt: Turning bigger strings, into complex things.
featured-image: /assets/expression-parsing/featured-image.jpg
---

## Expression Trees
In my [last post](/2021/12/19/tokenisation.html), I talked about splitting up sentences of text and using Regex inside a number of TokenParsers to turn a longer string into an array of objects representing fragments of a sentence. We also created a basic parser implementation to convert a sentence containing two parts, a Day and a Time, into a single DayTime object. But we also considered the following scenarios:

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

Selecting exactly two values into a single object is simple, but when things become conditional, repetitive, or optional, the patterns cannot be easily configured in a single step. We need a better understanding of the pattern structure in order to represent something that solves it in code. For this, we are going to go with expression trees, and we'll start by describing our original parsing subject, DayTime:

![Parsed DayTime](/assets/expression-parsing/expression-tree-daytime.PNG)

### Pickup DropOff
![Parsed PickupDropoff](/assets/expression-parsing/expression-tree-pickup-drop-off.png)

### Range Elements and seperators
![Parsed PickupDropoff](/assets/expression-parsing/expression-tree-ranges.png)

### Repeating Tokens

### Repeating Complex Elements
![Parsed PickupDropoff](/assets/expression-parsing/expression-tree-repeating-complex.png)

### Focus on Range Expressions

Now, what if we want to be able to interpret two different configurations, the same way, we need something conditional in our expression. Let's say we want to

![Range Expression](/assets/expression-parsing/expression-tree-range.png)

### Credits
Header Image by <a href="https://unsplash.com/@glencarrie?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Glen Carrie</a> on <a href="https://unsplash.com/?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>
  