using MongoDB.Bson;
using System;

namespace Domain.Entities
{
    public class Category_Details :IEntity
    {
        public Category_Details()
        {
            CreatedDate = DateTime.UtcNow.ToString();
            ModifiedDate = DateTime.UtcNow.ToString();
            IsActive = true;
        }

        public ObjectId Id { get; set; }
        public string Category_ID { get; set; }
        public string Category_Name { get; set; }

        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
