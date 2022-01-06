# Truphone.Device.Service
## Note: This project is a WebAPI for Truphone interview

### Project
WebAPI project using in-memory database.

OpenAPI document: [TruphoneDeviceOpenAPI](/docs/TruphoneDeviceOpenAPI.yml)

### Architecture
Solution made using Domain-driven design (DDD) software design approach. The code was made following SOLID principles.

**Project layers structure**
* 1 - Presentation
    * Truphone.Device.Service.API
        * Project description: Solution entry point. Project responsible for exposing inputs for the solution.
        * .NET version: .NET 6.0
        * Project type: WebAPI
* 2 - Application
    * Truphone.Device.Service.Application
        * Project description: Project responsible for translate the presentation layer to the domain layer.
        * .NET version: .NET Standard 2.1
        * Project type: Class library
* 3 - Domain
    * Truphone.Device.Service.Domain
        * Project description: Main solution project, responsible for maintaining the business rules, is totally independent from the rest of the solution.
        * .NET version: .NET Standard 2.1
        * Project type: Class library
* 4 - Infrastructure
    * 4.1 - Data
        * Truphone.Device.Service.Repository
            * Project description: Project responsible for store data.
            * .NET version: .NET Standard 2.1
            * Project type: Class library
    * 4.2 - CrossCutting
        * Truphone.Device.Service.CC.Common
            * Project description: Project responsible for maintaining the classes that are common to all projects.
            * .NET version: .NET Standard 2.1
            * Project type: Class library
        * Truphone.Device.Service.CC.IoC
            * Project description: Project responsable for register all solution interfaces.
            * .NET version: .NET Standard 2.1
            * Project type: Class library
* 5 - Test
    * Truphone.Device.Service.Domain.UnitTest
        * Project description: xUnit project responsable for unit tests.
        * .NET version: .NET 6.0
        * Project type: Console application


### How to run
* Get the project:
    * Clone the project from GitHub: [Truphone.Device.Service](https://github.com/WillianSales/Truphone.Device.Service)

* Run on Visual Studio 2022:
    * Open the solution on Visual Studio 2022;
    * Press F5 or press the "Docker" button on Visual Studio 2022;
    * Visual Studio 2022 will open the browser with the project swagger, where you can use the project.

* Run on Docker:
    * On a PowerShell terminal, change the directory to the project ".sln" folder;
    * Run docker-compose command: ```docker-compose up --build```
    * The project is exposed on localhost on port 49219: http://localhost:49219/devices
    * You can access the OpenAPI project document here: [TruphoneDeviceOpenAPI](/docs/TruphoneDeviceOpenAPI.yml)
    * Use Postman or any other RESTAPI software to interact with the project.