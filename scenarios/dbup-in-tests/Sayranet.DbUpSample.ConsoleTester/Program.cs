
using Sayranet.DbUpSample;

DbUpManager.Run(
    connectionString: @"Server=.,1477;Database=Test;User Id=sa;Password=<YourStrong@Passw0rd>;TrustServerCertificate=Yes", 
    withEFCoreMigration: true);

