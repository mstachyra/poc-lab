using Rebus.Handlers;

namespace RebusLab.ModuleB
{
    public class HandlerMessageB : IHandleMessages<MessageB>
    {
        private readonly ILogger _logger;

        public HandlerMessageB(ILogger<MessageB> logger)
        {
            _logger = logger;
        }

        public async Task Handle(MessageB message)
        {
            _logger.LogInformation("Handler received : {message}, processing...", message);

            await Task.Delay(1000);

            _logger.LogInformation("{message}, DONE.", message);
        }
    }
}
