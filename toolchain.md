---
layout: default
title: Toolchain
permalink: /toolchain/
---

## Toolchain

This is a list of services, software, packages and images that I have experience with. It's still a work in progress, and the list is by no means complete.

## AWS
* [Lambdas](https://aws.amazon.com/lambda/) - Serverless scaley woo-woo from Amazon
* [DynamoDb](https://aws.amazon.com/dynamodb/) - Partitioned, hyper scale structured document store
* [Sqs](https://aws.amazon.com/sqs/) - Amazon queuing service
* [Kinesis](https://aws.amazon.com/kinesis/) - Amazon streaming service

## Azure
* [Function Apps](https://learn.microsoft.com/en-us/azure/azure-functions/) - Serverless scaley woo-woo from Microsoft (With slot deployments)
* [CosmosDb](https://azure.microsoft.com/en-gb/products/cosmos-db/) - Partitioned, hyper scale structured document store (With a real indexing system, and rich scripting support)
* [Service Bus](https://azure.microsoft.com/en-gb/products/service-bus/) - Microsoft queuing service
* [Event Hubs](https://learn.microsoft.com/en-us/azure/event-hubs/) - Microsoft streaming service

## General SaaS Platforms
* [DataDog](https://www.datadoghq.com/) - Logging, reporting and monitoring.
* [Octopus Deploy](https://octopus.com/) - Manage and ship releases to your environments.

## Storage Providers
* [Sql Server](https://www.microsoft.com/en-gb/sql-server/sql-server-2022) - _The_ microsoft database.
* [Postgres](https://www.postgresql.org/) - Open source DB, has really good geospacial support and integration with GIS.
* [MySql](https://www.mysql.com/) - Now owned by Oracle.
* [ElasticSearch](https://www.elastic.co/elasticsearch/) - Scaleable, indexed document store.
* [CosmosDb](https://azure.microsoft.com/en-gb/products/cosmos-db/)
* [DynamoDb](https://aws.amazon.com/dynamodb/)

## Queues and Streams
* [Rabbit MQ](https://octopus.com/) - Queues and topic server.
* [Service Bus](https://azure.microsoft.com/en-gb/products/service-bus/)
* [Event Hubs](https://learn.microsoft.com/en-us/azure/event-hubs/)
* [Sqs](https://aws.amazon.com/sqs/)
* [Kinesis](https://aws.amazon.com/kinesis/)

## Docker Images
* [Postgres](https://hub.docker.com/_/postgres/) - Nifty databases.
* [Seq](https://hub.docker.com/r/datalust/seq/) - Local log sink with simple dashboard.

## Dev Tools
* [Visual Studio](https://visualstudio.microsoft.com/) - My main IDE for .net.
* [VS Code](https://visualstudio.microsoft.com/) - The other IDE, that the main IDE doesn't know about.
* [NCrunch](https://www.ncrunch.net/) - Because tests while you type rocks.
* [Specflow](https://specflow.org/) - BDD testing framework.
* [Jekyll](https://jekyllrb.com/) - Ruby based blog engine.

## DotNet Packages

### Testing
* [XUnit](https://xunit.net/) - Runs unit tests.
* [Moq](https://github.com/moq/moq) - Create fakes, mocks and stubs.
* [Fluent Assertions](https://fluentassertions.com/introduction) - Assert... fluently.
* [Simmy](https://github.com/Polly-Contrib/Simmy) - Chaos Behavior based off Polly, throw errors and add latency.

### Resilience
* [Polly](http://www.thepollyproject.org/) - Configure retry policies.

### Text Processing
* [Sprache](https://github.com/sprache/Sprache) - Scannerless parsing library for turning text into objects.

### Observability
* [Serilog](https://serilog.net/) - Logging library with configurable sinks.
* [Reactive Extensions](https://github.com/dotnet/reactive) - Composable, observable streams.

### Time and Space
* [NodaTime](https://nodatime.org/) - Alternative Date/Time library. LocalTime, TimeZones, Instants, Durations and other useful primitives and calculations.
* [NetTopologySuite](https://github.com/NetTopologySuite/NetTopologySuite) - GIS mapping library.

### Behaviour
* [Stateless](https://github.com/dotnet-state-machine/stateless) - Lightweight state machine.
* [Workflow Core](https://github.com/danielgerlag/workflow-core) - Lightweight workflow engine.

### Storage / Serialization
* [Dapper](https://github.com/DapperLib/Dapper) - Ultra lightweight ORM from StackOverflow.
* [DbUp](https://dbup.github.io/) - Database setup and migration tool.
* [Newtonsoft.Json](https://www.newtonsoft.com/json) - An old classic, but not so relevant now it's integrated into netcore.