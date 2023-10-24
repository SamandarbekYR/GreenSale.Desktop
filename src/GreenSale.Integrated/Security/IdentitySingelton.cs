using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Security
{
    public class IdentitySingelton
    {
        private static IdentitySingelton _identitySingelton;
        public string Token { get; set; }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  PhoneNumber { get; set; }
        public IdentitySingelton()
        {

        }

        public static IdentitySingelton GetInstance()
        {
            if (_identitySingelton == null)
            {
                _identitySingelton = new IdentitySingelton();
            }

            return _identitySingelton;
        }

    }
}
