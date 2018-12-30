using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetcoreapp
{
    public class Messages { 


    public ObjectId Id { get; set; }

    [BsonElement("Author")]
    public string Author { get; set; }

    [BsonElement("Message")]
    public string Message { get; set; }
    }

}
