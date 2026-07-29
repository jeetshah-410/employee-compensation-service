# Employee Compensation Service

This is a backend service for managing employee and department records, built as a set of HTTP-triggered Azure Functions using **C# / .NET 10 (Isolated Worker Model)** and **Entity Framework Core**.

## Steps to Run Locally

### 1. Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Azure Functions Core Tools (v4)](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- A local SQL Server instance (e.g., LocalDB or SQL Express)

### 2. Database Setup
1. Open SQL Server Management Studio (SSMS) or Azure Data Studio.
2. Create a local database (e.g., `EmployeeCompensationDB`).
3. Run the scripts in the `/sql` folder in order:
   - `01_create_tables.sql` (Creates the `Department` and `Employee` tables)
   - `02_seed_data.sql` (Seeds edge-case data for testing the reports)

### 3. Application Configuration
1. Open `local.settings.json` in the root of the project.
2. Update the `SqlConnectionString` under the `Values` section so the `Server=` property matches your local SQL Server name (e.g., `(localdb)\MSSQLLocalDB` or `.\SQLEXPRESS`).

### 4. Run the API
Open a terminal in the project root and start the Functions host:
```bash
dotnet clean
func start
```

## Testing the Endpoints
To test the API easily, a `requests.http` file is provided in the project root. 
- **VS Code / Visual Studio:** Open the file and click "Send Request" to test endpoints directly in your IDE.
- **Postman / curl:** You can use the file as a plain text reference for the URLs, methods, and JSON payloads.

## Production Deployment Plan

While this project is currently configured for local evaluation, the assignment asked for production-readiness regarding secrets. To address this, I ensured connection strings aren't hardcoded in the source code; they are read from configuration (`local.settings.json` locally).

If I were taking this to a true Azure production environment, my deployment plan would be:

1. **Hosting & Database:** I would deploy the Functions using a serverless Consumption Plan (cost-effective and scalable) backed by a basic Azure SQL Database tier.
2. **Secrets Management:** The connection string would be removed entirely from App Settings. I would use a System-Assigned Managed Identity on the Function App to pull the connection string directly and securely from **Azure Key Vault** at startup.