---
layout: post
author: tristan-rhodes
title: Fun with Git automation
excerpt: How to waste time automating with git
featured-image: /assets/fun-with-git-automation/featured-image.jpg
---

## Git 
Git is a fantastic lightweight source control system. We love it here. We love it so much that we try and automate common tasks in it with powershell because all developers are lazy.

This post is about the many hours spent trying to diagnose credential issues, and how to not repeat it. :)

## Git credentials
We are using git through Visual Studio Online, the web based project management tool by Microsoft. It comes with lots of nice API's, and some of them do the same thing, in different ways, using different credentials, which gets a bit confusing.

We are this with a companies federated login provider so our credentials are either our e-mail address, or username@veinteractive.com depending on the API / approach being used in the automation.
 
## Connecting
To connect to a git repostory using a git client and embedded credentials in the URL, you need a uri that looks like this:

{% highlight powershell %}

'https://username:password@company.visualstudio.com/DefaultCollection/MyCollection/_git/MyProjectName'

{% endhighlight %}

This works pretty well, it's easy enough to use this approach to connect to github. However, when using a federated login provider and you have username@domain.com and special charachters in your password, then the URL will fail when it looks like this:

{% highlight powershell %}

'https://username@domain.com:p@55w0rd!@company.visualstudio.com/DefaultCollection/MyCollection/_git/MyProjectName'

{% endhighlight %}

The error messages are not helpful, but the root causes were as follows:

* You need to generate a personal access token in Visual Studio Online to connect to git via the URL.
* The username actually expects your 'email' to be loginUserName@domain.com
* Special charachters in the user name and password fields need to be url encoded.

So when using powershell, create a vso access token (My Profile -> Security), then using the username loginName@veinteractive.com and your access token as the password, use the powershell with your :

{% highlight powershell %}

$rawUrl = 'https://{0}:{1}@company.visualstudio.com/DefaultCollection/MyCollection/_git/MyProjectName';
$gitUrl = $rawUrl -f [System.Net.WebUtility]::UrlEncode($username), [System.Net.WebUtility]::UrlEncode($password)

{% endhighlight %}

Then use the git URL when performing pulls, push and clone operations.