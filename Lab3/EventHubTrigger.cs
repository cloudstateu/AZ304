#r "Microsoft.Azure.EventHubs"

using System;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;

public static async Task Run(
 EventData[] events,
 ILogger log,
 IAsyncCollector<string> outputSbMsg1,
 IAsyncCollector<string> outputSbMsg2,
 IAsyncCollector<string> outputSbMsg3)
{
    var exceptions = new List<Exception>();

    foreach (EventData eventData in events)
    {
        try
        {
            string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

            await outputSbMsg3.AddAsync(messageBody);
            if (messageBody[15] == '1')
            {
                await outputSbMsg1.AddAsync(messageBody);
                log.LogInformation($"C# Event Hub trigger function send a message to topic1: {messageBody}");
            }
            else if (messageBody[15] == '2') 
            {
                await outputSbMsg2.AddAsync(messageBody);
                log.LogInformation($"C# Event Hub trigger function send a message to topic2: {messageBody}");
            }
            else if (messageBody[15] == '3') 
            {
                await outputSbMsg3.AddAsync(messageBody);
                log.LogInformation($"C# Event Hub trigger function send a message to topic3: {messageBody}");
            }
            else
                log.LogInformation("Event Hub trigger function didn't send a message to any topic");
        }
        catch (Exception e)
        {
            exceptions.Add(e);
        }
    }

    if (exceptions.Count > 1)
        throw new AggregateException(exceptions);

    if (exceptions.Count == 1)
        throw exceptions.Single();
}