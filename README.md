# TimeTrackerService

A backend service for managing time tracking data, built with .NET and C#.  
This service provides RESTful APIs to create, update, delete, and query time entries, as well as manage related entities such as people and tasks.

---

## 🚀 Features

- ✅ Create, update, and delete time entries
- ✅ Manage people and task information
- ✅ RESTful API design
- ✅ Entity Framework Core integration for database operations
- ✅ Supports SQL Server (configurable)
- ✅ Designed for easy integration with frontend clients (e.g., Angular app)

---

## 💻 Tech Stack

- **Backend:** .NET 8+, C#
- **Database:** SQL Server (via Entity Framework Core)
- **API:** ASP.NET Core Web API
- **Tools:** Swagger (API documentation), AutoMapper, Dependency Injection

---

## 🛠️ Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/Aura-lab/time-tracker-service.git
cd time-tracker-service
```

### 2️⃣ Setup database connection

Configure your database connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TimeTrackerDb;Trusted_Connection=True;"
  }
}
```

### 3️⃣ Run database migrations

```bash
dotnet ef database update
```

> ⚡ **Note:** Ensure you have `dotnet-ef` tool installed. You can install it with:

```bash
dotnet tool install --global dotnet-ef
```

### 4️⃣ Run the application

```bash
dotnet run
```

The API will be available at [https://localhost:5001](https://localhost:5001) or [http://localhost:5000](http://localhost:5000).

---

## 📄 API Documentation

Swagger UI is available when you run the app.  
Open in browser:

```
https://localhost:5001/swagger
```

This allows you to explore and test all available endpoints.

---

## 🧪 Testing

Run unit tests:

```bash
dotnet test
```

---

## 🤝 Contribution

Pull requests are welcome! For major changes, please open an issue first to discuss what you'd like to change.

---

## 📝 License

This project is licensed under the MIT License.

---

## 💬 Additional Information

- This backend service was designed to complement the [TimeTrackerApp](https://github.com/Aura-lab/time-tracker-app) frontend project.
- For questions or improvements, please open an issue.

---

## ⭐ Acknowledgments

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [Swagger](https://swagger.io/)
