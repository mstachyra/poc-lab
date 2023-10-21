# dbup-in-tests

The purpose of this example was to test the possibility of preparing a SQL database for integration testing. Libraries used:
- Testcontainers - Running image with MS SQL database engine.
- EF Core - Migrations - Creating the database and structure 
- Db-Up - Running additional SQL scripts with the execution log as in ef core migrations