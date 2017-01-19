using System;

namespace KafkaIntegrationSample
{
    using KafkaNet;
    using KafkaNet.Model;
    using KafkaNet.Protocol;

    class Program
    {
        static void Main(string[] args)
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"));
            var router = new BrokerRouter(options);

            using (var client = new Producer(router))
            {
                client.SendMessageAsync("test", new[] { new Message("Hi Hello! Welcome to Kafka!") }).Wait();
            }

            Console.ReadLine();
        }
    }
}
