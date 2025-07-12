Jewelry price estimation challenge
• Backend in ASP.NET Core and Database can be local DB / In memory

Requirement: We require a small estimation web application for a tiny Jewelry store.

The application has the following abilities to cater the service

Create Api endpoint to save jewellery details with following following inputs. a. Gold price b. Weight of the item c. Discount percentage

Get enpoint to calculate and fetch the final price of jewellery based on jewellery id. Formula for final price. Final price = (gold price * weight in gm) - discount%

Expectations:

backend must have unit tests along with right architecture. * Please use best practices everywhere.


                                                  Jewelry Price Estimation – Project Features

⚙️ Architecture & Technologies

✅ .NET 8 / ASP.NET Core Web API

✅ CQRS Pattern (MediatR) for separation of concerns

✅ Entity Framework Core with SQL Server

✅ xUnit & Moq for unit testing

✅ Middleware for global exception handling

✅ Swagger/OpenAPI support for API testing

.
✅ Global Model Validation (400 errors)

✅ Graceful error handling (500)


🧪 Unit Test Coverage


✔ CreateJewelryCommandHandler tests 

✔ GetFinalPriceQueryHandler tests

✔ API controller tests 


✅ Valid POST & GET requests

✅ 400 Bad Request (invalid payload)

✅ 500 Internal Server Error

📁 Project Structure

JewelryEstimation/

├── Api/               → ASP.NET Core Web API layer

├── Application/       → CQRS Commands, Queries, DTOs

├── Domain/            → Core domain entities (Jewelry)

├── Infrastructure/    → EF Core, Migrations, DbContext

├── UnitTests/         → xUnit tests for logic and APIs



🛠️ Entity Framework Core – Migrations & Database Setup

To apply Entity Framework Core migrations and set up the SQL Server database, follow these steps:

✅ 1. Add a Migration

dotnet ef migrations add InitialCreate --project JewelryEstimation.Infrastructure --startup-project JewelryEstimation.Api

--project: Points to the project that contains DbContext (Infrastructure layer).
--startup-project: Points to the startup project (API layer).

✅ 2. Apply Migration / Update Database

dotnet ef database update --project JewelryEstimation.Infrastructure --startup-project JewelryEstimation.Api

This will create the required database tables in your SQL Server instance.

🛠 Example Swagger Payloads


✅ Create Jewelry

POST /api/jewelry
{
  "goldPrice": 5500,
  "weight": 12,
  "discountPercentage": 5
}

✅ Get Final Price

GET /api/jewelry/1/final-price
