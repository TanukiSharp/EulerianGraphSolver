# Overview

This application is an Eulerian graph (also known as Eulerian path) solver.

You can find more interesting information about Eulerian graph on the [Wikipedia](https://en.wikipedia.org/wiki/Eulerian_path) page.

I implemented it when I was playing *Dragon Age: Inquisition*, there were enigmas on constellation, and I was just feeling lazy to solve them myself, so I made a program that solved them for me ^^

# How to use

Modify the `Program.cs` file to setup your own graph.

Each `Connect` method connects the first argument with all other arguments.
If you have a closer look at this method, you will see its prototype is as follow:

``` cs
Connect(T source, params T[] targets)
```

Then just run the application.

The method `Done` computes some properties of the graph, and then the `Solve` method produces the trail that connects all the points respecting the rules of the Eulerian graph.
