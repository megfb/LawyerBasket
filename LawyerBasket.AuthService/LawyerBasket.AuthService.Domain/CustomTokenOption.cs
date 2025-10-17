using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.AuthService.Domain
{
    public class CustomTokenOption
    {
        public string Issuer { get; set; }
        public List<string> Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public string SecurityKey { get; set; }
    }
}
