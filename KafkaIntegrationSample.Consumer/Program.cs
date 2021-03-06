﻿using System;
using System.Text;

namespace KafkaIntegrationSample.Consumer
{
    using KafkaNet;
    using KafkaNet.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"));
            var router = new BrokerRouter(options);
            var consumer = new Consumer(new ConsumerOptions("test", router));

            //Consume returns a blocking IEnumerable (ie: never ending stream)
            foreach (var message in consumer.Consume())
            {
                Console.WriteLine("Response: P{0},O{1} : {2}",
                   message.Meta.PartitionId, message.Meta.Offset,
                   Encoding.UTF8.GetString(message.Value));
            }
        }
    }
}
