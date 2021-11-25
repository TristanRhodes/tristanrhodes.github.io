---
layout: post
author: tristan-rhodes
title: Check Your Assumptions
excerpt: Sometimes we know something so well, we assume we can't make an obvious mistake.
featured-image: /assets/check-your-assumptions/featured-image.png
---

## Regex

I use a lot of regex. I've written parses and tokenisers for a wide range of DSL's and data formats. So one day, I reached for my Regex-fu to validate a credit card number, and it was gone. Like superman finding he suddenly couldn't fly, I found myself glued to the earth, taking the stairs...

## What's going on?
Does this look right to you? Because it sure didn't look right to me.... and I spent a _long_ time staring at it.

![Not a regex bug](/assets/check-your-assumptions/NotARegexBug.png)

## What's _really_ going on?

So... what is this dastardly bug that has cropped up? Why are my tests failing, and how did I get there? Well... I started with the simplest scenarios, and they worked, and when I added a new scenario, it failed. And this confused me, because I knew it worked on the simpler scenario. So, if my test passes, and the code was correct, but a new scenario failes then it has to be a bug, right?

Well... not quite. See... here's the thing...

<iframe src="https://giphy.com/embed/NmerZ36iBkmKk" width="480" height="323" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/NmerZ36iBkmKk">via GIPHY</a></p>

Regex... has _TWO STRING PARAMETERS_. Do you know what happens if you put them in the wrong order? _It don't work_.

## Sick Note
I totally want to play my sick note card here. I had food poisoning and spent hours peering through heavy brain fog.

<iframe src="https://giphy.com/embed/3o6ZsWFclZCXU3SuSQ" width="480" height="270" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/southparkgifs-3o6ZsWFclZCXU3SuSQ">via GIPHY</a></p>

But I don't really have any excuse... this is just one of those experiences that keeps you humble.

So in a note to other devs... sick or not, check your bloody assumptions before you do something _really stupid_ like filing a fucking report for a [non existant bug](https://github.com/dotnet/runtime/issues/62017).

### Credit
Header Image by <a href="https://pixabay.com/users/openclipart-vectors-30363/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=152211">OpenClipart-Vectors</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=152211">Pixabay</a>