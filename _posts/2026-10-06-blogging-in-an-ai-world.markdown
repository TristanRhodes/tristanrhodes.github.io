---
layout: post
author: tristan-rhodes
title: Blogging in an AI world
excerpt: What does it even mean?
featured-image: /assets/blogging-in-an-ai-world/featured-image.jpg
---

## AI and I

I come from a strange background with AI. My first program was a chess computer, and I've been in/around the space longer than I have been in industry. In that time, I've seen technologies that were once cutting edge AI become commoditised, turn into brands or products, and cease to be considered AI (Google Maps navigation, Google Translate, Grammarly).

My early experience in industry was skewed by the tail end of the last [AI Winter](https://en.wikipedia.org/wiki/AI_winter), and was an era where people had to hide their usage of AI in order to secure funding for projects.

I'm familiar with the cycle of excitement and overinvestment, followed by the pullback of funding and I see the current time as just another phase in the AI history cycle. In the words of AC/DC, `Money talks, everything else walks` - and once investors say they don't want AI, then that will be the driver. For now - Money says AI, and that's what we're going to do.

At some point "We use LLMs in our pipeline" will be the same as declaring that you use containers, Postgres or have CI/CD. The thing that matters will be what you do with it, and whether it is stable and secure.

Until that time comes - we as engineers are all figuring out our place in the world, and what our relationship with our tools is going to look like.

I have shaped an AI policy that reflects my values and should be workable in the real world, published [here](/ai-policy).

## How I use AI

* A simple Agents.md for common rules works well for most code bases - avoid bloat.
* Keep the skills files to a minimum - Only add core operations.
* Keep the context small and focused on task - My top two commands are `/new` and `esc`.
* Steering is key - watch it run, provide guidance into the dialogue stream.
* A code base should be navigable by humans. As a side effect, this will also be navigable by bots. We still need to pass audit - investors will want to see what is being done.
* Code tags are great for the bot to grep/discover. These are signposts in your code base. E.g. //BOT: Extend here for additional jobs.
* Sometimes I still write code by hand!! Refactor a bit, make a commit, run "Apply last commit to all classes implementing IDocumentFactory." *poof* magic.

## Test and Verify

I have always driven a strong testing culture. Most of my engineering effort has been focused on defining what correct looks like, and then proving that it does actually work that way.

From my experience, when we tackle our large engineering problems, code and code generation have never been the challenge. 90% of the engineering effort has always gone into proving that you have delivered what you have said you have delivered. No amount of automation makes that part go away, so how fast we can move with automation is limited by how fast we can verify it is correct.

In an age of AI, rapid development and equally rapid change - having robust test harnesses, test suites and observability is even more critical in enabling us to move safely at speed.

## Why keep blogging?

Sure - I could make a content factory loop, schedule a job running a bot, it's not a complicated process:

* Schedule a job
* Generate 5 subject ideas based off current tech trends for a persona (Eccentric bald lead engineer who likes hats)
* Pick the best idea and generate an extended script of the subject following the persona.
* Generate an audio track of the script from a sample voice pack.
* Generate a video from the script/audio using an image asset pack.
* Publish to YouTube once a week.

Content is easy, it's become noise. Now noise is easy, signal is hard. So mostly I write for me - as a journal of things I have built, a record of things I explored and learned, and ways of approaching problems that I find useful or interesting.

I will speak with my own voice and aim to be a home of something genuine, and maybe a signal in the sea of noise.

I'm keeping my old blog, and the old format, although I'll work on modernising the stack, as it provides history and content from the "before times" and a bridge into the modern AI era.

### Credits
I have always credited the photographer / artist behind images I used on my blog - but in the 4 years since my last post, finding anything made by a human has become... challenging.
Header photo by [Ryutaro Tsukata](https://www.pexels.com/photo/man-writing-with-pen-on-paper-6249385/)