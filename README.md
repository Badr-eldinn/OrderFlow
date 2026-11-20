# OrderFlow

Order management REST API built with ASP.NET Core 10, structured with Clean Architecture and CQRS.

## Overview

OrderFlow is a backend API for managing orders. The solution is split into four layers with clear dependency direction (API → Application → Domain, Infrastructure implements Application abstractions):

- **OrderFlow.Domain** - entities and business rules, no dependencies
- **OrderFlow.Application** - use cases as MediatR commands and queries (CQRS), interfaces for persistence
- **OrderFlow.Infrastructure** - EF Core 10 with SQL Server, repositories and migrations
- **OrderFlow.API** - ASP.NET Core controllers, Swagger/OpenAPI documentation

## Tech stack

- C# / .NET 10
- ASP.NET Core Web API
- MediatR 14 (CQRS)
- Entity Framework Core 10 + SQL Server
- Swashbuckle (Swagger UI) + Microsoft.OpenApi

## Running locally

1. Clone the repo:
   ```
   git clone https://github.com/Badr-eldinn/OrderFlow.git
   ```
2. Set the SQL Server connection string in `OrderFlow.API/appsettings.json`.
3. Apply migrations and run:
   ```
   cd OrderFlow.API
   dotnet ef database update
   dotnet run
   ```
4. Open Swagger UI at `/swagger` to explore and try the endpoints.

## API documentation

All endpoints are documented through Swagger/OpenAPI. With the app running in Development, the interactive UI is available at `/swagger`.
