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

So... what is this dastardly bug that has cropped up? Why are my tests failing, and how did I get there? Well... I started with the simplest scenarios. This seems like a solid start right? We're all good?

![All Good](/assets/check-your-assumptions/AllGood.png)

 That all works right? Regex magic all good. But when I added the new scenario above, it failed. And this confused me, because I knew it worked on the simpler scenario, that assumption was now in my white list. My test pass, they prove the regex is correct, but a new scenario failes then it has to be a bug, right?

Well... not quite. See... here's the thing...

<iframe src="https://giphy.com/embed/NmerZ36iBkmKk" width="480" height="323" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/NmerZ36iBkmKk">via GIPHY</a></p>

Regex... has _TWO STRING PARAMETERS_. Do you know what happens if you put them in the wrong order? _It don't work right_. So what was my assumption? That the Regex pattern was on the left.

But... why didn't we catch that? Surely the tests would have picked up that the Regex and the check are in the wrong order? We validate both the positive and the negative... But...

![Oops](/assets/check-your-assumptions/Oops.png)

This check string matches the valid pattern, and the pattern matches the valid check string, so it works either way round.

So yeah, assumptions creep in, and you forget to go back to basics and re-validate.

## Sick Note
I totally want to play my sick note card here. I had food poisoning and spent hours peering through heavy brain fog.

<iframe src="https://giphy.com/embed/3o6ZsWFclZCXU3SuSQ" width="480" height="270" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/southparkgifs-3o6ZsWFclZCXU3SuSQ">via GIPHY</a></p>

But I don't really have any excuse... this is just one of those experiences that keeps you humble.

So in a note to other devs... sick or not, check your bloody assumptions before you do something _really stupid_ like filing a report for a [non existant bug](https://github.com/dotnet/runtime/issues/62017).

### Credit
Header Image by <a href="https://pixabay.com/users/openclipart-vectors-30363/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=152211">OpenClipart-Vectors</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=152211">Pixabay</a>