# Inventory Project

A web-based inventory management system developed as part of an internship program. Built with ASP.NET Core 6.0 and Entity Framework Core, featuring user authentication and authorization.

## Features

- **User Authentication & Authorization**: Built-in ASP.NET Core Identity with custom password policies
- **Secure Access Control**: All endpoints require authentication by default
- **Database Integration**: Entity Framework Core with SQL Server
- **Session Management**: 30-minute session timeout
- **MVC Architecture**: Clean separation of concerns with Model-View-Controller pattern

## Technology Stack

- **Framework**: ASP.NET Core 6.0
- **Database**: SQL Server with Entity Framework Core 6.0.13
- **Authentication**: ASP.NET Core Identity
- **Architecture**: MVC (Model-View-Controller)
- **IDE**: Visual Studio 2022

## Prerequisites

- .NET 6.0 SDK
- SQL Server (LocalDB or full installation)
- Visual Studio 2022 (recommended) or Visual Studio Code

## Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd InventoryProject
```

### 2. Database Setup
The application is configured to use a local SQL Server database named "Login". Update the connection string in `appsettings.json` if needed:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=Login;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true"
}
```

### 3. Database Migration
Run the following commands in the Package Manager Console or terminal:
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:5001` (or the port specified in your launch settings).

## Configuration

### Password Policy
The application enforces the following password requirements:
- Minimum length: 10 characters
- Required unique characters: 3

### Session Configuration
- Session timeout: 30 minutes
- Sessions are automatically configured and enabled

### Security Features
- All controllers require authentication by default
- HTTPS redirection enabled
- HSTS (HTTP Strict Transport Security) enabled in production

## Project Structure

```
InventoryProject/
├── Controllers/          # MVC Controllers
├── Data/                # Database context and configurations
├── Models/              # Data models and view models
├── Views/               # Razor views and layouts
├── wwwroot/             # Static files (CSS, JS, images)
├── appsettings.json     # Application configuration
└── Program.cs           # Application startup and configuration
```

## Development

### Adding New Features
1. Create controllers in the `Controllers/` folder
2. Add corresponding views in the `Views/` folder
3. Update models in the `Models/` folder
4. Run database migrations if model changes affect the database

### Database Migrations
When making changes to your models:
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Authentication

The application uses ASP.NET Core Identity for user management. Key features include:
- User registration and login
- Role-based authorization
- Secure password hashing
- Account lockout protection

## Learning Objectives

This internship project demonstrates proficiency in:
- ASP.NET Core MVC development
- Entity Framework Core and database management
- User authentication and authorization
- Security best practices in web applications
- Clean code architecture and project organization

## Development Notes

This project was developed as part of an internship to gain hands-on experience with:
- Modern web development frameworks
- Database design and integration
- User management systems
- Security implementation
- Professional development practices


## Project Timeline & Milestones

This internship project includes the following development phases:
- **Phase 1**: Project setup and authentication system 
- **Phase 2**: Database design and Entity Framework integration 
- **Phase 3**: Inventory management features 
- **Phase 4**: User interface and experience improvements
- **Phase 5**: Testing, documentation, and deployment

## Skills Developed

Through this project, the following technical skills were gained:
- ASP.NET Core MVC framework
- Entity Framework Core ORM
- SQL Server database management
- ASP.NET Core Identity system
- Web security implementations
- Project structure and organization
- Version control with Git
