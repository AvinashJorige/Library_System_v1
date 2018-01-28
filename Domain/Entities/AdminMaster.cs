using System;
namespace Domain.Entities
{
    public class AdminMaster : IEntity
    {
        public AdminMaster()
        {
            CreatedDate = DateTime.UtcNow.ToString();
            ModifiedDate = DateTime.UtcNow.ToString();
            logInDateTime = DateTime.UtcNow.ToString();
            IsActive = true;
        }

        public string Id { get; set; }
        public string adCode { get; set; }
        public string adName { get; set; }
        public string adPassword { get; set; }
        public string adEmail { get; set; }
        public string PasswordHashKey { get; set; }
        public string logInDateTime { get; set; }
        public string UserRole { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
