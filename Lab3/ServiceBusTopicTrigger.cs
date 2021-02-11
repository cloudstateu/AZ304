#r "Newtonsoft.Json"

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

public static void Run(string mySbMsg, ILogger log, out dynamic outputDocument)
{
    log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
    outputDocument = JsonConvert.DeserializeObject<dynamic>(mySbMsg);
}