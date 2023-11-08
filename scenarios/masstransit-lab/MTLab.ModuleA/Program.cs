using MassTransit;
using MTLab.ModuleA;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageAConsumer>();

    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddMassTransitHostedService(true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/publish/10-message-type-a", async (HttpContext context,
    IBus bus, ILogger<MessageA> logger) =>
{
    logger.LogInformation("Publishing {MessageCount} messages", 10);

    await Task.WhenAll(
        Enumerable.Range(0, 10)
            .Select(i => new MessageA())
            .Select(message => bus.Publish(message)));

    context.Response.ContentType = "text";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("MassTransit sent another 10 messages!");
})
.WithName("Publish")
.WithOpenApi();

//app.MapGet("/publish/10-message-shared", async (HttpContext context,
//    IBus bus, ILogger<MessageShared> logger) =>
//{
//    logger.LogInformation("Publishing {MessageCount} messages", 10);

//    await Task.WhenAll(
//        Enumerable.Range(0, 10)
//            .Select(i => new MessageShared())
//            .Select(message => bus.Send(message)));

//    await bus.Publish(new MessageShared());

//    context.Response.ContentType = "text";
//    context.Response.StatusCode = 200;
//    await context.Response.WriteAsync("Rebus sent another 10 messages and 1 publish!");
//})
//.WithName("PublishShared")
//.WithOpenApi();

app.Run();