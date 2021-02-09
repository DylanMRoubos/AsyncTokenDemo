using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CancellationTokenExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlowRequestController : Controller
    {
        private readonly ILogger _logger;

        public SlowRequestController(ILogger<SlowRequestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/slowtest")]
        public async Task<string> Get()
        {
            _logger.LogInformation("Starting to do slow work");

            await Task.Delay(10_000);

            var message = "Finished slow delay of 10 seconds.";

            _logger.LogInformation(message);

            return message;
        }

        [HttpGet("/slowtestwithcancel")]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to do slow work");

            await Task.Delay(10_000, cancellationToken);

            var message = "Finished slow delay of 10 seconds.";

            _logger.LogInformation(message);

            return message;
        }
    }
}
