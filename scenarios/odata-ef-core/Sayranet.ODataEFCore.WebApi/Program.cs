using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Sayranet.ODataEFCore.WebApi.Controllers;
using Sayranet.ODataEFCore.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntityType<PersonPhone>().HasKey(x => new { x.BusinessEntityId, x.PhoneNumber, x.PhoneNumberTypeId });
modelBuilder.EntityType<Person>().HasKey(x => x.BusinessEntityId).Ignore(x => x.Demographics);
modelBuilder.EntitySet<Customer>("Customers");
modelBuilder.EntitySet<Person>("Persons");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AdventureWorks2022Context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/persons", (AdventureWorks2022Context context) =>
{
    var persons = context.Person.OrderByDescending(x => x.BusinessEntityId).Take(2).ToArray();
    return persons;
})
.WithName("Persons")
.WithOpenApi();

app.MapGet("/api/personsExt", (AdventureWorks2022Context context) =>
{

    //  SELECT[t].[FirstName], [t].[BusinessEntityID], [p0].[BusinessEntityID], [p0].[PhoneNumber], [p0].[PhoneNumberTypeID], [p0].[ModifiedDate]
    //  FROM(
    //      SELECT TOP(@__p_0)[p].[FirstName], [p].[BusinessEntityID]
    //      FROM[Person].[Person] AS[p]
    //      ORDER BY[p].[BusinessEntityID] DESC
    //  ) AS[t]
    //  LEFT JOIN[Person].[PersonPhone] AS[p0] ON[t].[BusinessEntityID] = [p0].[BusinessEntityID]
    //  ORDER BY[t].[BusinessEntityID] DESC, [p0].[BusinessEntityID], [p0].[PhoneNumber]

    // From OData

    //  SELECT[t].[BusinessEntityID], [p0].[BusinessEntityID], [p0].[PhoneNumber], [p0].[PhoneNumberTypeID], [p0].[ModifiedDate], [t].[FirstName]
    //  FROM(
    //      SELECT TOP(@__TypedProperty_2)[p].[FirstName], [p].[BusinessEntityID]
    //      FROM[Person].[Person] AS[p]
    //      ORDER BY[p].[BusinessEntityID] DESC
    //  ) AS[t]
    //  LEFT JOIN[Person].[PersonPhone] AS[p0] ON[t].[BusinessEntityID] = [p0].[BusinessEntityID]
    //  ORDER BY[t].[BusinessEntityID] DESC, [p0].[BusinessEntityID], [p0].[PhoneNumber]

    var persons = context.Person
        .Include(x => x.PersonPhone)
        .OrderByDescending(x => x.BusinessEntityId)
        .Select(x => new
        {
            x.FirstName,
            PersonPhone = x.PersonPhone.Select(p => new
            {
                p.BusinessEntityId,
                p.PhoneNumber,
                p.PhoneNumberTypeId,
                p.ModifiedDate
            })
        })
        .Take(2).ToArray();
    return persons;
})
.WithName("ExtPersons")
.WithOpenApi();

app.Run();