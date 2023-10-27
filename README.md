# poc-lab

Space for my personal experiments and PoC.

TIP:
Some examples use a Docker image whose definition is available from the repository. For more information, see: [README.md](/data/database-backup/README.md)

## dbup-in-tests

The purpose of this example was to test the possibility of preparing a SQL database for integration testing. Libraries used:
- [Testcontainers for .NET](https://dotnet.testcontainers.org/) - Running docker image from unit test with MS SQL database engine.
- [EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli) - Migrations - Creating the database and structure 
- [Db-Up](https://dbup.readthedocs.io/en/latest/) - Running additional SQL scripts with the execution log as in ef core migrations

More info: [Readme](/scenarios/dbup-in-tests/README.md)

## odata-ef-core

A simple example of using the [OData](https://github.com/OData/AspNetCoreOData) library together with [EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli). In addition, a [SQL database project](https://visualstudio.microsoft.com/vs/features/ssdt/) based on the publicly available [AdventureWorks2022](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks) database has been added to the solution. EFCore models were generated using [EFCore Power Tools](https://github.com/ErikEJ/EFCorePowerTools).

More info: [Readme](/scenarios/odata-ef-core/README.md)

## workflow-lab

TODO - Try and do some experiments with [ELSA](https://v3.elsaworkflows.io/) library. 
> Elsa Workflows is a powerful and flexible execution engine, encapsulated as a set of open-source .NET libraries designed to infuse .NET applications with workflow capabilities. With Elsa, developers can weave logic directly into their systems, enhancing functionality and automation for a seamless user experience.

## service-bus

TODO experiments with libraries:
- [Rebus](https://github.com/rebus-org/Rebus) - Rebus is a lean service bus implementation for .NET
- [MassTransit](https://github.com/MassTransit/MassTransit) - MassTransit provides a developer-focused, modern platform for creating distributed applications without complexity.

