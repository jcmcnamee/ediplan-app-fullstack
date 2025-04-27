
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
    dotnet ef database update
    dotnet run

# **High-Level Overview**

The Ediplan application is a modular .NET 8 solution designed to follow clean architecture principles. It is divided into multiple projects, each with a specific responsibility:

1. **Ediplan.Api**: The entry point of the application, responsible for exposing APIs to clients.
2. **Ediplan.Application**: Contains the core business logic, including commands, queries, and validation.
3. **Ediplan.Identity**: Manages user authentication and authorization using ASP.NET Core Identity.
4. **Ediplan.Infrastructure**: Provides external integrations, such as email services and file handling.
5. **Ediplan.Persistence**: Handles database interactions using Entity Framework Core.

Each project is designed to be loosely coupled, promoting maintainability and scalability.

---

## **Project Structure and Responsibilities**

### **1. Ediplan.Api**
- **Purpose**: Acts as the API layer, exposing endpoints to clients.
- **Key Features**:
  - Uses `MediatR` for handling requests and responses.
  - Configures Swagger for API documentation.
  - Integrates Serilog for logging.
- **Key Files**:
  - `Program.cs`: Configures the application pipeline, dependency injection, and middleware.
  - `Startup.cs` (if applicable): Configures services and middleware.
- **Dependencies**:
  - References `Ediplan.Application`, `Ediplan.Infrastructure`, and `Ediplan.Persistence`.

### **2. Ediplan.Application**
- **Purpose**: Contains the core business logic and application rules.
- **Key Features**:
  - Implements CQRS (Command Query Responsibility Segregation) using `MediatR`.
  - Uses `FluentValidation` for input validation.
  - Contains mappings using `AutoMapper`.
- **Key Folders**:
  - `Features`: Organized by domain areas (e.g., `Assets`, `Locations`, `Productions`).
    - **Commands**: Handles write operations (e.g., creating or updating entities).
    - **Queries**: Handles read operations (e.g., fetching data).
  - `Contracts`: Defines interfaces and DTOs for communication between layers.
- **Dependencies**:
  - Referenced by all other projects.

### **3. Ediplan.Identity**
- **Purpose**: Manages user authentication and authorization.
- **Key Features**:
  - Uses ASP.NET Core Identity with Entity Framework Core.
  - Supports PostgreSQL as the database provider.
- **Key Folders**:
  - `Migrations`: Contains database migration files for Identity.
- **Dependencies**:
  - References `Ediplan.Application`.

### **4. Ediplan.Infrastructure**
- **Purpose**: Handles external integrations and infrastructure concerns.
- **Key Features**:
  - Provides email functionality using `SendGrid`.
  - Supports CSV file handling using `CsvHelper`.
- **Key Files**:
  - Service implementations for external integrations.
- **Dependencies**:
  - References `Ediplan.Application`.

### **5. Ediplan.Persistence**
- **Purpose**: Manages database interactions.
- **Key Features**:
  - Uses Entity Framework Core for database access.
  - Supports PostgreSQL as the database provider.
  - Contains database migrations.
- **Key Folders**:
  - `Migrations`: Contains migration files for the application's database schema.
- **Dependencies**:
  - References `Ediplan.Application`.

---




