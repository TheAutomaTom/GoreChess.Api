using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoreChess.Serverless.Functions
{
    public static class TestFunction
    {
        [FunctionName("SendMessage")]
        public static async Task<IActionResult> SendMessage(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string message = req.Query["message"];

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
              var signalRMessage = new SignalRMessage
              {
                Target = "newMessage",
                Arguments = new object[] { name, message }
              };
              await signalRMessages.AddAsync(signalRMessage);
              return new OkObjectResult($"Message sent to SignalR hub");
            }
            else
            {
              return new BadRequestObjectResult("Please pass a name and message in the query string");
            }
         }
    }
}
