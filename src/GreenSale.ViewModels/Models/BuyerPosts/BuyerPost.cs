using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.ViewModels.Models.BuyerPosts
{
    public class BuyerPost
    {
        public long Id { get; set; }
        public string region { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public double price { get; set; }
        public int capacity { get; set; }
        public string capacityMeasure { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public DateTime updatedAt { get; set; }
        public int status { get; set; } 
        public string mainImage { get; set; } = string.Empty;
        public double AverageStars { get; set; }
        public double UserStars { get; set; }
    }
}
