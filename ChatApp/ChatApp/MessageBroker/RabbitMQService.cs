using ChatApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ChatApp.MessageBroker
{
    public class RabbitMQService : IRabbitMQService
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        protected readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            // Opens the connections to RabbitMQ
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _serviceProvider = serviceProvider;
        }

        public virtual void Connect()
        {
            
            string queueName = "ChatAppMessagesQueue";
            _channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);          
      
            //this section excecutes every time there is a message in the queue
            consumer.Received += (model, eventArguments) =>
            {

                var body = eventArguments.Body.ToArray();
                var stockInfoMessage = Encoding.UTF8.GetString(body);

                ChatHub.AddMessageInCache("StockBot", stockInfoMessage, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                                
                var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));
                               
                chatHub.Clients.All.SendAsync("ReceiveMessage", "StockBot", stockInfoMessage, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

            };

            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);            
        }
    }
}
