using Microsoft.AspNetCore.SignalR;
using ChatApp.Models;
using ChatApp.MessageBroker;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private static List<Messages> _chatMessages = new List<Messages>();

        public void Connect()
        {
            Clients.Caller.SendAsync("OnConnected", _chatMessages);
        }

        public async Task SendMessage(string user, string message, string time)
        {
            AddMessageInCache(user, message, time);

            await Clients.All.SendAsync("ReceiveMessage", user, message, time);
                       
            string stockCommand = "/stock=";

            //Only if the message has the command is added to the Message broker queue
            if (message.ToLower().IndexOf(stockCommand) >= 0)
            {
                int StockCodePosition = message.ToLower().IndexOf(stockCommand) + stockCommand.Length;
                string StockCode = message.Substring(StockCodePosition);
                Send.SendMessageToBroker(StockCode);
            }
        }

        public async Task GetMessages()
        {
            await Clients.Caller.SendAsync("ReceiveMessages", _chatMessages);
        }

        public static void AddMessageInCache(string userName, string message, string time)
        {
            _chatMessages.Add(new Messages { Username = userName, Message = message, Time = time });

            if (_chatMessages.Count > 50)
            {
                _chatMessages.RemoveAt(0);
            }
        }

    }
}
