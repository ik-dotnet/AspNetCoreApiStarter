# CodeStream AspNetCoreApiStarter
This is a starter project for building an ASP.NET Core WebApi project according to CodeStream opinions, tools and frameworks.

Built on top of ASP.NET Core 2.0

## Frameworks used:

* MediatR
* SimpleInjector
* AutoMapper
* Dapper
* EntityFramework Core
* Swashbuckle (swagger / OpenApi)
* CI on AppVeyor (appveyor.yml)
* deployment to azure web app (via appveyoy.yml)

## Getting started

* Clone
* Open in Visual Studio
* Build
* Run and navigate to http://localhost:9019/swagger to inspect swagger documentation.

## To repurpose for new API projects

* Fork this repo to your new project repo
* Run through `Getting Started` steps listed above
* Find and Replace `CodeStreamAspNetCoreApiStart` with `Your Shiny New Project Name` throughout all files in root project directory.
* Build and Run to ensure no errors.