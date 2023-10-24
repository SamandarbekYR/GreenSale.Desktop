    using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Dtos.Dtos.Storages
{
    public class StorageDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double AddressLatitude { get; set; } = 128439;
        public double AddressLongitude { get; set; } = 29046;
        public string Info { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
