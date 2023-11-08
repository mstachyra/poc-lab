using MassTransit;

namespace MTLab.ModuleA
{
    public class MessageAConsumer : IConsumer<MessageA>
    {
        readonly ILogger<MessageAConsumer> _logger;

        public MessageAConsumer(ILogger<MessageAConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MessageA> context)
        {
            _logger.LogInformation("Received : {message}, processing...", context.Message);

            await Task.Delay(1000);

            _logger.LogInformation("{message}, DONE.", context.Message);
        }
    }
}
