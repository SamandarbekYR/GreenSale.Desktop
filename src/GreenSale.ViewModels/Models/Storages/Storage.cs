using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.ViewModels.Models.Storages
{
    public class Storage
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public double AverageStars { get; set; }
    }
}
