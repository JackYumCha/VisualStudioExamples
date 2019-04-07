using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace VsExample.Data.Entities
{
    public class PersonEntity
    {
        [BsonId]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
