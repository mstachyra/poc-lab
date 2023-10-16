using Microsoft.EntityFrameworkCore;
using Sayranet.EFCoreSample;

var contextOptions = new DbContextOptionsBuilder<EFSampleDbContext>()
    .UseSqlServer(@"Server=.,1477;Database=Test;User Id=sa;Password=<YourStrong@Passw0rd>;TrustServerCertificate=Yes")
    .Options;

using var context = new EFSampleDbContext(contextOptions);

await context.Database.MigrateAsync();

context.Materials.Add(new Material { Name = "First" });
var countInserted = await context.SaveChangesAsync();
var countAll = await context.Materials.CountAsync();

Console.WriteLine($"Done! countInserted:{countInserted} , countAll:{countAll}");
