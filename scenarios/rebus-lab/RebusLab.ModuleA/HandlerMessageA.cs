using Rebus.Handlers;

namespace RebusLab.ModuleA;

public class HandlerMessageA : IHandleMessages<MessageA>
{
    private readonly ILogger _logger;

    public HandlerMessageA(ILogger<MessageA> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MessageA message)
    {
        _logger.LogInformation("Handler received : {message}, processing...", message);

        await Task.Delay(1000);

        _logger.LogInformation("{message}, DONE.", message);
    }
}
