
## Technology Stack

- **Framework**: .NET 8.0 / 9.0 (ASP.NET Core)
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Mapping**: AutoMapper
- **API Documentation**: Swagger (Swashbuckle)

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or LocalDB)

### Local Setup

1. **Clone the repository**:
   git clone https://github.com/YourUsername/learn_task.git
   cd Task

2. **Update Connection String**:
   Open `Sample/appsettings.json` and ensure the `DefaultConnection` points to your local SQL Server instance.

3. **Run Migrations**:
   Open your terminal in the `Sample` directory and run:
   dotnet ef database update
   

4. **Run the Application**:
   Click the **Green Play button** (or press **F5**) in Visual Studio to start the application.

## API Documentation

Once the app is running, navigate to:
`https://localhost:7044/swagger` (Port may vary)

### Example Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| **GET** | `/Products?pageNumber=1&pageSize=10` | Get paginated list of products |
| **GET** | `/Products/{id}` | Get product by ID |
| **POST** | `/Products` | Create a new product |
| **PUT** | `/Products/{id}` | Update an existing product |
| **DELETE** | `/Products/{id}` | Delete a product |

---

##  Pagination Guide

To use pagination, simply add query strings to your GET request:
`GET /Products?pageNumber=2&pageSize=5`

- **pageNumber**: The page you want to view (defaults to 1).
- **pageSize**: Number of items per page (defaults to 10).
