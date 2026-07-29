# Employee Compensation Service

This is the backend service for the Employee Compensation assignment. It manages employee and department records and provides specific reporting endpoints, built as a set of HTTP-triggered Azure Functions.

Tech stack: **C# / .NET 10 (Isolated Worker Model)**, **Entity Framework Core**, and **Azure SQL**.

## 🚀 How to Run Locally

### 1. Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Azure Functions Core Tools (v4)](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- A local SQL Server instance (e.g., LocalDB or SQL Express)

### 2. Database Setup
1. Create a local database (e.g., `EmployeeCompensationDB`).
2. Run the scripts in the `/sql` folder in order:
   - `01_create_tables.sql` (Schema setup)
   - `02_seed_data.sql` (Sample data specifically designed to test the Part B edge cases)
3. Update the `SqlConnectionString` in `local.settings.json` to point to your local database. *(Note: This file is git-ignored to keep secrets out of source control).*

### 3. Run the API
Open a terminal in the project root and start the Functions host:
```bash
func start

---

## ☁️ Production Deployment Plan

While this project is currently configured for local evaluation, the assignment asked for production-readiness regarding secrets. To address this, I ensured connection strings aren't hardcoded in the source code; they are read from configuration (`local.settings.json` locally).

If I were taking this to a true Azure production environment, my deployment plan would be:

1. **Hosting & Database:** I would deploy the Functions using a serverless Consumption Plan (cost-effective and scalable) backed by a basic Azure SQL Database tier.
2. **Secrets Management:** The connection string would be removed entirely from App Settings. I would use a System-Assigned Managed Identity on the Function App to pull the connection string directly and securely from **Azure Key Vault** at startup.
3. **Automated Deployments:** I'd manage the database schema changes using a DACPAC deployed via an Azure DevOps CI/CD pipeline. This automates the rollout and prevents configuration drift across environments.