---
layout: post
author: tristan-rhodes
title: Commands, Events, And?
excerpt: Is there anything else?
featured-image: /assets/commands-events-and/featured-image.jpg
---

## Command and Event based systems

By now, Command and Event based systems in software are ubiquitous and well understood. We can see them in WPF / React with UI commands and state change events, and we see it in distributed systems with Command and Event queues and the publish / subscribe model.

These systems have two types of messages, and are composed of three main concepts.

### State
The system state as it currently stands can be stateful and in memory, such as a React state / WPF model, or stateless and out of process such as a database or document store tied to a set of function apps / lambdas.

### Commands
Commands are an instruction to modify state: they have not yet happened, but are an expression of intent to make something happen in the future. Commands have one recipient and they can fail, but when they succeed they have side effects.

### Events
Events are indications that the state has changed. The state has been updated and the event provides a description and timestamp for the thing that occurred in the past. They can be broadcast to multiple recipients / subscribers. 

### Putting it together
In this diagram, flowing from left to right, you have commands on the left, that are modifying the current state in the middle, which are raising events on the right.

![Command, State, Event](/assets/commands-events-and/command-state-event.png)

### Making it real
While this is good, it's all still a bit abstract. Let's make it real - who likes food? We're all on the same page? Great! So let's go with a restaurant kitchen. We're going to map the above concepts to our kitchen:

* State: the kitchen's current ingredients, available chefs (threads) and in-progress dishes.
* Commands: the orders coming in from the waiters. You'll see these queuing up on the shelf.
* Events: the dishes output by the kitchen. The Ack part is slapping the order ticket on the completed orders spike when the dish is picked up.

![Order, Kitchen, Dish](/assets/commands-events-and/kitchen-commands-events.png)

Bonus: Our kitchen analogy comes with built in queues; I don't even need to explain them.

### What else is there?

We've got two messages here: Commands / Orders that express intent / hunger, and Events / Dishes that express a side effect / satisfied customer. What else is there in our Kitchen system?

### Ingredients

These aren't commands and they don't do anything on their own - a tomato is not a command, and neither is a steak. We'll receive them and use them when we execute an order/command and may issue low stock alerts when they run low, but the ingredients themselves are inert.

When we receive ingredients, we have a couple of options:

* We could prepare them and freeze them
* We could put them in the fridge raw
* We could prepare them in a sauce as an ingredient for another dish
* We could prepare a dish to be served immediately.

Whatever choice we make we'll do based on our own context, not because of any specific instructions attached to our raw ingredient.

### Recipies

Recipes look kind of like commands at first glance, but the key difference is that we're not running them now - they're a pre-defined set of steps we want to store as a template, and execute when we run a command.

We may unpack those steps into individual commands to be run inside the context of the kitchen, and we may skip some steps depending on the context of the order (I'm not fond of mushrooms, so we'll leave them out), but as far as front of house is concerned these are invisible.

### Behind the scenes

This is what's going on behind the scenes of our KitchenAPI:

![Order, Kitchen, Dish, Back of house](/assets/commands-events-and/kitchen-commands-events-messages.drawio.png)

### Wait, what's that?

Our orders and dishes are all we want expose to our front of house - or in other words, our commands and events are all we want to surface from our KitchenAPI.

But there are clearly other things happening behind the scenes, because guacamole doesn't appear out of thin air. These aren't commands/orders, as we are not representing intent. Nor are they events/dishes, as we are not indicating something has happened. What we receive as the payload _is_ the thing that is happening, and that thing can have side effects in its own right.

![Ingredient Delivery](/assets/commands-events-and/ingredient-delivery.drawio.png)

What are they in a software context, these concepts that aren't commands or events?

### Just a message

They're just a plain message, a document, an inert packet of information or data that carries no context of its own. What the recipient does with the payload is up to them. A couple of real world examples:

* Data points: instead of sensor information as a sequence of events, they can be viewed as a series of sampled absolute values.
* Frames: a video frame is not an event, but it is part of a feed that provides you with hours of entertainment.
* Documents: the complete works of Shakespeare are not an event. Unless someone performs it, it's just a payload of data.
* Document fragment: if a document is large and needs to be broken down, the parts of that document that are transmitted are not events.
* Workflow: a self-contained workflow can be a payload in its own right, and when processed may generate commands and events as part of its transformation while not be either itself.

### Shoulders of giants

And of course, [nothing new here](https://www.enterpriseintegrationpatterns.com/patterns/messaging/DocumentMessage.html).

### Bottom line

We're all building first-class command and event-based systems. When thinking about our command sources and event stores, and while running our user action and event storming workshops, it's always worth bearing in mind that it's still valid to pass messages between systems that are neither commands nor events - and it's worth having room to express these concepts in your system designs.
 
### Finally

I'm hungry, so I'm going to run

> kitchen exec prepare cheeseburger-taco

Aaaand....

![Cheeseburger Taco](/assets/commands-events-and/taco_taco_taco.jpg)

(No, there is no clean way to eat these)

### Credits

Art work - All stick man artwork is masterfully created by me in Draw.io... probably the wrong tool for the job.

Main photo by [Rodion Kutsaiev](https://www.pexels.com/@frostroomhead?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels) from [Pexels](https://www.pexels.com/photo/yellow-and-white-3-d-cube-9436715/?utm_content=attributionCopyText&utm_medium=referral&utm_source=pexels)