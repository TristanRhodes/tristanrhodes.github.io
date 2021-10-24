---
layout: post
author: tristan-rhodes
title: Solving Problems
excerpt: As engineers our job is to solve problems, but the first part of solving a problem is to define it.
featured-image: /assets/solving-problems/featured-image.jpg
---

## Problems for Business

As engineers, problems are our life. Companies have them and we want to fix them, ideally by being paid for our services. If you have ever worked for motivational bosses who say things like "I don't want to hear about problems, I want to hear about solutions", you just know they don't really understand what engineers do.

Without problems, we can't provide solutions.

## Well-Defined Problems

We value being able to solve problems. It's our skill, our hobby, our job. Complex problems are interesting and usually arise as a result of common, real world challenges. 

During the Bronze Age, assembling large stone blocks into pointy mounds in the desert was an example of a real-world challenge efficiently solved and implemented [by teams of professional engineers](https://historyofyesterday.com/myth-debunked-the-pyramids-were-not-built-by-slaves-3d6b3a84337d).

![The Pyramids](/assets/solving-problems/the-pyramids.jpg)

Photo by [David McEachan](https://www.pexels.com/@davidmceachan?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/gray-pyramid-on-dessert-under-blue-sky-71241/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)

Problems also make great metrics for evaluating potential engineers. When we hire, we use problem solving as a litmus for new candidates, using algorythmic and system design problems.

We can test for understanding of things like string manipulation, list sorting, tree traversal, graph walking and so on. These are easy to describe as a problem to someone and ask them to implement a solution, although the problems have generally been around long enough that the solution is well known and optimised.

Beyond this, there are more complex problems such as game state scenarios; Chess and Go are two examples that have more complex rules / depth of domain. These problem domains have well-understood rules and there are systems that solve them more effectively than a human.

![Chess](/assets/solving-problems/chess.jpg)

Photo by [Vlada Karpovich](https://www.pexels.com/@vlada-karpovich?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/a-boy-playing-chess-6115011/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)

These problems are solvable because we understand them, which means we have a mental model that we can describe to others in order to collaborate on the implementation of a solution. The state of the game is finite, the transforms are known, and can be described and codified. Once codified the state can be put into the appropriate data structure and the rules executed in the appropriate algorithm, and we can automate the generation of the (best fit) solution.

## Poorly Defined Problems

But what if you don't understand the problem, only the symptoms? What if you can't explain what you see as you have no vocabulary to describe it, and where you can describe a concept, it is based off other concepts combined together that also need to be conveyed first?

Donald Rumsfelt's infamous "Unknown Unknowns" is relevant here. That is just one part of a four-part matrix:

* Known Knowns (We know how many planes they have.)
* Known Unknowns  (We don't know what that is, but they sure have a lot of them.)
* Unknown Knowns (We know they have planes, but we don't know how many they have.)
* Unknown Unknowns (Look, shiny birds!)

The deeper you are in unknown territory, the less you know about the state of the problem and how it looks. You don't know what the rules are or how to apply them. You don't know what success looks like and have no idea what the end goal is. There's no heuristic you can use to determine where you are right now, but you have to start somewhere, and part of that is breaking things down.

![Confusion](/assets/solving-problems/confusion.jpg)

Image by [ElisaRiva](https://pixabay.com/users/elisariva-1348268/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1922477) from [Pixabay](https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=1922477)

To begin tackling this, you need a mental scaffold on which to build your model, and onto this framework you will create a language that can be shared and understood by multiple parties. This ties into two concepts that we use regularly in software Domain Specific Languages, and Domain Driven Design.

## Domain-Specific Language

> A Domain-Specific Language (DSL) is a programming language with a higher level of abstraction optimized for a specific class of problems. A DSL uses the concepts and rules from the field or domain. - [JetBrains](https://www.jetbrains.com/mps/concepts/domain-specific-languages/)

While the quote above talks about programming languages specifically, the concept does not need to be related to programming. I currently work in the field of mobility, and parking rules are incredibly complex. Part of describing how these work involved defining a shared company language that provided common meaning:

- Bay: a place to Park
- Vehicle: a cars, van, motorcycle or HGV
- Operating Hours: the time periods a bay is accessible for parking
- Tariff: the cost structure applicable for a given time period
- Permit: a right to park for a given reason, such as a disabled badge, resident permit, pre-payed voucher, commercial licence, etc.
- Fuel: petrol, diesel, electric

These terms and many others enable us to define and share the concepts of interest in the parking domain, and form the foundation used to describe our system. The first part of identifying any problem is putting a name to the things you're looking at.

Using the language we defined above, we can now describe our problem as: 

_When we are looking to park a vehicle, we need to calculate the availability and price of parking in a bay for a period of time, where we take into account the rules around fuel and vehicle types, permits, discounts etc._

## Domain-Driven Design

> Domain-Driven Design (DDD) is a set of principles and schemes aimed at creating optimal systems of objects. The development process boils down to creating software abstractions called domain models. These models include business logic that links the actual conditions of a product's application to the code. - [domaindrivendesign.org](https://domaindrivendesign.org/ddd-domain-driven-design/)

Once you have your DSL layered on top of your conceptual framework, you are ready to start defining a system to solve the actual problem. Now that you can describe the problem domain in words, you can communicate with others to define the building blocks of the system.

![Design](/assets/solving-problems/diagram.jpg)

Photo by [Christina Morillo](https://www.pexels.com/@divinetechygirl?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/white-dry-erase-board-with-red-diagram-1181311/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)

Applying this process to the parking domain shed light on a number of important concepts:

- Parking Session: a period of time a vehicle is parked which may or may not require payments
- Payment: payment for a parking session
- Occupancy: is a bay occupied? If a multi-bay, how many spaces are occupied?
- Authority: the top-level legal entity responsible for bays on a road

The backbones of our systems then evolved from these these basic domain concepts:

- Session Management: a system that manages the start and end of parking sessions and notification of various events / milestones.
- Session Pricing: a system for determining occupancy options and price ranges.
- Payments: a system for managing payment options for, and taking payments from, the end user.
- Map Editor: a system for authorities to edit their parking bays.
- Availability: a system for calculating the occupancy of a bays from a variety of Internet of Things sensors / inputs.

These compose the core ecosystem of AppyWay and represent the end result of refining a problem domain using DDD. The subject of Domain-Driven Design is a large and already written about at length. Head over to [domaindrivendesign.org](https://domaindrivendesign.org/) for a deeper dive.

## Conclusion
Being a good engineer isn't just about our ability to solve hard problems. We need to be able to identify them in the first place, define them and frame them in terms that the business and colleagues understand - and only then do we get to the implementation and solution. While building the solution is almost always the fun part, the amount of work required to frame and present the problem should not be overlooked.

### Credit
Header Image by [fauxels](https://www.pexels.com/@fauxels?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/people-discuss-about-graphs-and-rates-3184292/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)