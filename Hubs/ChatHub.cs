using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace aspnetcoreapp.Hubs
{
    public class ChatHub : Hub
    {

        public IMongoCollection<Messages> _messages; 

        public async Task SendMessage(string user, string message)
        {
            

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Db");
            _messages = database.GetCollection<Messages>("messages");

            Messages msg = new Messages();
            msg.Author = user;
            msg.Message = message;

            await _messages.InsertOneAsync(msg);

            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }

        public async Task ShowPrevMessages()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Db");
            _messages = database.GetCollection<Messages>("messages");

            List<Messages> list = new List<Messages>();

            list = _messages.Find(message => true).ToList();
            int length = list.Count;
            string user1 = list[length - 1].Author;
            string message1 = list[length - 1].Message;
            string user2 = list[length - 2].Author;
            string message2 = list[length - 2].Message;
            string user3 = list[length - 3].Author;
            string message3 = list[length - 3].Message;

            await Clients.Caller.SendAsync("PreviousMessages", user1 , message1, user2, message2, user3, message3 );
        }

    }
}
