---
layout: post
author: tristan-rhodes
title: Blogging in an AI world
excerpt: What does it even mean?
featured-image: /assets/blogging-in-an-ai-world/featured-image.jpg
---

## AI and I

I come from a strange background with AI. My first program was a chess computer, and I've been in/around the space longer than I have been in industry. In that time, i've seen technologies that were once cutting edge AI become commoditised, turn into brands or products, and cease to be considered AI (Google maps navigation, OCR).

My early experience in industry was skewed by the tail end of the last [AI Winter](https://en.wikipedia.org/wiki/AI_winter), and was an era where people had to hide their usage of AI in order to secure funding for projects.

I'm familiar with the cycle of excitement and overinvestment, followed by the pullback of funding and I see the current time as just another phase in the AI history cycle. In the words of AC/DC, Money Talks, Everything else Walks - and once investors say they don't want AI, then that will be the driver. For now - Money says AI, and that's what we're going to do.

At some point "We use LLM's in our Pipeline" will be the same as declaring that you use Containers, Postgres or have CI/CD. The thing that matters will be what you do with it, and whether it is stable and secure.

Until that time comes - we as engineers are all figuring out our place in the world, and what our relationship with our tools is going to look like.

I have tried to define an AI Policy that reflects my values and will be workable in the real world, published [here](/ai-policy).

## How I use AI - light touch.

* A simple Agents.md for common rules works well for most code bases - avoid bloat.
* Keep the skills files to a minimum - Only add core operations.
* Keep your context small and focused on task - My top two commands are `/new` and `esc`.
* A code base should be navigable by Humans. As a side effect, this will also be navigable by bots. We still need to pass audit - our investors will want to see what is being done.
* Code tags are great for the bot to Grep/Discover. These are signposts in your code base. Eg //BOT: Extend here for additional jobs.

## Test and Verify
I have a strong emphasis on testing. Most of my engineering effort has been focused on defining what correct looks like, and then proving that it does actually work that way.

In an age of AI, rapid development and equally rapid change - having robust test harnesses, test suites and observability is even more critical in enabling us to safely move at speed.


## Why keep blogging?

Sure - I could make a content factory loop, schedule a job running a bot, it's not a complicated process:

* Schedule a job
* Generate 5 ideas based off current tech trends for a persona description (Excentric bald engineer who likes hats)
* Pick the best idea and generate a detailed post from the perspective of a principal engineer.
* Push to github.

Hell - you can do a full youtube content feed:

* Schedule a job
* Generate 5 ideas based off current trends for a text persona description (Excentric bald engineer who likes hats)
* Pick the best idea and generate an extended script following the persona.
* Generate a audio track of the script from a sample voice pack.
* Generate a video from the script/audio using an image asset pack.
* Publish to YouTube once a week.

Content is easy, it's become noise. Now noise is easy, signal is hard. So mostly I write for me - as a journal of things I have built, a record of things I explored and learned, and ways of approaching problems that I find useful or interesting.

I will speak with my own voice and aim to be a home of something genuine, and maybe a signal in the sea of noise.

I'm keeping my old blog, and the old format, although I'll work on modernising the stack, as it provides history and content from the "before times" and a bridge into the modern AI era.