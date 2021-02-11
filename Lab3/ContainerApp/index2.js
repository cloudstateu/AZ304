const { EventHubProducerClient } = require("@azure/event-hubs");
async function main() {
  const producerClient = new EventHubProducerClient("EVENTHUB_CONNECTION_STRING", "databroker");
    setInterval(async function(){
        const eventDataBatch = await producerClient.createBatch();
        const data = { 
            messageType: Math.floor(Math.random() * 3) + 1,
            a: Math.floor(Math.random() * (500 - 0 + 1) + 0),
            b: Math.floor(Math.random() * (500 - 0 + 1) + 0),
            Time: new Date()
        };
        let wasAdded = eventDataBatch.tryAdd({ body: data });
        console.log(data);
        console.log(wasAdded);
        await producerClient.sendBatch(eventDataBatch);
        console.log("message sent successfully.");
    }, 1000);  
    await producerClient.close();
}

main().catch((err) => {
    console.log(err);
});