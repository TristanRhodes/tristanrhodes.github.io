---
layout: post
author: tristan-rhodes
title: Maturity Levels
excerpt: This post covers project and team maturity levels in Ve
featured-image: /assets/ve-maturity-levels/featured-image.jpg
---

## Brief
Last year at Ve, we embarked on a massive code restructuring from a monolith TFS repository to granular Git repos, and a migration from fixed data centre infrastructure to cloud (at the same time...). This worked well in terms of enabling us to scale the company, and we now find ourselves with a large number of teams, working on an even larger number of projects.

The distribution of projects is not even, some teams will have one project with the sole objective of maintaining high velocity throughput, others will work on multiple projects where the focus is on delivering new UI features rapidly, and still others work on multiple API projects which are focused on coordinated releases and avoiding breaking changes.

This breakup has highlighted the broad spectrum of quality across projects, and we needed a way of grading our projects so we could better channel effort into improving processes, infrastructure and automation where it would be most effective. To this end, we created a number of maturity level categories each project would be graded against. The three categories are:

* Environment
* Deployment
* QA

In this post I'm going to go over what the different levels are for maturity, and what is involved in reaching them.

## Environment Maturity

Environment maturity covers the quality of infrastructure and the level of automation in place. At the lowest level, we have a physical box and with manual setup and install required, going all the way to fully automated Infrastructure As Code deployments with elastic scaling and intelligent failure management.

{: .page-image-wrapper }
![Environment Maturity Levels](/assets/ve-maturity-levels/environment-maturity-levels.jpg){: .page-image }

* **(0)** Single production environment with manual deployment.
* **(1-6)** Seperate CI / Pre / Production environments with deployments using packages with only configuration changes per environment.
* **(2-6)** Infrastructure as Code, with all elements of infrastructure defined via ARM templates and Puppet Scripts.
* **(3-6)** Automated deployments using Octopus deploy.
* **(4-6)** Spin up an entire new environment through one click process.
* **(5-6)** Environment withstands security and penetration tests.
* **(6)** Cross datacentre environments with failover support to switch to alternative in the event of failure.
* **(6)** Elastic scaling. When our system recieves heavy load, allocate additional resources to ensure we can handle this.

## Deployment Maturity

Deployment maturity is, at least for me, the most exciting one. As a developer, I love writing code, but code on its own is useless without a delivery mechanism (oh and storage / data, but whatever :) ). So Deployment maturity is all about being able to ship code that you trust is robust to production in the shortest period of time, with the minimum amount of disruption. This is really about driving the DevOps culture into teams and getting everyone on-board with looking after their own deployment pipelines and systems monitoring. (I may spend too much time looking at the pretty graphs that tell me our app is not broken.)

{: .page-image-wrapper }
![Deployment Maturity Levels](/assets/ve-maturity-levels/deployment-maturity-levels.jpg){: .page-image }

* **(0-1)** Manual testing required.
* **(0-2)** Operations involvement required.
* **(1-5)** Deployment are fully automated.
* **(2-5)** Level of automation testing is considered satisfactory to push the new features live without manual testing.
* **(3-5)** System and application monitoring present.
* **(3-5)** Deployment controlled by team instead of operations.
* **(3-5)** Use of feature toggling to manage active features centrally.
* **(5)** Deployment done anywhere at anytime.

## QA Maturity

QA maturity covers the quality of testing and test infrastructure. This ranges from no testing, through manual testing, unit and integration testing, all the way up to full test automation with performance and penetration testing as part of our deployment pipeline.

{: .page-image-wrapper }
![QA Maturity Levels](/assets/ve-maturity-levels/qa-maturity-levels.jpg){: .page-image }

* **(1-4)** Manual testing done by team during sprints.
* **(2-4)** Regression after sprint is done as exploratory experience driven testing.
* **(2-7)** Developers write unit tests for new users stories.
* **(3-7)** The team write automated acceptance tests for new users stories in a BDD style.
* **(4-7)** The automated acceptance tests run in the project CI pipeline.
* **(5-7)** Enough confidence in automation to potentially release to production with no manual intervention.
* **(6-7)** Application Monitors in place that monitor the health of your application in all environments.
* **(7)** Automated Performance Testing scheduled and/or in CI pipeline.
* **(7)** Automated Security Testing scheduled and/or in CI pipeline.

## Wrap up
That's how we rate our projects at Ve. Some projects are still very much a work in progress and slowly climbing the maturity levels, others are pretty much the top of the scale, as is expected in any large company.

So if that sounds interesting, and:

* If you enjoy writing tests and watching everything go green makes your day.
* If you love continuous deployment and metrics make you excited.
* If infrastructure as code appeals to you and elastic scaling under load sounds _really_ cool.
* Or even if you just like watching your deployments go live on the big screen with your mates and a cold beer in your hand.

Then drop us a line, we are [hiring](https://www.veinteractive.com/about-us/careers/).

### Credits
Images credit: Credit to Simon Larkin for the maturity level overview and images.

Photo credit: <a href="https://www.flickr.com/photos/58376723@N07/5359060059/">karmabomb1</a> via <a href="http://foter.com/">Foter.com</a> / <a href="http://creativecommons.org/licenses/by-nc-nd/2.0/">CC BY-NC-ND</a>