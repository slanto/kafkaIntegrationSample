using System;
using System.Text;

namespace KafkaIntegrationSample.ProducerService
{
    using System.Threading.Tasks;

    using RdKafka;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataTransferService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataTransferService.svc or DataTransferService.svc.cs at the Solution Explorer and start debugging.
    public class DataTransferService : IDataTransferService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
      
        public async Task<DeliveryResult> PushDataAsync(string message)
        {                        
            using (Producer producer = new Producer("127.0.0.1:9092"))
            using (Topic topic = producer.Topic("test"))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                DeliveryReport deliveryReport = await topic.Produce(data);                
                return new DeliveryResult() { Offset = deliveryReport.Offset, Partition = deliveryReport.Partition };
            }
        }
    }
}
