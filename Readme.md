# Background

This simple project was / is a proof of concept, to test two technologies I've been interested for a long time: `blazor` and `grpc`.
Use the search-engine of you choice to find out how and why to install dotnet-core.

I choose a fairly simple, but maybe useful use case for the `grpc`-service: _darts_.
This service may be used for some small `iot`, `maker` or tutorial-projects.

The two front-ends provided by this project are a simple console-based caller for the grpc-service and a simple adaption of the default blazor-template.
Instead of loading fake weather-data, the latter one provides you with a button-prompt to randomly place a virtual dart on the virtual dartboard.
Of course the graphics quality may vary drastically based on your fantasy.

# Ingredients

# Startup

To start this awesome learning experience / proof of concept you need dotnet-core.

And at least two terminal windows.

If you encounter any SSL-Errors, try running the following command in the server + client-folders (e.g. every dotnet-core project contained within this folder):

~~~
dotnet dev-certs https --trust
~~~

And a modern browser.

## 1. Start the dart logic server

New Terminal:

~~~
cd dartServer\
dotnet dev-certs https --trust
dotnet run
~~~

(The 2nd Line: `dotnet dev-cert...` is only necessary if this is you first start of the application and you don't have the already enabled your dev-ssl settings)

## 2. Start the blazor frontend

New Terminal:

~~~
cd BlazorFrontend\
dotnet dev-certs https --trust
dotnet run
~~~

(The 2nd Line: `dotnet dev-cert...` is only necessary if this is you first start of the application and you don't have the already enabled your dev-ssl settings)

## 3. Open Browser

Open your Browser on [http://localhost:5002/fetchdata](http://localhost:5002/fetchdata). 

# License

Don't use this code for any production, non-benefit, non-opensource and corporative use-cases.
