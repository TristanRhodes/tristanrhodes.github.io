---
layout: post
author: tristan-rhodes
title: Config Settings in .NET Core
excerpt: This post covers how configuration files and handling of settings has changed in .NET Core.
featured-image: /assets/config-settings-in-dnx/featured-image.jpg
---

## What is .NET Core?
.NET Core is the current name for the latest version of the .NET build engine which is making big steps towards moving .NET to a true cross platform enviroment. It comes with DNVM (Dot Net Version Manager) and a whole new ecosystem of supporting tools. It's gone under a variety of aliases, including vNext, DNX and most recently .NET Core.

This shift has entirely changed the structure of Visual Studio projects, so even experienced .NET developers can spin up a new web app in .NET Core and  everything is unrecognisable. Today, we are going to try and make it more familiar by focusing on the awesome changes to the configuration settings mechanism.

## What's new?
There is so much cool stuff in the new .NET enviroment. Dependency Injection and Logging are now first class concerns with _IServiceCollection_ and _ILoggerFactory_, you can't avoid having them in your applications. Adding a reference has been phased out, and now we have all depedencies as Nuget packages. However, this article is focusing on the changes to configuration management.

.NET Core / DNX now uses JSON files to store settings, by default config.json. This means gone are the XML files and Debug / Release transformations of old, along with the whole ConfigurationManager and AppSettings dictionary. Schemas are much less restrictive and the whole structure is more composable as we can add multiple .json files to our configuration builder and have them all loaded into a hierachial property bag.

## Sample Settings
This is what the config.json file looks like now:

{% highlight json %}
{
  "AppSettings": {
    "SiteTitle": "Dnx.Sample"
  },
  "Data": {
    "DefaultConnection": {
      "ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=aspnet5-Dnx.Sample-b60c6e19-ac70-4f37-8cf5-f6f1799796f5;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
  }
}
{% endhighlight %}

## Adding Configuration Files at startup

{% highlight csharp %}
var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
    .AddJsonFile("config.json")
    .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
{% endhighlight %}

Here the last added JSON file will be override any existing settings.

## Creating your configuration class
Once you have composed all your different settings files, you create your configuration definition with the following line in your Startup class.

{% highlight csharp %}
Configuration = builder.Build();
{% endhighlight %}

This creates an instance of _IConfiguration_ which is your entry point to the configuration system.

## Accessing Configuration Values

With the Configuration object now set up, we can drill into the settings. If we have the following JSON in a loaded settings file:

{% highlight json %}
{
  "MyAppSettings": {
    "MyComponentSettings": {
      "DisplayName": "Welcome to my App!"
    }
  }
}
{% endhighlight %}

Then we can query the value using a colon (:) seperated path like this:

{% highlight csharp %}
var displayName = Configuration["MyAppSettings:MyComponentSettings:DisplayName"];
{% endhighlight %}

## Configuration Sections

You can also retrieve a whole configuration section, which will return an _IConfiguration_ instance for the part of the JSON tree selected, in the example below you would get the content of the "MyAppSettings" node.

{% highlight csharp %}
var myAppSection = Configuration.GetConfigurationSection("MyAppSettings");
{% endhighlight %}

Which would behave like you just loaded:

{% highlight json %}
{
  "MyComponentSettings": {
    "DisplayName": "Welcome to my App!"
  }
}
{% endhighlight %}

## Strongly Typed Configuration Sections

Finally, a very cool new feature is strongly typed configuration sections. With these, you can declare a configuration class, and load it directly into your container as a settings provider to be picked up by any class that depends on it.

If we create some classes to match the JSON shown above.

{% highlight csharp %}
public class MyAppSettings
{
    public MyComponentSettings MyComponentSettings { get; set; }
}

public class MyComponentSettings
{
    public string DisplayName { get; set; }
}
{% endhighlight %}

Then in the startup load them into the service provider

{% highlight csharp %}
var section = Configuration.GetConfigurationSection("MyAppSettings");
services.Configure<MyAppSettings>(section);
{% endhighlight %}

With this done, we can consume these settings by adding the following parameter:

{% highlight csharp %}
public HomeController(IOptions<MyAppSettings> settings)
{
	// The .Options property returns the strongly typed instance.
    _myAppSettings = settings.Options;
}
{% endhighlight %}

And that's it, simple, clean and efficient. What's not to like?

### Credits

Credit to Rick Strahl, whos [blog post](https://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5) came up in my research and provided a lot of additional information.