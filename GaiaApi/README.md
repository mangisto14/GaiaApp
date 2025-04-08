Gaia Project - README
Project Overview
The Gaia Project is a software development exercise consisting of two parts:

GaiaApi: A Web API for performing arithmetic and string operations with a database that logs operations.

GaiaWeb: A Razor Pages web application that communicates with the API to allow users to perform calculations and view operation history.

This guide explains how to set up and run the database and the applications.

Setup Instructions
# 1. Prerequisites
Ensure the following tools are installed:

Visual Studio 2022 or newer

.NET 6.0 SDK or newer

Microsoft SQL Server or SQL Server Express (or an equivalent database)

Entity Framework Core for database management

# 2. Database Setup (SQL Server)
Follow these steps to set up the database:

-1- Create the Database:

    Open SQL Server Management Studio (SSMS).

    Connect to your SQL Server instance.

    Run the following SQL query to create a new database (replace GaiaDB with your preferred database name):

    CREATE DATABASE GaiaDB;
    GO

-2- Create the Tables:

    Run the following SQL script to create the necessary table(s) for logging operations:


    CREATE TABLE Operations (
        Id INT IDENTITY PRIMARY KEY,
        FieldA FLOAT NOT NULL,
        FieldB FLOAT NOT NULL,
        Operation VARCHAR(50) NOT NULL,
        Result VARCHAR(255) NOT NULL,
        Timestamp DATETIME NOT NULL
    );
    GO

-3- Set Up Connection String in appsettings.json:

    Open the GaiaApi project.

    In the appsettings.json file, configure the connection string to point to your database:


    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server_name;Database=GaiaDB;Trusted_Connection=True;"
      }
    }
# 3. API Setup (GaiaApi)
-1- Run Database Migrations (using Entity Framework):

    Open Package Manager Console in Visual Studio.

    Run the following commands to set up the database schema:

    Add-Migration InitialCreate
    Update-Database
    This will apply the migrations and create the necessary tables in the database.

-2- Run the API:

    Right-click the GaiaApi project and select Set as Startup Project.

    Press F5 or click Start to run the API.

    The API will be hosted at http://localhost:5000 (or another port, depending on your configuration).

# 4. Web Application Setup (GaiaWeb)
-1- Configure API Endpoint in appsettings.json:

    Open the GaiaWeb project.

    In the appsettings.json file, configure the API URL endpoint:

    {
      "ApiUrl": "http://localhost:5000/api/operations"
    }
-2- Run the Web Application:

    Right-click the GaiaWeb project and select Set as Startup Project.

    Press F5 or click Start to run the web application.

    The Razor Pages web application will be hosted at http://localhost:5001 (or another port, depending on your configuration).
  
# 5. Using the Applications
-1- API Usage:

# Calculate Operation (POST /api/operations/calculate):

    Endpoint: POST /api/operations/calculate

    Body (JSON):
    {
      "FieldA": 10,
      "FieldB": 5,
      "Operation": "add"
    }
    Response:
    {
      "result": "15"
    }
# View Operation History (GET /api/operations/history/{operation}):

    Endpoint: GET /api/operations/history/add

    Response (example):
    [
      {
        "FieldA": 10,
        "FieldB": 5,
        "Operation": "add",
        "Result": "15",
        "Timestamp": "2025-04-07T12:00:00"
      },
      ...
    ]
# Count Operations (GET /api/operations/count/{operation}):

    Endpoint: GET /api/operations/count/add

    Response (example):
    {
      "count": 10
    }

-2- Web Application Usage:

    The web application allows users to input values for Field A, Field B, and choose an Operation (add, subtract, multiply, divide).

    After clicking Calculate, the result will be displayed along with the last 3 operations of that type and the count of operations performed in the current month.

# 6. Important Notes
Make sure your API and web application are running simultaneously (if using separate Visual Studio projects).

If using a different database, update the connection string in the appsettings.json file accordingly.