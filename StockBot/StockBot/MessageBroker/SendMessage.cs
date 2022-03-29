using RabbitMQ.Client;
using System.Text;

namespace StockBot.MessageBroker
{
    public class SendMessage
    {
        public static void SendMessageToBroker(string Message)
        {
            string queueName ="ChatAppMessagesQueue";
            var factory = new ConnectionFactory() { HostName = "localhost" };     
           
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                                 
                    var messageBody = Encoding.UTF8.GetBytes(Message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: messageBody);
                }
            }
        }
    }
}
