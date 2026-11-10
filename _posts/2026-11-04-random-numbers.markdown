---
layout: post
author: tristan-rhodes
title: Random Numbers
excerpt: From a one sided die
featured-image: /assets/random-numbers/featured-image.jpg
---

It's well documented that LLMs struggle with random number generation. The weights baked into the tensors and training will force a behaviour that repeats the most likely token sequences from training 90% of the time.
If you look at the reasoning behind this, it makes sense from the computer logic perspective.

![Random Number Reasoning](/assets/random-numbers/random-reasoning.png)

Based on the bots own logic, "mid-upper, odd, common, etc" 7 is the most random number because:
* Odd numbers are more random than even numbers.
* 1 and 9 are extreemes, so not common.
* 5 is exactly in the middle.
* 7 is more mid-upper than 3.
* Therefor 7 is the random number between 1-10.

Perfect, for a random number between 1-10, 90% of the time I get 7!

So much of this behaviour is driven by the weights of the underlying model, which is a derivative of how it is trained. (And I will caveat this with models have now been trained to write random number generation code, instead of predicting numbers, but that only works when an agent harness can actually run code or invoke randomisation tools.)

For 1-100, we trend towards the The Hitchhiker’s Guide to the Galaxy, and 90% of the time I get 42...

![Random Number Reasoning 1-100](/assets/random-numbers/random-reasoning-1-100.png)

Reminds me of the [XKCD comic](https://xkcd.com/221/), which is as prescient today as it was then.

![XKCD](https://imgs.xkcd.com/comics/random_number.png)

## Not just numbers

And it's not just numbers! Predictable patterns of behaviour surface all the time with LLM's. While benchmarking my local setup to balance an optimal throughput / context size, I had a prompt `Generate 100 token short story about a hungry fish.` that I relied on to measure. I ran this prompt _a lot_, and the protagonist was _always_ Barnaby.

![Barnaby the hungry fish](/assets/random-numbers/barnaby.png)

### and

![Barnaby the other hungry fish](/assets/random-numbers/barnaby-2.png)

And it's _not just fish!_.

## Elias / Elara

We had another hero leaping to the fore in 2026 - `Elias the lighthouse keeper` covered by IBM [here](https://www.ibm.com/think/news/why-every-ai-writes-the-same-story-elias-thorne).

I had to fire it a bunch of times, but from my local runs - he might not be a lighthouse keeper, but he's out there in a storm!

![Elias](/assets/random-numbers/elias.png)

And a shout out to the side note at the bottom of the IBM [article](https://www.ibm.com/think/news/why-every-ai-writes-the-same-story-elias-thorne), as Elara enters the chat.

![Elara](/assets/random-numbers/elara.png)

## Mean, Median, Mode

LLMs are trained on huge datasets to determine the most likely token given a context. This will be run through a range of weights, and those weights produce an "average" behaviour. We've got a couple of different types of averages:

* Mean - The total of values, divided by the number of values. The most common average.
* Median - The middle value of the set.
* Modal - The most common value in the set.

LLMs appear to exhibit Modal behaviour rather than Mean - the Mean would lead to a spread closer to the standard deviation, but the Modal spread trends towards high probability spikes, like 7, Barnaby, or Elias - which is what we see day to day.

TODO: Diagram of Average (LLM, Human, Normal Distribution, Equal Distribution)

## Selection Mechanics

If you want to branch out a bit more and get _really_ creative, you can try setting the model temperatures to 2, but you get more variance overall, so your story (or code, or data) will end up as a reality distorting salvador dali-esque fever dream!

NOTE: If anyone has used the bot for document reviews, you should be familiar with a pattern... "This post/document/thesis has a strong/solid core, but is let down by a/b/c. As it stands it's a 6.5-7.5, but if you add x/y/z, it'll be a 8.5-9!". And when you iterate, that barely moves. There's always something else that's needed to get the magical 9. 

If I were to point two bots that exhibit this behaviour at each other, then what I would get is a control systems problem, where it will loop, but be unable to reach the "zero point" of hitting the goal of 9. And if it does this just iterating on text between an editor and a reviewer role, then I would expect to see the same mechanisms in code generation / review as well.

[Pointed Look]

## Pending

One thing I have found is that it is easier for an LLM to identify above average, than it is at creating above average. It can only generate selections around the mode, but it can clearly see things that deviate from it and highlight where things stand above / below the line (Just to caveat - I'm not using my own work to test this, I just confirmed Batman vs Superman was worthy of an Oscar).

While interrogating the bot on this - it highlighted the distinction between Recognition/Evaluation vs Generation/Creation. 

During evaluation an LLM is rating or critiquing a complete artefact against its internal library to identify why it's good, for generation every step is "Given the previous tokens, what is the most likely next token." These are fundamentally different operations with different 

But it's worth taking with a grain of salt - _It's also trained to flatter you when it thinks you are the author._

## Homogenisation

We are all finding ourselves dazzled by the raw power of Claude (other frontier models are available), and the books, websites, slide decks and systems we can create from it in seconds. But - I do not see what progress we make as a species having a 1000 individuals generate the same 3js animated horse, or identical purple glass websites with swooshing animations.

I do not believe we can ideate, create and sell unique products through a lense of homogenisation. We can't prompt more/harder, we need to retain and promote creative, out of band thought and curate our vision to avoid becoming a bland, uniformaly "purple-glass" nightmare (Or maybe that's too late).

So how _do_ we stop calling all our fish Barnaby? Stay creative folks - Brain first, bot second.

Maybe use a pen.