# 🛠️ Furniture Factory API

**Furniture Factory API** is a RESTful Web API developed using **ASP.NET Core Web API** and **Entity Framework Core** to manage the production system of a furniture factory. This API facilitates the management of products, components, and subcomponents.

## 📁 Project Structure

FurnitureFactory.API/
│
├── FurnitureFactory.API/ → Main ASP.NET Core Web API project
│ ├── Controllers/ → API Controllers (Products, Components, etc.)
│ ├── Program.cs → Application startup configuration
│ └── appsettings.json → Configuration and connection string
│
├── FurnitureFactory.DAL/ → Data Access Layer
│ ├── Entities/ → Entity classes (Product, Component, Subcomponent)
│ ├── Configurations/ → Fluent API configurations for each entity
│ ├── FurnitureFactoryDbContext.cs → EF Core DbContext
│
├── FurnitureFactory.BL/ → Business Logic Layer (optional for abstraction and services)
│
└── FurnitureFactory.sln → Visual Studio Solution File


## 🧱 Key Features

- 🔄 **CRUD Operations**:
  - Products
  - Components
  - Subcomponents

- 📦 **Entity Relationships**:
  - A `Product` consists of multiple `Components`.
  - A `Component` can include multiple `Subcomponents`.

- 🛠️ **EF Core Migrations**:
  - Migrations are managed using `FurnitureFactory.DAL` as the migrations assembly.
  - Supports automatic database creation and updating.

## ⚙️ Technologies Used

- **.NET 9 Web API** [Official Site](https://dotnet.microsoft.com/)
- **Entity Framework Core** [Official Docs](https://learn.microsoft.com/en-us/ef/core/)
- **SQL Server**
- **LINQ** and **Fluent API**
- **Layered Architecture** (API, BL, DAL)

## 🧪 How to Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MahmoudELdeba34/Furniture-Factory-API.git
2. Set up the connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=FurnitureFactoryDB;Trusted_Connection=True;TrustServerCertificate=True"
}

3.Apply migrations and update the database:

- Open the Package Manager Console.

- Set Default Project to FurnitureFactory.DAL.
-Then, run the following commands:
->Add-Migration InitialCreate
->Update-Database

4- Run the API:
-> dotnet run --project FurnitureFactory.API

👨‍💻 Author
This project was developed by Eng. Mahmoud Ahmed as part of a production system for a furniture manufacturing company.


-> dotnet run --project FurnitureFactory.API![API](https://github.com/user-attachments/assets/875eca78-ad68-4679-aaef-d6e1396fa28b)





