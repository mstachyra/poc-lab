# Use the official SQL Server 2022 base image from Microsoft
FROM mcr.microsoft.com/mssql/server:2022-latest

# Create the backup directory
RUN mkdir /var/opt/mssql/backup

# Copy the database backup file to the container
COPY AdventureWorks2022.bak /var/opt/mssql/backup/AdventureWorks2022.bak

# Set the environment variables for SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>

# Start SQL Server and restore the database backup using SQLCMD
#RUN /opt/mssql/bin/sqlservr & sleep 20s \
    #&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '<YourStrong@Passw0rd>' -Q "RESTORE DATABASE AdventureWorks2022 FROM DISK='/var/opt/mssql/backup/AdventureWorks2022.bak' WITH REPLACE" & sleep 30s \
    #&& pkill sqlservr

# Expose the default SQL Server port
EXPOSE 1433
