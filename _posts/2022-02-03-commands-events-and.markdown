---
layout: post
author: tristan-rhodes
title: Commands, Events, And?
excerpt: Three phase systems
featured-image: /assets/commands-events-and/featured-image.jpg
---

## Command and Event based systems

By now Command and Event based systems in software are ubiquitous and well understood. We can see them in WPF / React with UI commands and state change events, and we see it in distributed systems with Command and Event queues and the publish / subscribe model.

These systems have two types of messages, but are composed of three main concepts.

### State
The system state as it currently stands, this can be stateful and in memory, such as a React state / WPF model or stateless and out of process such as a database or document store tied to a set of function apps / lambdas.

### Commands
Commands are an instruction to modify state, they have not yet happened, but are an expression of intent to make something happen. Commands have one recipient, and they can fail, but when they succeed they have side effects.

### Events
Events are indications that the state has changed, they have happened, the state has been updated and the event provides a description and timestamp for the thing that has occurred. They can be broadcast to multiple recipients / subscribers. 

### Putting it together
This is easier to envision together with this simple diagram, flowing from left to right, you have commands to the left, modifying the current state in the middle, raising events on the right.

![Command, State, Event](/assets/commands-events-and/command-state-event.png)

### Making it real
While this is good, it's all still a bit abstract. Let's make it real, who likes food? We're all on the same page? Great! So let's go with a restaurant kitchen. We're going to map the above concepts to our kitchen:

* State - The Kitchen's current ingredients, available chefs (threads) and in progress dishes.
* Command - The orders coming in from the waiters. You'll see these queuing up on the shelf 
* Events - The dishes output by the kitchen. The Ack part is slapping the order ticket on the completed orders spike when the dish is picked up.

![Order, Kitchen, Dish](/assets/commands-events-and/kitchen-commands-events.png)

Bonus: Our kitchen analogy comes with built in queues, I don't even need to explain them.

### Are there other types of message?

So we've got two messages here, Commands / Orders that express intent / hunger, and Events / Dishes that express a side effect / satisfied customer, but what else is there in this Kitchen system?

### Ingredients

Well, these aren't commands, they don't do anything on their own, a Kiwi is not a command, and neither is a Steak. We'll receive them, and use them when we execute an order/command, otherwise they are inert.

When we receive ingredients, we have a couple of options:

* We could prepare them and freeze them
* We could put them in the fridge raw
* We could prepare a sauce as an ingredient for another dish
* We could prepare a dish to be served immediately

Whatever we do, we'll do based on our own context, not because of any specific instructions attached to our raw ingredient.

### Recipies

Recipes look kind of like commands at first glance, but we're not running them now, they are more a pre-defined set of steps we want to store as a template for use when running a command, so they aren't commands, but instruction sets.

We may un-pack those steps into individual commands to be run inside the context of the kitchen, and we may skip some steps depending on the context of the order (I'm not fond of mushrooms, so we'll leave them out), but as far as front of house is concerned these are invisible.

### Back of house

This is what's going on behind the scenes of our KitchenAPI:

![Order, Kitchen, Dish, Back of house](/assets/commands-events-and/kitchen-commands-events-messages.drawio.png)

### Document Message

So these are things that don't represent intent or side effects, but what would they look like in an engineering project?

They're just a plain message, a document, an inert packet of information or data that carries no context of its own. What the recipient does with the payload is up to it. 

* DataPoints - Sensor information may not necessarily be viewed as an event but a series of sampled absolute values.
* Frames - A video frame is not an event, but it is part of a feed that provides you with hours of entertainment.
* Documents - The complete works of Shakespere is not an event. Unless someone performs it, it's just a payload of data.
* Document Fragment - If a document is large and needs to be broken down, the parts of that document that are transmitted are not events either.

### Shoulders of giants

And of course, I don't get to claim any credit for [a new idea](https://www.enterpriseintegrationpatterns.com/patterns/messaging/DocumentMessage.html).

### As above, so below

It's always worth remembering that:

* The external systems that send commands to us, and subscribe to our events can also be command and event based systems with their own state. Like a rich client app talking to our server.
* The sub systems we use internally can also be command and event based systems with their own state. Like our server talking to a SQL database.

Any system we build is composed of other systems, we will always be sitting in the middle of something, between clients and storage, as API middle-ware, on hardware, across datacentres. 

### Finally

While we are thinking about our command sources and event stores, and running our user action and event storming workshops, it's always worth bearing in mind that you can still pass messages between systems that are neither commands nor events, and it's worth having some room to express these concepts in your system designs.

### And now

I'm hungry, so I'm going to run

> cook cheesburger-tacos

![Cheeseburger Taco]()

(No, there is no clean way to eat these)

### Credits

Art work - All stick man artwork is masterfully created by me in Draw.io... probably the wrong tool for the job.

Photo by [Rodion Kutsaiev](https://www.pexels.com/@frostroomhead?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/yellow-and-white-3-d-cube-9436715/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)