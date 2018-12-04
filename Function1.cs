using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Newtonsoft.Json;

namespace EventHubFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventHubTrigger("samples-workitems", Connection = "ConnectionString")]string myEventHubMessage, TraceWriter log)
        {
            log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage} to push to cosmoss DB");
            var message = JsonConvert.DeserializeObject<MessageBody>(myEventHubMessage);
            CosmosDB db = new CosmosDB();
            db.Init().Wait();
            db.pushTemperatureDocument(message).Wait();
            //nullify created object
            db = null;
            
        }
    }
}
