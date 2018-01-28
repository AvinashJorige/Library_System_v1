using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdminModelEntity : IEntity
    {
        public AdminModelEntity()
        {
            CreatedDate = DateTime.UtcNow.ToString();
            ModifiedDate = DateTime.UtcNow.ToString();
            IsActive = true;
        }

        public string Id { get; set; }
        public string adCode { get; set; }
        public string adName { get; set; }
        public string adPassword { get; set; }
        public string adEmail { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
