using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
using RebusLab.ModuleB;
using RebusLab.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register handlers 
builder.Services.AutoRegisterHandlersFromAssemblyOf<HandlerMessageB>();
builder.Services.AutoRegisterHandlersFromAssemblyOf<HandlerMessageShared>();

var rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMq");

// Configure and register Rebus
builder.Services.AddRebus(configure => configure
        //.Logging(l => ...) //< do not configure logging - it will automatically use the host's logging
        //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "Messages"))
        .Transport(t => t.UseRabbitMq(rabbitMqConnectionString, "Messages"))
        .Routing(r => {
            r.TypeBased()
                .Map<MessageB>("Messages")
                .Map<MessageShared>("Messages");
        })
        .Options(opt =>
        {
            opt.SetMaxParallelism(2);
        })
    );

// WARN: If you stard as Docker, set breakpoint in .AddRebus line and run comands below

// docker network create rebuslab-net
// docker run -d -p 15672:15672 -p 5672:5672 --network rebuslab-net --hostname rabbitmqhostname --name rabbitmqname rabbitmq:management
// docker network connect rebuslab-net RebusLab.ModuleB

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/publish/10-message-type-b", async (HttpContext context, 
    IBus bus, ILogger<MessageB> logger) =>
{
    logger.LogInformation("Publishing {MessageCount} messages", 10);

    await Task.WhenAll(
        Enumerable.Range(0, 10)
            .Select(i => new MessageB())
            .Select(message => bus.Send(message)));

    context.Response.ContentType = "text";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Rebus sent another 10 messages!");
})
.WithName("Publish")
.WithOpenApi();

app.MapGet("/publish/10-message-shared", async (HttpContext context, 
    IBus bus, ILogger<MessageShared> logger) =>
{
    logger.LogInformation("Publishing {MessageCount} messages", 10);

    await Task.WhenAll(
        Enumerable.Range(0, 10)
            .Select(i => new MessageShared())
            .Select(message => bus.Send(message)));

    context.Response.ContentType = "text";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Rebus sent another 10 messages!");
})
.WithName("PublishShared")
.WithOpenApi();

app.Run();