# Estebizz

Estebizz is a web application built with ASP.NET Core, providing a modern and scalable solution for business management.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- C#
- HTML/CSS/JavaScript
- SQL Server (for data storage)

## Project Structure

- `Controllers/`: Contains MVC controllers that handle HTTP requests
- `Models/`: Data models and view models
- `Views/`: Razor views for the user interface
- `Data/`: Database context and configurations
- `wwwroot/`: Static files (CSS, JavaScript, images)
- `Migrations/`: Database migration files

## Prerequisites

- .NET 6.0 SDK or later
- SQL Server (LocalDB or higher)
- Visual Studio 2022 (recommended) or VS Code

## Getting Started

1. Clone the repository:
   ```bash
   git clone [repository-url]
   ```

2. Navigate to the project directory:
   ```bash
   cd Estebizz
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Update the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

The application will be available at `https://localhost:5001` or `http://localhost:5000`

## Development

- Use Visual Studio or VS Code to open the solution file `Estebizz.sln`
- Make sure to run database migrations when making changes to the data models
- The application uses standard ASP.NET Core conventions for routing and dependency injection

## Contributing

1. Fork the repository
2. Create a new branch for your feature
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request