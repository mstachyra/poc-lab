using Microsoft.Data.SqlClient;
using System.Data;
using FluentAssertions;
using Sayranet.DbUpSample;

namespace Sayranet.DbUpBaseFirstTests
{
    public class MsSqlTests : IClassFixture<MsSqlTestcontainerFixture>
    {
        private readonly MsSqlTestcontainerFixture _fixture;

        public MsSqlTests(MsSqlTestcontainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task WhenOnRestoreDbSelectNotExistedTable_ShouldThrowException()
        {
            var connectionString = _fixture.Container.GetConnectionString();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var createDbCommandRESTORE = new SqlCommand("""
                RESTORE DATABASE AdventureWorks2022 FROM DISK='/var/opt/mssql/backup/AdventureWorks2022.bak' WITH REPLACE,
                MOVE 'AdventureWorks2022' TO '/var/opt/mssql/data/AdventureWorks2022.mdf',
                MOVE 'AdventureWorks2022_log' TO '/var/opt/mssql/data/AdventureWorks2022_Log.ldf'
                """, connection);
            createDbCommandRESTORE.ExecuteNonQuery();

            var act = () =>
            {
                // Execute SQL on restored Database
                using var createDbCommand = new SqlCommand("SELECT COUNT(*) FROM [AdventureWorks2022].[Person2].[Person]", connection);
                createDbCommand.ExecuteNonQuery();
            };

            connection.State.Should().Be(ConnectionState.Open);
            act.Should().Throw<SqlException>().WithMessage("Invalid object name 'AdventureWorks2022.Person2.Person'.");
        }

        [Fact]
        public async Task WhenCallMigrationsFromEFCoreAndDbUp_ThenSelectShouldReturnNamesFromView()
        {
            var connectionString = _fixture.Container.GetConnectionString();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Run Migration
            DbUpManager.Run(connectionString, withEFCoreMigration: true);

            // Execute SQL on restored Database
            using var createDbCommand = new SqlCommand("SELECT Count(*) FROM [dbo].[MaterialNames]", connection);
            createDbCommand.ExecuteNonQuery();

            connection.State.Should().Be(ConnectionState.Open);
        }
    }
}