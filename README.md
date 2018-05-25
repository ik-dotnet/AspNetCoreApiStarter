# CodeStream AspNetCoreApiStarter
This is a starter project for building an ASP.NET Core WebApi project according to CodeStream opinions, tools and frameworks.

Built on top of ASP.NET Core 2.1 RC1 (as of 25 May 2018)

## Frameworks used:

* [MediatR](https://github.com/jbogard/MediatR)
* [CodeStream.Mediatr](https://www.nuget.org/packages/CodeStream.MediatR/)
* [SimpleInjector](https://simpleinjector.org)
* [AutoMapper](https://automapper.org/)
* [Dapper](https://github.com/StackExchange/Dapper)
* [EntityFramework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [NExtensions](https://github.com/halcharger/NExtensions)
* [Swashbuckle (swagger / OpenApi)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* CI on [AppVeyor](appveyor.com) ([appveyor.yml](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/appveyor.yml))
* deployment to azure web app (via [appveyoy.yml](https://github.com/codestreamsystems/AspNetCoreApiStarter/blob/master/appveyor.yml))

## Getting started

* Clone (`git clone https://github.com/codestreamsystems/AspNetCoreApiStarter.git`)
* Open in Visual Studio (Code)
* Build (`dotnet build`)
* Run (`dotnet run`) and navigate to https://localhost:44393/swagger to inspect swagger documentation.

The app is hosted on https://localhost:5001 when launched from the command line using `dotnet run` (using Kestrel) and hosted on https://localhost:44393 when launched from Visual Studio (using IIS Express).

## To repurpose for new API projects

* Fork this repo to your new project repo
* Run through `Getting Started` steps listed above
* Find and Replace `CodeStreamAspNetCoreApiStart` with `Your Shiny New Project Name` throughout all files in root project directory.
* Build and Run to ensure no errors.