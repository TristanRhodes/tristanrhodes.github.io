---
layout: post
author: tristan-rhodes
title: Random Numbers
excerpt: From a one sided die
featured-image: /assets/random-numbers/featured-image.jpg
---

It's well documented that LLMs struggle with random number generation. The weights baked into the tensors and training will force a behaviour that repeats the _average_ 100% of the time. 
If you look at the reasoning behind this, it makes sense from the computer perspective.

![Random Number Reasoning](/assets/random-numbers/random-reasoning.png)

Based on the bots own logic, 7 is the most random number because:
* Odd numbers are more random than even numbers.
* 1 and 9 are on the extreemes and should be discarded.
* 5 is in the middle and should be discarded.
* 7 is bigger than 3, there for it's more random.
* 7 is the random number between 1-10.

Perfect, for a random number between 1-10, 90% of the time I get 7!

TODO: Diagram of Average (LLM, Human, Average Distribution, Normal Distribution)

So much of this behaviour is driven by the weights of the underlying model, which is a derivative of how it is trained.

For 1-100, we trend towards the The Hitchhiker’s Guide to the Galaxy, and 90% of the time I get 42...

![Random Number Reasoning 1-100](/assets/random-numbers/random-reasoning-1-100.png)

Reminds me of the old programmer meme that the number 4 was chosen via a dice roll and hard coded into the system.