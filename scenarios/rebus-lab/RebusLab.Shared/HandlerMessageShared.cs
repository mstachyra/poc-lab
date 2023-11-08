using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace RebusLab.Shared
{
    public class HandlerMessageShared : IHandleMessages<MessageShared>
    {
        private readonly ILogger _logger;

        public HandlerMessageShared(ILogger<HandlerMessageShared> logger)
        {
            _logger = logger;
        }

        public async Task Handle(MessageShared message)
        {
            _logger.LogInformation("HandlerMessageShared received : {message}, processing...", message);

            await Task.Delay(1000);

            _logger.LogInformation("{message}, DONE.", message);
        }
    }
}
