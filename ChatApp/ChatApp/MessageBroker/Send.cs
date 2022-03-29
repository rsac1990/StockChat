using RabbitMQ.Client;
using System.Text;

namespace ChatApp.MessageBroker
{
    public class Send
    {
        public static void SendMessageToBroker(string Message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "BotMessagesQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = Message;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "BotMessagesQueue",
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}
