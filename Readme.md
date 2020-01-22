# Background

This simple project was / is a proof of concept, to test two technologies I've been interested for a long time: `blazor` and `grpc`.

I choose a fairly simple, but maybe useful use case for the `grpc`-service: _darts_.
This service may be used for some small `iot`, `maker` or tutorial-projects.

The two front-ends provided by this project are a simple console-based caller for the grpc-service and a simple adaption of the default blazor-template.
Instead of loading fake weather-data, the latter one provides you with a button-prompt to randomly place a virtual dart on the virtual dartboard.
Of course the graphics quality may vary drastically based on your fantasy.

# Ingredients

# Startup

To start this awesome learning experience / proof of concept you need dotnet-core.

And at least two terminal windows.

And a modern browser.

## 1. Start the dart logic server

New Terminal:

~~~
cd dartServer\
dotnet run
~~~

## 2. Start the blazor frontend

New Terminal:

~~~
cd BlazorFrontend\
dotnet run
~~~

# License

Don't use this code for any production, non-benefit, non-opensource and corporative use-cases.
