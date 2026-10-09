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

I'm familiar with the cycle of excitement and overinvestment, followed by the pullback of funding and I see the current time as just another phase in the AI history cycle. In the words of AC/DC, Money Talks, Everything else Walks - and once investors say they don't want AI, then that will be the end of it. For now - Money says AI, and that's what we're going to do.

At some point "We use LLM's in our Pipeline" will be the same as declaring that you use Containers, Postgres or have CI/CD. The thing that matters will be what you do with it, and whether it is stable and secure.

Until that time comes - we as engineers are all figuring out our place in the world, and what our relationship with our tools is going to look like.

I have tried to define an AI Policy that reflects my values and will be workable in the real world, published [here](/ai-policy).

## How I use AI - The light touch.

* A simple Agents.md for common rules works well for most code bases - avoid bloat.
* Keep the skills files to a minimum - Only add core operations.
* Keep your context small and focused on task - My top two commands are `/new` and `esc`, in process steering is frequent.
* A code base should be navigable by Humans. As a side effect, this will also be navigable to bots. We still need to pass audit.
* Code tags are great for the bot to Grep/Discover. These are signposting in your code base. Eg //BOT: Extend here for additional jobs.

## WAT

There is a huge difference between proving something works, and proving something works as intended - and no amount of AI code generation can solve either. This is always where the time falls. I have worked in teams shipping 10+ changes a day to production from 10 years ago, and this is only achievable if you know what you are aiming for and have the tests and monitoring in place to confirm you are going in that direction.

I see C-Suites being handed 600 pages of Business Proposals composed of incomprehensible word salad, they run that through a bot to summarise down to 200 pages - but that's not human digestible either and now not even the top level of the company understand the direction of travel.

I do see some changes - Managers and Engineers being hired to "Handle the slop" and control the volume of noise - but this can't be done while having conversations incentivising metrics tied to code generation, volume of PR's, and AI Token spends where people are driven to maximise all three.

I do not see specs or skills.md files as a source of truth - the code is still the source of truth, it still holds the actual behaviour and the actual tests. I don't think we should try to externalise these concepts as heavily, but instead provide a high level Agents.md file and ensure that the codebase is consumable by humans, but also navigable / sign-posted for AI navigation. Baked in //BOT: tags to support grepping the code base.

There seems to be a prevailing assumption that now people have the bot - all work is instant. But there is still iteration, an investment of time, refinement to shape output closer to the goal. We cannot automate vision, intent or drive.

I will admit - I still occasionally write code by hand. It's rare, very rare now, but I do feel that sometimes I have a change in mind, and I can't find the words to describe it. It might not be complicated, but I find that I can't easily articulate it as steps. In these instances, I find making a small change, committing it, and saying "Hey bot, apply the refactor in the last commit to all classes with the IDocumentFactory interface.". This to me is the _best_ use case, clear intent, automated bulk apply.

The bot still needs to navigate your code base - and context is limited. So the bot will only grep/sample the code - it only really sees the code it searches for and some adjacent parts. If it needs more, it needs to re-visit and iterate. A huge bloated code base is hard for the bot to navigate, as well as the human.

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

I'm keeping my old blog, and the old format, although I'll work on modernising it, as it provides history and content from the "before times" and a bridge into the modern AI era.