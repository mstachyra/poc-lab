using DotNet.Testcontainers.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace Sayranet.DbUpBaseFirstTests
{
    public class MsSqlTestcontainerFixture : IAsyncLifetime
    {
        public MsSqlContainer Container { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            Container = new MsSqlBuilder()
                .WithImage("sqlserver-adventure-works-image:latest")
                .Build();

            await Container.StartAsync();
        }

        public async Task DisposeAsync()
        {
            if (Container != null)
            {
                await Container.StopAsync();
            }
        }
    }
}
