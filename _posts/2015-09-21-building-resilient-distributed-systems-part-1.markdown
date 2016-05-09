---
layout: post
author: tristan-rhodes
title: Building Resilient Distributed Systems - Part 1
excerpt: This post is part one in the series of building resilience in a distributed environment, and how to improve a systems handling of transient errors.
featured-image: /assets/building-resilient-distributed-systems-part-1/featured-image.jpg
---

## Brief
This post is part of a series that covers the concept of resilience in a distributed environment, and how to improve a systems handling of transient errors.

## Posts
* [Part 1 - System failure and Resilience](/building-resilient-distributed-systems-part-1.html)
* [Part 2 - Implementing Resilience](/building-resilient-distributed-systems-part-2.html)
* [Part 3 - Benefits of Resilience](/building-resilient-distributed-systems-part-3.html)

## Resilience
Wikipedia - In computer networking: "**Resiliency** is the ability to provide and maintain an acceptable level of service in the face of faults and challenges to normal operation."

## What does this mean?
Large distributed systems are made up of many nodes, over many clustered layers that communicate in long chains of calls, either synchronously or asynchronously.

{: .page-image-wrapper }
![Successful Service Call](/assets/building-resilient-distributed-systems-part-1/successful-service-call.jpg){: .page-image }

The more nodes involved in a system, the greater the probability of a failure. A data center with 100,000 servers and millions of users will experience more hardware failures than a data center with 10 servers and a few hundred users. So what happens when one of these nodes fails?

{: .page-image-wrapper }
![Failed Service Call](/assets/building-resilient-distributed-systems-part-1/failed-service-call.jpg){: .page-image }

Our user gets this!

{: .page-image-wrapper }
![500 Error](/assets/building-resilient-distributed-systems-part-1/500-error.jpg){: .page-image }

## What causes failure?
So what kind of things can cause a transient error in our system?

* Hardware failure on a server in a cluster behind a load balancer.
* A record we expected to be loaded into cache has not yet been loaded. For example the end result of a work flow that has not yet been completed.
* When attempting to access a synchronized distributed cache, and there is a lock on the record (Common problem with AppFabric).
* Server is being updated by an automated deployment and is not ready to use, returns an unavailable message (503).
* Server has been saturated and has hit the limit of the maximum number of connections it can receive.

These are all errors that can happen in a large system, but need not be instantly fatal to a given request. Using resilience techniques, we can ensure the system better handles these events.

## What kinds of systems will benefit from resilience?
Some kinds of systems benefit much more from resiliency than others.

* Peer to peer and distributed systems
* Video chat / Streamed data
* Chat room services
* Our systems ;)

These systems all provide a feed of data and interruptions result in connections to the client being severed. Where the cost of re-establishing connections is high, then the lack of resilience is more pronounced.

## Conclusion
This covers what resilience is and what symptoms you can expect from a non-resilient system. In my next post, I will go over how you can implement resilience in your system.
