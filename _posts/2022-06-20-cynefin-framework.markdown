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

I love agile, iterations and the domain discovery process, but I've had to accept the conclusion that things are more nuanced than just pure agile iterations and discovery all the time.

I wanted to pitch this in terms of software development.

### Obvious

1. Sense - Determine what has occured.
2. Categorise - Select appropriate course of action.
3. Respond - With well known solution.

Known Knowns - We have a complete understanding of the problem domain, there are no gaps in our knowledge.

You expect a problem from a particular category, for example a user calls with a known bug report. It's a low priority bug where there's a workaround, you know what needs to be done, all the steps are pre-defined, repeatable, and known to either the individual or the organization (hey Confluence!), it's just a case of working through them.

We don't need agile for this. This is usually selecting a best practice/business process for a standard task and running it. You can ticket it up, and chuck it in a sprint, but there's nothing to iterate on, and the "estimates" are going to be accurate.

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

![Complicated](/assets/cynefin-framework/complicated.jpg)
Image by <a href="https://pixabay.com/users/peggy_marco-1553824/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Peggy und Marco Lachmann-Anke</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Pixabay</a>

#### Example

You are replacing a legacy stream processing component with a new stream processing component and it involves a complicated migration process. You need to plan the traffic routing from the old to the new, re-wire the queuing systems, ensuring state is synchronised across systems and transform any messages that get retried between the old/new system. The process is large and involved, but you're working with a well understood situation.

### Complex

1. Probe - Explore the problem, experiment-evaluate-repeat.
2. Sense - Generate a number of possible next steps. If nothing looks good, probe more if necessary.
3. Respond - Select the best possible next step. Move into complicated domain.

Unknown Unknown - We do not understand the full extent of the problem domain, we do not know there is knowledge to have gaps in.

Complex problem domains have multiple overlapping problems, with multiple potential solutions, and most likely no solution is a perfect fit for all problems. These situations are surprisingly common in business environments, there's a lot of ambiguity. 

![Complex](/assets/cynefin-framework/complex.jpg)
Image by <a href="https://pixabay.com/users/peggy_marco-1553824/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Peggy und Marco Lachmann-Anke</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2320124">Pixabay</a>

#### Example

You are building a logistics management system that tracks the progress of products in a supply chain. As the supply chain is made up of many transport providers, some you may be un-aware of, who all use different API's and standards and provide different tooling, you cannot know what is involved up front.

This scenario will produce many experiments and false starts, you will need to investigate all your integration partners and figure out how to connect with them and if they can be aggregated, there will me multiple routes to the same information, and you will need to experiment to find the right common abstractions for your system. From there, you still need to come up with a best fit solution.

Only when you have a plan and a roadmap are you able to say you are now in the Complicated domain.

### Chaos

1. Act - Fix the immediate problem. Stem the bleeding.
2. Sense - Figure out what happened. Assess where you are. Determine available options.
3. Respond - Take action to move to another domain.

This is the emergency triage phase. Your major incidents, the P1's. 

![Chaos](/assets/cynefin-framework/chaos.jpg)
Image by <a href="https://pixabay.com/users/geralt-9301/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2980845">Gerd Altmann</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2980845">Pixabay</a>

#### Example

Your API call counter has suddenly dropped to zero for unknown reasons. If you run a zero down time business, you need to fix this _now_, regardless of how you go about it. If you need to give your lead engineer production access to manually terminate a deadlock in a database, and keep watching for it to occur again while someone else hacks out a hotfix and rushes out a deployment without the endurance tests, you do that.

Now 'a' fix is out, you need to complete your post incident review process, the Reason for Outage, Incident Report, Production Outage Report, Root Cause Analysis or whatever the flavour you have in house, devise a proper fix, create a plan, and execute on that.

### Disorder

You have no idea what's going on, what the problem is, what you are trying to achieve. Have a cup of tea, a chocolate digestive, and think about your domain.

## Credit

Header Image by <a href="https://pixabay.com/users/geralt-9301/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2692466">Gerd Altmann</a> from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2692466">Pixabay</a>