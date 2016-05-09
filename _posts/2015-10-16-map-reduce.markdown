---
layout: post
author: tristan-rhodes
title: Map Reduce
excerpt: As functional programming is a growing paradigm - specifically within Ve - I wanted to cover one of the core concepts&#58 MapReduce
featured-image: /assets/map-reduce/featured-image.jpg
---

## What is MapReduce?
MapReduce is a two-phase process for working with large, read-only (immutable) datasets. Funnily enough, the first phase is called Map, which generates key:value pairs of data. The second is Reduce, which takes the key:value pairs and reduces them to the desired end result. 

{: .page-image-wrapper }
![Map-Reduce Process](/assets/map-reduce/Map-Reduce-Process.jpg){: .page-image }

This is an old concept tied to functional languages such as Haskell. Because of its functional nature and the immutable input, MapReduce is massively parallelisable and can be scaled very easily. More modern implementations such as Google MapReduce and Apache Hadoop provide powerful distributed solutions to the MapReduce problem. 

Basic MapReduce can be implemented using a variety of different language features, including those in C#, and can be run locally or in larger distributed enviroments.

## Example of MapReduce in action
So, we have a big set of data representing our customers and we want to run some queries against it. We can represent our individual records with the following C# class:

{% highlight csharp %}

public class Customer
{
    public int Id { get; set; }

    public int Age { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1} {2}, Age: {3}", Id, FirstName, LastName, Age);
    }
}

{% endhighlight %}

From this dataset, we want to determine the youngest, oldest and average age of people, grouped by their last name. For example, at the end of our query we want to see a number of records that look like this:

	Hooper - Youngest: 1, Oldest: 96, Average: 62.16

Given that we have our set of customers in memory, we can start with the Map phase. Here, we scan the dataset and for each person we generate a key:value pair of Customers grouped by LastName.

{% highlight csharp %}

// Map Phase - Group our customers by last name
var mappedCustomers = customers
            .GroupBy(c => c.LastName) // Group by last name here.
            .ToList(); // NOTE: This prevents multiple enumerations.

// Print our mapped results
Trace.WriteLine("===Mapped===");
foreach (var mapped in mappedCustomers)
{
    Trace.WriteLine(string.Format("{0} ({1})", mapped.Key, mapped.Count()));
}

{% endhighlight %}

This generates our grouped customers, where we have put our customers into buckets based on their last name. The trace output looks like this:

	===Mapped=== 
	Hooper (19) 
	Fisher (26) 
	Clarke (17) 
	Baker (25) 
	Smith (13) 

Now that we have our grouped data, we can process it to get our desired outcome. In this case we are looking at the minimum, maximum and average of the ages in each of the mapped buckets.

{% highlight csharp %}

// Reduce phase: Calculate average age for groups
var reducedCustomers = mappedCustomers
            .Select(c => new
            {
                LastName = c.Key,
                Oldest = c.Max(v => v.Age),
                Youngest = c.Min(v => v.Age),
                Average = c.Average(v => v.Age)
            });

// Print our reduced results
Trace.WriteLine("===Reduced===");
foreach (var reduced in reducedCustomers)
{
    Trace.WriteLine(string.Format("{0} - Youngest: {1}, Oldest: {2}, Average: {3:0.##}", 
                    reduced.LastName, reduced.Youngest, reduced.Oldest, reduced.Average));
}

{% endhighlight %}

	===Reduced===
	Hooper - Youngest: 1, Oldest: 96, Average: 62.16
	Fisher - Youngest: 3, Oldest: 96, Average: 47.58
	Clarke - Youngest: 3, Oldest: 95, Average: 47.94
	Baker - Youngest: 4, Oldest: 96, Average: 51.36
	Smith - Youngest: 11, Oldest: 98, Average: 70.15

And that's it. Our large record set of customers has been mapped (grouped) by last name, and then further reduced to a single record per group containing the youngest, oldest and average age. From this end result set we can make assertions about the demographics of a given last name and perform useful analytics tasks.

If we strip out all the tracing and cruft from the above example, we can boil the whole MapReduce process down to a single simple statement:

{% highlight csharp %}

var reducedCustomers = customers
    .GroupBy(c => c.LastName)
	.Select(c => new
	{
		LastName = c.Key,
		Oldest = c.Max(v => v.Age),
		Youngest = c.Min(v => v.Age),
		Average = c.Average(v => v.Age)
	});

{% endhighlight %}

Those eight lines of C# code are MapReduce at its most basic level. It is not a new or complicated concept, but one that is important to understand as a core building block in functional applications.

## What else can we use MapReduce for?
MapReduce has a lot of power and can provide us with answers to complex questions. For example, if we had a dataset of all the people in England, where they live and what they do, we can answer such useful questions as: 

* What is the ratio of software developers to other workers by city? 
* What is the ratio of pubs to software developers by city? 
* What is the gender ratio in software developers by age group? 
* What is the best town to work in for a young, alcoholic software developer looking for a date with another software engineer?

## Have a go
The full working code [is available for download](/assets/map-reduce/MapReduce.cs){:target="_blank"}.

## Links I think might be useful
These are some links to find out more about MapReduce and how it works at scale:

* [https://en.wikipedia.org/wiki/MapReduce](https://en.wikipedia.org/wiki/MapReduce)
* [http://codebetter.com/matthewpodwysocki/2009/03/03/exploring-mapreduce-with-f](http://codebetter.com/matthewpodwysocki/2009/03/03/exploring-mapreduce-with-f)
