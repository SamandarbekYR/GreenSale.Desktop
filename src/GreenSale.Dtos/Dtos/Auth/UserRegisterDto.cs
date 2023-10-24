using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Dtos.Dtos.Auth
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Region { get; set; } = "Farg'ona";
        public string District { get; set; } = "Quva";
        public string Address { get; set; } = "Qoraqum";
        public string Password { get; set; } = string.Empty;
    }
}
