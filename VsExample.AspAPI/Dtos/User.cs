using Jack.DataScience.Data.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VsExample.AspAPI.Dtos
{
    public class User: DocumentBase
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        [BsonRepresentation(BsonType.String)]
        public List<RoleEnum> Roles { get; set; }
    }
}
