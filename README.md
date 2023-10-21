# poc-lab

Space for my personal experiments and PoC.

TIP:
Some examples use a Docker image whose definition is available from the repository. For more information, see: [README.md](/data/database-backup/README.md)

## dbup-in-tests

The purpose of this example was to test the possibility of preparing a SQL database for integration testing. Libraries used:
- Testcontainers - Running image with MS SQL database engine.
- EF Core - Migrations - Creating the database and structure 
- Db-Up - Running additional SQL scripts with the execution log as in ef core migrations

More info: [Readme](/scenarios/dbup-in-tests/README.md)

## odata-ef-core

A simple example of using the [OData](https://github.com/OData/AspNetCoreOData) library together with [EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli). In addition, a [SQL database project](https://visualstudio.microsoft.com/vs/features/ssdt/) based on the publicly available [AdventureWorks2022](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks) database has been added to the solution. EFCore models were generated using [EFCore Power Tools](https://github.com/ErikEJ/EFCorePowerTools).

More info: [Readme](/scenarios/odata-ef-core/README.md)


