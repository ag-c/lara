Lara Web Engine [![License: Apache 2.0](https://img.shields.io/badge/License-Apache--2.0-blue)](https://github.com/integrativesoft/lara/blob/master/LICENSE) [![NuGet version](http://img.shields.io/nuget/v/Integrative.Lara.svg)](https://www.nuget.org/packages/Integrative.Lara/)  [![Download count](https://img.shields.io/nuget/dt/Integrative.Lara.svg)](https://www.nuget.org/packages/Integrative.Lara/)  [![Build Status](https://travis-ci.org/integrativesoft/lara.svg?branch=master)](https://travis-ci.org/integrativesoft/lara)  [![Coverage Status](https://coveralls.io/repos/github/integrativesoft/lara/badge.svg?branch=master&lala=2)](https://coveralls.io/github/integrativesoft/lara?branch=master)
==================


**Lara** is a library for developing **web user interfaces**. Lara gives you full control of the client's HTML Document Object Model (DOM) from the server in C#.

>*"It is similar to server-side Blazor, but is much more lightweight and easier to install. For example, while any type of Blazor requires a whole SDK, Lara is just a NuGet package."* [ScientificProgrammer.net](https://scientificprogrammer.net/2019/08/18/pros-and-cons-of-blazor-for-web-development/?pagename=pros-and-cons-of-blazor)

## Sample application

```csharp
using Integrative.Lara;
using System.Threading.Tasks;

namespace SampleProject
{
    class Program
    {
        static async Task Main()
        {
            using var app = new Application();
            await app.Start(new StartServerOptions
            {
                // launches user's default web browser and terminates when closed
                Mode = ApplicationMode.BrowserApp,  // comment out to host as regular website instead
                
                // search for classes decorated with Lara attributes and load them
                PublishAssembliesOnStart = true
            });
            await app.WaitForShutdown();
        }
    }

    [LaraPage(Address = "/")]
    class MyPage : IPage
    {
        int counter = 0;

        public Task OnGet()
        {
            var button = Element.Create("button");
            button.InnerText = "Click me";
            button.On("click", () =>
            {
                counter++;
                button.InnerText = $"Clicked {counter} times";
                return Task.CompletedTask;
            });
            LaraUI.Page.Document.Body.AppendChild(button);
            return Task.CompletedTask;
        }
    }
}
```

Other resources available for building web pages are the [LaraBuilder](https://github.com/integrativesoft/lara/wiki/LaraBuilder) class, [Web Components](https://github.com/integrativesoft/lara/wiki/Web-Components), and [Reactive Programming](https://github.com/integrativesoft/lara/wiki/Reactive-programming).

## Integrating Lara into an existing web server

To add Lara to an existing ASP.NET Core server, use:

```csharp
public void Configure(IApplicationBuilder app)  
{  
    app.UseLara(new LaraOptions
    {
        // configuration options
    });
} 
```

## Creating Desktop applications

The sample application above creates a program that launches a browser tab on the user's default browser, and terminates when the user closes the tab. However, you may want to create your own desktop container instead. Here's a few options:

- [electron.js](https://www.electronjs.org/) combined with [electron-cgi](https://github.com/ruidfigueiredo/electron-cgi#readme) library
- [Chromely](https://github.com/chromelyapps/Chromely)
- [neutralinojs](https://github.com/neutralinojs/neutralinojs)

## Getting started

There's no need to download this repository to use Lara, instead, there's a [NuGet package](https://www.nuget.org/packages/Integrative.Lara/).

To start, create a new project and add the NuGet package `Integrative.Lara`. In Visual Studio go to Tools -> NuGet Package Manager -> Manage NuGet packages for Solution, then search for the package 'Integrative.Lara'.

In your project, copy and paste the [sample application](https://github.com/integrativesoft/lara/wiki/Sample-Application).

This repository contains a [sample project](https://github.com/integrativesoft/lara/tree/master/src/SampleProject).

The [wiki](https://github.com/integrativesoft/lara/wiki) has the documentation for using Lara.

## How does Lara work?

Whenever the browser triggers a registered event (e.g. click on a button), it sends to the server a message saying that the button was clicked. The server executes the code associated with the event, manipulating the server's copy of the page, and replies a JSON message with the delta between server and client.

The source code contains a [sample project](https://github.com/integrativesoft/lara/tree/master/src/SampleProject). The documentation is available in the [wiki](https://github.com/integrativesoft/lara/wiki).

## How to contribute

**Please send feedback!** Issues, questions, suggestions, requests for features, and success stories. Please let me know by either opening an issue or by [direct message](https://www.linkedin.com/in/pablocar/). Thank you!

**If you like Lara, please give it a star - it helps!**

## Credits

Thanks to [JetBrains](https://www.jetbrains.com/?from=LaraWebEngine) for the licenses of Rider and DotCover.

[![JetBrains](support/jetbrains.svg)](https://www.jetbrains.com/?from=LaraWebEngine)
