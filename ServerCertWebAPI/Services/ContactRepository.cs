using ServerCertWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace ServerCertWebAPI.Services
{
    public class ContactRepository
    {
        private const string CacheKey = "ContactStore";
        public Contact[] GetContacts()
        {
            HttpContext context = HttpContext.Current;
            if(context != null)
            {
                return (Contact[])context.Cache[CacheKey];
            }
            return new Contact[] 
            {
                new Contact
                {
                    Id = 0,
                    Name = "Placeholder",
                    CreditCardNumber = "0000-0000-0000-0000"
                    
                }
            };
        }

        public bool PostContact(Contact contact)
        {
            bool result = false;
            try
            {
                List<Contact> contacts = new List<Contact>(GetContacts());
                contacts.Add(contact);
                HttpContext.Current.Cache[CacheKey] = contacts.ToArray();
                result = true;
            }
            catch(Exception)
            {
                //Handle error or log issue.
            }
            return result;
        }

        public ContactRepository()
        {
            HttpContext context = HttpContext.Current;
            if(context != null)
            {
                if (context.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[] 
                    {
                        new Contact
                        {
                            Id = 1,
                            Name = "Jim Random",
                            CreditCardNumber = "1122-3344-5566-7788"
                    
                        },
                        new Contact
                        {
                            Id = 2,
                            Name = "Shelby Customer",
                            CreditCardNumber = "1092-8347-5665-7483"
                        }
                    };
                    context.Cache[CacheKey] = contacts;
                }
            }
        }
    }
}