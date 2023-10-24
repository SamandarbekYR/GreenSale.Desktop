using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Dtos.Dtos.Storages
{
    public class StorageUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double AddressLatitude { get; set; }
        public double AddressLongitude { get; set; }
        public string Info { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
