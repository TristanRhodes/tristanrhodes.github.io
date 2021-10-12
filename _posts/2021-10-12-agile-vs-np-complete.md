---
layout: post
author: tristan-rhodes
title: Agile vs NP Complete
excerpt: While agile and iteration is an essential part of software development, sometimes breaking things down doesn't work out how we expect.
featured-image: /assets/agile-vs-np-complete/featured-image.jpg
---

## Agile
Since the Agile Manifesto was signed in 2001, iterative software development has become the norm. We know we can't answer all of the questions involved in a software project before we start, so we must instead make a best guess, and move forward in small increments, evaluating after each one and adjusting course accordingly. Waterfall is dead.

Whether delivered in sprints or via a feed of Kanban tasks, this process is the core premise of agility. To always be near a working solution and to be able to respond to change rapidly.

I have worked in some form of Agile or another since around 2003, developed a strong affinity for Kanban and found the process of breaking things down to be essential to delivering any project, software or otherwise, successfully.

## NP-Complete
Generally, NP Complete problems are logic problems that have no efficient solution. As the complexity of the problem grows, the time taken to solve it increases exponentially.

> it is a problem for which the correctness of each solution can be verified quickly and a brute-force search algorithm can actually find a solution by trying all possible solutions. - [Wikipedia](https://en.wikipedia.org/wiki/NP-completeness)

In terms of developing software, this second description is more relevant:

> the problem can be used to simulate every other problem for which we can verify quickly that a solution is correct. In this sense it is the hardest of the problems to which solutions can be verified quickly so that if we could actually find solutions of some NP-Complete problem quickly, we could quickly find the solutions of every other problem to which a solution once given is easy to check. - [Wikipedia](https://en.wikipedia.org/wiki/NP-completeness)

So with NP-Complete type problems, we find that in order for a solution to be complete, it has to solve all sub problems. 

![NP Complete](/assets/agile-vs-np-complete/np-complete.jpg)

## Releasing Features

Normally when we extend major pieces of software, the majority of features are independent, so when we break down our tasks, we look at the a set of features, and we find they are mostly isolated from each other, and so when we plan and estimate them we do not need to take into account the workings of other features.

![Individual Releases](/assets/agile-vs-np-complete/individual-release.jpg)

## Releasing A Feature Set

In software that solves NP-Complete type problems, we find ourselves in the situation where in order for a feature to be viewed as complete, it has to implement all sub features completely, a partial implementation has no useful purpose. 

This doesn't only apply to software, there are plenty of real world examples where this definition is true, there's no such thing as a partially successful space flight.

![Individual Releases](/assets/agile-vs-np-complete/feature-set-release.jpg)

## Releasing An interconnected Feature Set

Expanding on this, when we are implementing something where all sub parts are in the the same domain, each feature implemented can affect the implementation of the other features around it, which can invalidate what you have already built, like turning a rubix cube to get one side complete, but breaking another. 

Take designing a high performance car, where all the individual considerations interact with each other.

![Individual Releases](/assets/agile-vs-np-complete/interconnected-feature-set-release.jpg)

## Agile Solution

Let's say you've got a big report system to write, so you break down your features, under the assumption you will iterate on the steps one at a time, and you figure each will take about a week.

- 1 Week: Base Report and Page
- 1 Week: Paginated Query with multi-part search parameters
- 1 Week: Obfuscated Query (Where obfuscation works with search params / excluded from parameter filters)
- 1 Week: Minimum performance (Where performance has to work with pagination and obfuscation)
- 1 Week: Transaction and Security checks on underlying record writes (While obfuscating some views on the read side without impacting performance)

Total => 5 Weeks

Sprint one goes well, you've implemented the new report.

As you start on sprint two, you start looking into how the paginated queries will need to work and what you will be searching on, you realise that the schema you put together for the first phase was missing parts, and you need to re-visit it. In order to support paging and add the new query parameters, you end up re-writing 75% of your original work. The sprint rolls over into the next one without a complete feature as you try to fix it up.

Next, you start to look at the obfuscation side of things, this means that you need to put masking in front of a number of sensitive fields, and when running search queries you need to take the masking behaviour into account. This drags you back to your previously complete pagination and query code, and requires extensive re-working of that, again pushing out your deadline.

As you pass the original deadline, you begin to realise that when you added the obfuscation to the report query, you did a lot of extra string formatting that hammered performance, and so you unwind some code and try a different approach, as the cycle of work continues.

At this point, your product owner is asking what's taking so long, and why you are spending so much time re-working existing code. And you haven't even realised that the report tables built so far will not support the locking model for the volume of writes needed, and you actually need to implement some kind of partitioning.

It's in these kinds of scenarios that I've found the short term iterations of Agile can really struggle. Without an element of high level overview, each sprint is just unveiling a new side of the 'solved' rubix cube that was previously unknown, one at a time.

<iframe src="https://giphy.com/embed/chOyZePGEHDoTSY2CA" width="480" height="367" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/tumblr-retrowave-fuzzyghost-chOyZePGEHDoTSY2CA">via GIPHY</a></p>

## Other Considerations

These kind of problems impact other aspects of project planning.

It's much harder to parallelise these kinds of tasks, as multiple generalists working independently of each other in the same domain can invalidate each others' solutions as they go, leading to conflicting requirements, re-work, frustration and likely burnout. In situations like this, specialisations can be more effective, like bringing together a materials expert, an engine expert, an aerodynamics expert and someone who understands car frame mechanics, under a capable coordinator in order to solve the car design problem.

Once it has gotten to the point where the outline of the solution is solved, parallelism becomes easier as the distinct parts of the system are well defined and isolated, and multiple generalists are able to operate in different isolated problem areas at the same time, without interfering with each others work. 

## Final Word

Whatever kind of problem you face, and however it is tackled, the hardest part is breaking it down in the first place. NP-Complete type problems can still be solved using an agile approach, it's just important to be aware that some groups of features cannot be logically isolated from each other and should instead be tackled as a complete set. The challenge is two fold, figuring out the complete higher-order solution, and explaining to the business why the release cycle is eight weeks instead of two.

### Credits
Credit to [Miguel A. Padrinan](https://www.pexels.com/@padrinan) as the source of the [free header image](https://www.pexels.com/photo/photo-of-golden-cogwheel-on-black-background-3785935/).