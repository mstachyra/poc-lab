using DbUp;
using DbUp.Helpers;
using DbUp.ScriptProviders;
using Microsoft.EntityFrameworkCore;
using Sayranet.EFCoreSample;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sayranet.DbUpSample
{
    public static class DbUpManager
    {
        public static int Run(string connectionString, bool withEFCoreMigration)
        {
            if (withEFCoreMigration)
            {
                RunEFCoreMigration(connectionString);
            }

            Console.WriteLine("Start executing predeployment scripts...");
            var preDeploymentScriptsExecutor =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssemblies(new[]
                    {
                        typeof(DbUpManager).Assembly
                    },
                    (string s) => s.StartsWith("Sayranet.DbUpSample.Scripts.PreDeployments"))
                    .LogToConsole()
                    .LogScriptOutput()
                    .JournalTo(new NullJournal())
                    .Build();

            var preDeploymentUpgradeResult = preDeploymentScriptsExecutor.PerformUpgrade();

            if (!preDeploymentUpgradeResult.Successful)
            {
                throw new Exception(preDeploymentUpgradeResult.Error.ToString());
            }

            ShowSuccess();

            Console.WriteLine("Start executing migration scripts...");
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssemblies(new[]
                    {
                        typeof(DbUpManager).Assembly
                    },
                    (string s) => s.StartsWith("Sayranet.DbUpSample.Scripts.Migration"))
                    .LogToConsole()
                    .LogScriptOutput()
                    .JournalToSqlTable("dbo", "__DbUpMigrationsHistory")
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception(result.Error.ToString());
            }

            ShowSuccess();

            Console.WriteLine("Start executing postdeployment scripts...");
            var postDeploymentScriptsExecutor =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssemblies(new[]
                    {
                        typeof(DbUpManager).Assembly
                    },
                    (string s) => s.StartsWith("Sayranet.DbUpSample.Scripts.PostDeployments"))
                    .LogToConsole()
                    .LogScriptOutput()
                    .JournalTo(new NullJournal())
                    .Build();

            var postdeploymentUpgradeResult = postDeploymentScriptsExecutor.PerformUpgrade();

            if (!postdeploymentUpgradeResult.Successful)
            {
                throw new Exception(postdeploymentUpgradeResult.Error.ToString());
            }

            ShowSuccess();

            return 0;
        }

        private static void RunEFCoreMigration(string connectionString)
        {
            var contextOptions = new DbContextOptionsBuilder<EFSampleDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new EFSampleDbContext(contextOptions);

            context.Database.Migrate();
        }

        private static void ShowSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }

        private static int ReturnError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
            return -1;
        }
    }
}