using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerCertWebAPI.Models
{
    public class Contact
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string CreditCardNumber
        {
            get;
            set;
        }
    }
}