---
layout: post
author: tristan-rhodes
title: Observability
excerpt: It can get expensive...
featured-image: /assets/observability/featured-image.jpg
---
## What is Observability

I looked around for an official defintion of Observability, and it appears to be one of those things that doesn't have an official easy soundbite description. Best I found was one from IBM:

> Observability provides deep visibility into modern distributed applications for faster, automated problem identification and resolution. - [IBM](https://www.ibm.com/cloud/learn/observability)

It's a nuanced topic, there's a lot going on. Some take the view that observability is being able to obtain complete systems visibility over your entire domain, at all times, purely from the system output. I'm not entirely sure I agree, it needs to be accessible, but let's unpack it.

When we talk about observability, we're talking about viewing how our application runs. We've come a long way from this:

![Windows Message Box Error]()

Our systems are distributed now, we've got microservices everywhere, so many containers that we're struggling with the right collective noun and we host them in the cloud so it's all magical woo-woo, on top of that, there's no all purpose dashboard. So how do we know what's going on?

### Logs
Records of events, warnings, messages, information published by your system.

### Tracing
Correlating the flow of logs through different parts your system and being able to view a sequence.

### Metrics
Counters representing guages, cumulative values, averages over time, etc.

## Observability at scale


### Credit
Header Image by <a href="https://pixabay.com/users/msporch-4201628/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2488227">Michael</a> from <a href="https://pixabay.com//?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=image&amp;utm_content=2488227">Pixabay</a>

# NOTES

* Observability is great, it's how we are able to view the status of our application in production.
* Important: Observability can leak PII. Sanitisation is critical. Prefer dedicated, safe logging objects instead of writing out domain entities.
* Verbose Observability is _very_ chatty, and expensive. It can cost a lot for bandwidth and log management SaaS costs. (Looking at you DataDog)
* Verbose Observability at scale can compete with your core network traffic, this can degrade unrelated services and result in warnings and alerts in unexpected parts of the system.
* Partial visibility / Fog of War
* Sampling
* Sometimes you can't log everything, you have to instead have counters to monitor health.
* If you have counters as your observability, you can still access the raw logs, but these logs will need to be retained at the node level, rather than centrally logged.
* These log files will generally be generated into time buckets (eg, hourly) with a retention period.
* If your logs are stored on the node, you can automate analysing them for information when performing an investigation.

