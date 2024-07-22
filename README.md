
# Ediplan: Broadcast TV Post-Production Management System

Ediplan is a comprehensive web application designed to streamline asset and booking management in the broadcast TV post-production industry. Leveraging modern software architecture and design patterns, Ediplan offers a robust solution for managing bookings, assets, productions, and more, ensuring efficient and effective post-production workflows.

## Key Features

- Booking Management: Create, update, and manage bookings with detailed information, including dates, notes, and associated production and location details.
- Asset Management: Comprehensive management of assets including equipment, rooms, and personnel, allowing for efficient allocation and tracking.
- Production Tracking: Keep track of productions, including associated bookings, assets, and personnel involved.
- Dynamic Queries: Utilize dynamic sorting and filtering to easily find and manage bookings and assets.
- Automated Email Notifications: Send automated email notifications for important booking updates and changes.
- CSV Exporting: Export lists of bookings and assets to CSV for reporting and analysis.

## Architecture

Ediplan is built using a clean architecture approach, divided into several layers to ensure separation of concerns, scalability, and maintainability:

- Domain Layer: Defines core business entities and logic.
- Persistence Layer: Manages data access and storage using Entity Framework Core with PostgreSQL.
- Application Layer: Contains business logic and application services, implementing CQRS pattern with MediatR for clear separation of commands and queries.
- Infrastructure Layer: Provides implementations for cross-cutting concerns such as email services and CSV exporting.
- API Layer: Exposes functionality over HTTP, allowing clients to interact with the application through RESTful endpoints.

## Technologies

- .NET 8: Target framework for building high-performance, cross-platform web applications.
- Entity Framework Core: ORM for .NET, used for data access.
- PostgreSQL: Open-source relational database.
- MediatR: Library implementing the mediator pattern for in-process messaging.
- AutoMapper: Object-to-object mapping tool to simplify complex data transformations.
- Swagger: API documentation tool for testing and interacting with the RESTful API.

## Getting Started

To get started with Ediplan, ensure you have .NET 5 SDK and PostgreSQL installed on your machine. Clone the repository, update the database connection string in appsettings.json, and run the following commands in your terminal:

    dotnet restored
    otnet ef database update
    dotnet run

# Architecture continued:

## Ediplan.Domain

This layer defines the core business entities and logic. It includes entities such as Booking, BookingGroup, Production, Location, Asset, Person, Equipment, Room, and AssetGroup. These entities represent the data model around which the application is built.

## Ediplan.Persistence

This layer is responsible for data access and storage. It includes the EdiplanDbContext class, which is a DbContext for Entity Framework Core, configured to work with a PostgreSQL database as indicated by the use of UseNpgsql in the PersistenceServiceRegistration.cs file. This layer defines how entities are mapped to the database schema and includes repositories for accessing and manipulating data.

## Ediplan.Application

The Application layer contains the business logic and application services. It uses the MediatR library to implement the CQRS (Command Query Responsibility Segregation) pattern, allowing for a clear separation between commands (actions that modify data) and queries (actions that retrieve data). This layer also includes services for property mapping and checking, which likely facilitate dynamic sorting and validation of input data.

## Ediplan.Infrastructure

This layer provides implementations for cross-cutting concerns such as email services (IEmailService) and CSV exporting (ICsvExporter). It is configured to read settings from the application's configuration, such as EmailSettings.

## Ediplan.Api

The API layer exposes the application's functionality over HTTP, allowing clients to interact with the application through RESTful endpoints. It includes controllers such as AssetController and BookingController, which handle HTTP requests, validate input, and return responses. These controllers depend on services like IMediator for handling commands and queries, and IPropertyMappingService and IPropertyCheckerService for dynamic property mapping and validation.

## Testing and Middleware

The project includes integration tests (API.IntegrationTests) with a custom web application factory for setting up a test environment with an in-memory database. There's also an exception handling middleware (ExceptionHandlerMiddleware) in the API layer, which catches and processes exceptions, ensuring that the API returns proper error responses.

## Dependency Injection

Throughout the project, dependency injection is heavily utilized to decouple components and facilitate testing. Services are registered in extension methods (AddApplicationServices, AddInfrastructureServices, AddPersistenceServices), making the startup configuration cleaner and more modular.
The Ediplan project follows modern software architecture principles, making it scalable, maintainable, and testable.




