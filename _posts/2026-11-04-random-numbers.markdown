---
layout: post
author: tristan-rhodes
title: Random Numbers
excerpt: From a one sided die
featured-image: /assets/random-numbers/featured-image.jpg
---

It's well documented that LLMs struggle with random number generation. The weights baked into the tensors and training will force a behaviour that repeats the _average_ of training 95% of the time. 
If you look at the reasoning behind this, it makes sense from the computer logic perspective.

![Random Number Reasoning](/assets/random-numbers/random-reasoning.png)

Based on the bots own logic, 7 is the most random number because:
* Odd numbers are more random than even numbers.
* 1 and 9 are on the extreemes and should be discarded.
* 5 is in the middle and should be discarded.
* 7 is bigger than 3, there for it's more random.
* 7 is the random number between 1-10.

Perfect, for a random number between 1-10, 95% of the time I get 7!

So much of this behaviour is driven by the weights of the underlying model, which is a derivative of how it is trained. (And I will caveat this with models have now been trained to write random number generation code, instead of predicting numbers, but that only works when an agent harness can actually run code.)

For 1-100, we trend towards the The Hitchhiker’s Guide to the Galaxy, and 95% of the time I get 42...

![Random Number Reasoning 1-100](/assets/random-numbers/random-reasoning-1-100.png)

Reminds me of the [XKCD comic](https://xkcd.com/221/), which is as prescient today as it was then.

![XKCD](https://imgs.xkcd.com/comics/random_number.png)

## Not just numbers

And it's not just numbers! Predictable patterns of behaviour surface all the time with LLM's. While benchmarking my local setup to balance an optimal throughput / context size, I had a prompt `Generate 100 token short story about a hungry fish.` that I relied on to measure. I ran this prompt _a lot_, and the protagonist was _always_ Barnaby.

![Barnaby the hungry fish](/assets/random-numbers/barnaby.png)

and

![Barnaby the other hungry fish](/assets/random-numbers/barnaby-2.png)

## Elias / Elara

And, it's _not just fish!_. We had another hero leaping to the fore in 2026 - `Elias the lighthouse keeper` covered by IBM [here](https://www.ibm.com/think/news/why-every-ai-writes-the-same-story-elias-thorne).

![Elias](/assets/random-numbers/elias.png)

And a shout out to the Side note at the bottom of the IBM [article](https://www.ibm.com/think/news/why-every-ai-writes-the-same-story-elias-thorne), as Elara enters the chat.

![Elara](/assets/random-numbers/elara.png)

## Selection Mechanics

If you want to branch out a bit more and get _really_ creative, you can try setting the model temperatures to 2, but you get more variance overall, so your story (or code, or data) will end up as a salvador dali-esque fever dream!

TODO: Diagram of Average (LLM, Human, Average Distribution, Normal Distribution)

## Homogenisation

TODO: