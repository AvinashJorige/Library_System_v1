using MongoDB.Bson;
using System;

namespace Domain.Entities
{
    public class BooksCollection : IEntity
    {
        public BooksCollection()
        {
            CreatedDate = DateTime.UtcNow.ToString();
            ModifiedDate = DateTime.UtcNow.ToString();
            IsActive = true;
        }

        public ObjectId Id { get; set; }

        public string BkId { get; set; }
        public string BkTitle { get; set; }
        public string BkAuther { get; set; }
        public string BkISBN { get; set; }
        public string BkEditionNumber { get; set; }
        public string BkCopyright { get; set; }
        public string BkNo_Of_Copies_Actual { get; set; }
        public string BkNo_Of_Copies_Current { get; set; }
        public string BkRating { get; set; }
        public string BkCost { get; set; }
        public string BkCategoryID { get; set; }
        public string BKShelfID { get; set; }

        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
