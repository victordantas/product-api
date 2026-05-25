# Product API

Production-style REST API built with .NET 10 using Clean Architecture and CQRS.

## Features
- CRUD operations
- Validation pipeline
- Structured logging
- EF Core persistence
- Global exception handling
- Docker support
- CI/CD pipeline

## Architecture
- Domain
- Application
- Infrastructure
- API

## Tech Stack
- .NET 10
- EF Core
- MediatR
- FluentValidation
- Serilog
- SQLite
- Docker

## Running

dotnet run --project ProductApi.API

## Docker

docker build -t product-api .
docker run -p 8080:8080 product-api