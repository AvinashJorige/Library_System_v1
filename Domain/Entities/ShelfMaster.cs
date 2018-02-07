using MongoDB.Bson;
using System;

namespace Domain.Entities
{
    public class ShelfMaster : IEntity
    {
        public ShelfMaster()
        {
            CreatedDate = DateTime.UtcNow.ToString();
            ModifiedDate = DateTime.UtcNow.ToString();
            IsActive = true;
        }

        public ObjectId Id{ get; set; }
        public string ShfId { get; set; }
        public string[] ShfNameList { get; set; }
        public string[] ShfRowNameList { get; set; }
        public string ShfTotal { get; set; }
        public string ShfNumber { get; set; }
        public string ShfRowCapacity { get; set; }
        public string ShfRow { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
