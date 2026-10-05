---
layout: post
author: tristan-rhodes
title: Cynefin Framework
excerpt: Decision making in the software business
featured-image: /assets/cynefin-framework/featured-image.jpg
---

I recently came across the a useful thought tool, the Cynefin framework.

![Cynefin Framework](/assets/cynefin-framework/Cynefin.png)

(Image from [Wikipedia](https://en.wikipedia.org/wiki/Cynefin_framework))

I love agile, iterations and the domain discovery process, but I've had to accept that things are more nuanced than just pure domain discovery and iteration all the time.

I wanted to pitch this framework in terms of software development and how it relates to agile, but first lets go over the main quadrants in detail.

### Obvious

1. Sense - Determine what has occurred.
2. Categorize - Select appropriate course of action.
3. Respond - With well known solution.

Known Knowns - We have a complete understanding of the problem domain, there are no gaps in our knowledge.

You expect a problem, or want to solve a task that has been done before and is well understood. There's no discovery work, you just select the action plan and execute. 

These tasks aren't agile in the sense that you need to explore, spike and estimate, they still need to fit into an agile work practice, but you just spin up the tickets, if they need story points, you have a standardised ones for these, and they just go on the backlog and feed into the sprint.

![Simple](/assets/cynefin-framework/simple.jpg)
Image by [Vicky Tran](https://www.pexels.com/photo/person-standing-on-arrow-1745766/)

#### Example 

You are setting up a new project, your company has a standardized approach towards creating repository templates, getting access to an environment, configuring new build and deploy pipelines, etc. The process is documented and well understood, parts of it are automated, all you need is a name and a couple of days.

### Complicated

1. Sense - Identify the problem.
2. Analyze - Break it down and plan out the steps to solve it.
3. Respond - Build roadmap and execute.

Known Unknown - We have a good understanding the problem domain and know where there are gaps in our knowledge, there may be a couple of different ways of solving the problem.

Complicated tasks are bigger, and have a larger scope, but they are not so big that you can't plan all of them with a good degree of accuracy. The arrangement of the parts may be new, but there are no new or unknown parts.

These kinds of tasks will produce a number of tickets that are easily tackled, and a number that require some spiking and investigation. The estimates should be reasonably accurate because the plan is well formed and should look mostly like the expected outcome.

![Complicated](/assets/cynefin-framework/complicated.jpg)
Image by <a href="https://pixabay.com/users/peggy_marco-1553824/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Peggy und Marco Lachmann-Anke</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Pixabay</a>

#### Example

You are replacing a legacy stream processing component with a new stream processing component and it involves a complicated migration process. You need to plan the traffic routing from the old to the new, re-wire the queuing systems, ensuring state is synchronised across systems and transform any messages that get retried between the old/new system. The process is large and involved, but you're working with a well understood situation and with a number of domain experts.

### Complex

1. Probe - Explore the problem, experiment-evaluate-repeat.
2. Sense - Generate a number of possible next steps. If nothing looks good, probe more if necessary.
3. Respond - Select the best possible next step. Move into complicated domain.

Unknown Unknown - We do not understand the full extent of the problem domain, we do not know there is knowledge to have gaps in.

Complex problem domains have multiple overlapping problems, with multiple potential solutions, and most likely no solution is a perfect fit for all problems. These situations are surprisingly common in business environments, there's a lot of ambiguity. 

These kinds of tasks can have wildly different outcomes to expected. Some things can be broken down and estimated, but there can be lots of spikes to prove theories that may affect the breakdown of other tickets. 

![Complex](/assets/cynefin-framework/complex.jpg)
Image by <a href="https://pixabay.com/users/peggy_marco-1553824/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Peggy und Marco Lachmann-Anke</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Pixabay</a>

#### Example

You are building a logistics management system that tracks the progress of products in a supply chain. As the supply chain is made up of many transport providers, some you may be un-aware of, who all use different API's and standards and provide different tooling, you cannot fully understand what is involved up front.

This scenario will produce many experiments and false starts, you will need to investigate a number of your integration partners and figure out how to connect with them and if they can be aggregated, there will me multiple routes to the same information, and you will need to experiment to find the right common abstractions for your system. From there, you still need to come up with a best fit solution.

Only when you have sufficient information to structure a well defined plan and roadmap are you able to say you are now in the Complicated domain.

### Chaos

1. Act - Fix the immediate problem. 
2. Sense - Figure out what happened. Assess where you are. Determine available options.
3. Respond - Take action to move to another domain.

This is the emergency triage phase. Your major incidents, the P1's. Your first action needs to be to fix the immediate problem. If the patient dies or company fails, you won't have to worry about long term planning.

As far as agile goes, these would be the "fix bugs first" type issues. If it's a P1/P2, you won't be spinning up tickets and having planning meetings. You have a war room running on Saturday afternoon until it's resolved to the point where you can pick it up on Monday.

![Chaos](/assets/cynefin-framework/chaos.jpg)
Image by <a href="https://pixabay.com/users/geralt-9301/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2980845">Gerd Altmann</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2980845">Pixabay</a>

#### Example

Your API call counter has suddenly dropped to zero for unknown reasons. If you run a zero down time business, you need to fix this _now_, regardless of how you go about it. If you need to give your lead engineer production access to manually terminate a deadlock in a database, and keep watching for it to occur again while someone else hacks out a hotfix and rushes out a deployment without the endurance tests, you do that.

Now 'a' fix is out, you need to complete your post incident review process, the Reason for Outage, Incident Report, Production Outage Report, Root Cause Analysis or whatever the flavour you have in house, devise a proper fix, create a plan, and execute on that.

### Disorder

Finally, the center of the quadrants, the state that falls between the cracks - Disorder.

You have no idea what's going on, what the problem is, what you are trying to achieve. Have a cup of tea, a chocolate digestive, and think about what you want/need to achieve.

## Agile

So, how does this relate to agile and making business de

* How does this relate to agile?
* How does this affect our ability to plan?
* Probability of failure?
* Estimation Accuracy?
* What should the business expect from each state?
* How do you determine what your state is and pitch to the business?


## Credit

Header Image by <a href="https://pixabay.com/users/geralt-9301/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2692466">Gerd Altmann</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2692466">Pixabay</a>