# CodeStream AspNetCoreApiStarter
This is a starter project for building an ASP.NET Core WebApi project according to CodeStream opinions, tools and frameworks.

Built on top of ASP.NET Core 2.1.2 (as of 23 Jul 2018)

[![Build status](https://ci.appveyor.com/api/projects/status/1prnaf788kk4ytt1?svg=true)](https://ci.appveyor.com/project/AllenFirth-CodeStream/aspnetcoreapistarter)

## Frameworks used:

* [MediatR](https://github.com/jbogard/MediatR)
* [CodeStream.Mediatr](https://www.nuget.org/packages/CodeStream.MediatR/)
* [SimpleInjector](https://simpleinjector.org)
* [AutoMapper](https://automapper.org/)
* [Dapper](https://github.com/StackExchange/Dapper)
* [EntityFramework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [NExtensions](https://github.com/halcharger/NExtensions)
* [Swashbuckle (swagger / OpenApi)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [RedBear.LogDNA](https://github.com/RedBearSys/RedBear.LogDNA)
* CI on [AppVeyor](appveyor.com) ([appveyor.yml](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/appveyor.yml))
* deployment to azure web app (via [appveyoy.yml](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/appveyor.yml))

## Getting started

* Clone (`git clone https://github.com/codestreamsystems/AspNetCoreApiStarter.git`)
* Open in Visual Studio (Code)
* Build (`dotnet build`)
* Run (`dotnet run`) and navigate to https://localhost:5001/swagger to inspect swagger documentation.

The app is hosted on https://localhost:5001 when launched from the command line using `dotnet run` (using Kestrel) and hosted on https://localhost:44393 when launched from Visual Studio (using IIS Express).

## To repurpose for new API projects

* Fork this repo to your new project repo
* Run through `Getting Started` steps listed above
* Find and Replace `CodeStreamAspNetCoreApiStart` with `Your Shiny New Project Name` throughout all files in root project directory.
* Build and Run to ensure no errors.
* Update the `LogDNA` section of `appsettings.json` with the new relevant values.
* Update `appveyor.yml` with relevant Azure publish profile settings of Azure app service you wish to deploy to.

## LogDNA configuration and usage

* For configuration see [RegisterLogDNAServicesInSimpleInjector method in Startup.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Startup.cs)
* For usage see [LogDNAMediatrPipeline.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/MediatR/LogDNAMediatrPipeline.cs)

## MediatR Pipeline configuration and implementation

* For configuration see [AppSimpleInjectorPackage.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/AppSimpleInjectorPackage.cs)
* For implementation see [ErrorHandlerMediatrPipeline.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/MediatR/ErrorHandlerMediatrPipeline.cs) and [LogDNAMediatrPipeline.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/MediatR/LogDNAMediatrPipeline.cs)

## AutoMapper configuration

* For configuration see [AutoMapperProfile.cs](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/AutoMapperProfile.cs)

## MediatR Pipeline logging with LogDNA

* All `Command` and `Query` messages should inherit from [AppMessage](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/AppMessage.cs)
* All executions of `Commands` and `Queries` will be logged via [LogDNAMediatrPipeline](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Infrastructure/MediatR/LogDNAMediatrPipeline.cs)
* Execution time in milliseconds will also be logged against the `Command / Query`

## `Command / Query` chaining and `CorrelationId`

In complex applications there will be the need for one `Command / Query` to call another and so on creating a chain of messages (one calling the other). This results in multiple `Commands / Queries` being called all in the scope of a single Http Request. When viewing logs of these `Commands / Queries` is if often imperative to view all logs for all chained / linked messages. This can be done as long as all messages use the same `CorrelationId`. 

An example of this can be seen in the [HeartBeatQueryHandler](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/Queries/HeartBeatQueryHandler.cs) where the `HeartBeatQueryHandler executes the `AppVersionQuery`.