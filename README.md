# Library API v3

A RESTful ASP.NET Core Web API built as a learning project to explore modern backend development practices in .NET.

## Features

- CRUD operations for Books and Authors
- CQRS pattern implementation
- MediatR for request handling
- FluentValidation for command and query validation
- Repository pattern
- Entity Framework Core
- SQL Server database
- OpenAPI support with Scalar
- JWT Authentication (in progress)

## Tech Stack

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- Scalar

## Project Structure

```text
Features/
├── Books/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   └── Validators/
│
├── Authors/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   └── Validators/

Controllers/
Data/
Providers/
Common/
```