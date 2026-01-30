# 🏦 BTS Bank - Administration System

A comprehensive bank administration system built with ASP.NET Core Razor Pages and Entity Framework Core. This application is designed for bank staff (not customers) and provides functionality for managing customers, accounts, transactions, and users with role-based access control.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/database-SQL%20Server-CC2927?logo=microsoft-sql-server)

## ✨ Features

### 🔐 Authentication & Authorization
- ASP.NET Core Identity integration
- Role-based access control (Admin, Cashier)
- Email confirmation support

### 📊 Dashboard & Statistics
- Real-time statistics panel on the homepage (publicly accessible)
- Country-based financial statistics
- Customer and account overview metrics

### 👥 Customer Management
- Advanced customer search with pagination
- Customer detail pages showing all associated accounts
- Total balance calculation per customer
- Customer profile editing

### 💰 Account Management
- Account detail pages with transaction history
- Lazy loading for transactions via AJAX
- Support for deposits, withdrawals, and transfers
- Real-time balance updates

### 🎨 User Interface
- Responsive Bootstrap 5 design
- Clean and intuitive layout
- Mobile-friendly interface

### 🔒 Security
- Input validation and sanitization
- Protection against unauthorized access
- Role-based page restrictions

## 👤 User Roles & Permissions

| Role | Permissions |
|------|-------------|
| **Admin** | Full access including user management and registration |
| **Cashier** | Customer and account management only |
| **Anonymous** | Access to homepage and statistics only |

## 🔑 Seeded Users

For testing purposes, the following users are pre-configured:

| Email | Password | Role |
|-------|----------|------|
| richard.chalk@admin.se | `Abc123#` | Admin |
| richard.chalk@cashier.se | `Abc123#` | Cashier |

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 9.0** - Web framework
- **Razor Pages** - UI framework
- **Entity Framework Core 9.0** - ORM (Database First approach)
- **SQL Server** - Database
- **ASP.NET Core Identity** - Authentication & authorization

### Frontend
- **Bootstrap 5** - CSS framework
- **JavaScript/AJAX** - Dynamic content loading
- **HTML5 & CSS3** - Markup and styling

### Architecture
- **Three-tier architecture**
  - `Bankapp` - Presentation layer (Razor Pages)
  - `Services` - Business logic layer
  - `DataAccessLayer` - Data access and models

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

#### 1. Clone the repository

```bash
git clone https://github.com/maxiimize/Bankapp.git
cd Bankapp
```

#### 2. Update the connection string

Edit `appsettings.json` in the `Bankapp` folder:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BankAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### 3. Apply database migrations

```bash
dotnet ef database update --project DataAccessLayer
```

#### 4. Run the application

```bash
cd Bankapp
dotnet run
```

#### 5. Access the application

Open your browser and navigate to `https://localhost:5001` (or the port shown in the console)

## 📁 Project Structure

```
Bankapp-1/
├── Bankapp/                    # Main web application
│   ├── Pages/                  # Razor Pages
│   │   ├── Accounts/          # Account management pages
│   │   ├── Customers/         # Customer management pages
│   │   └── Shared/            # Shared layouts and partials
│   ├── wwwroot/               # Static files (CSS, JS, images)
│   └── Program.cs             # Application entry point
├── DataAccessLayer/           # Data access layer
│   ├── Models/                # Entity models
│   ├── Migrations/            # EF Core migrations
│   └── BankAppDataContext.cs  # Database context
└── Services/                  # Business logic layer
    ├── Interfaces/            # Service interfaces
    ├── Services/              # Service implementations
    └── Viewmodels/            # View models
```

## 🗺️ Key Pages

- `/` - Homepage with statistics dashboard
- `/Customers/Search` - Search and browse customers
- `/Customers/CustomerDetails` - View customer details and accounts
- `/Accounts/Details` - View account transactions
- `/Accounts/Transfer` - Perform account transfers
- `/Administration` - User management (Admin only)

## 🔄 Development Notes

- The application uses Database First approach with Entity Framework Core
- Migrations are automatically applied on application startup
- Data seeding occurs during first run via `DataInitializer`
- AJAX is used for lazy loading transaction history for better performance
  

## 📝 Future Enhancements

- Complete Azure deployment configuration
- Add transaction export to PDF/Excel
- Implement email notifications
- Add two-factor authentication
- Enhanced reporting and analytics
- Audit logging for all transactions

## 📄 License

This project was created for educational purposes.

## 👨‍💻 Author

**Maxiimize**

## 🙏 Acknowledgments

- Built as part of a learning project for ASP.NET Core development
- Bootstrap theme used for responsive design

---

**BTS Bank** - *Professional Banking Administration Made Simple*
