using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockBot.MessageBroker;
using StockBot.Models;
using StockBot.Services;
using System.Text;

namespace StockBot
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string queueName = "BotMessagesQueue";

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);


                consumer.Received +=  (ch, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var StockCode = Encoding.UTF8.GetString(body);

                    string replyText = string.Empty;

                    StockInfo stockInfo =  StockService.GetStockInfo(StockCode);

                    if (stockInfo!= null)
                    {
                        replyText = $"{stockInfo.Code.ToUpper()} quote is ${stockInfo.ClosingValue} per share. ";
                    }
                    else
                    {
                        replyText = $"The stock code {StockCode.ToUpper()} was not found.";
                    }

                    SendMessage.SendMessageToBroker(replyText);

                    channel.BasicAck(ea.DeliveryTag, false);
                };
      
                string consumerTag = channel.BasicConsume(queueName, false, consumer);

                CreateHostBuilder(args).Build().Run();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
