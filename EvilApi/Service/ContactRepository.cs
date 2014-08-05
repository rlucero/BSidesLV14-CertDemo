using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvilApi.Service
{
    public class ContactRepository
    {
        private const string CacheKey = "ObjectStore";
        public Object[] GetStolenObjects()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
            {
                return null;
            }

            return (Object[])context.Cache[CacheKey]; 
        }

        public bool PostContact(Object incomingObject)
        {
            bool result = false;
            try
            {
                List<Object> objects = new List<Object>(GetStolenObjects());
                objects.Add(incomingObject);
                HttpContext.Current.Cache[CacheKey] = objects.ToArray();
                result = true;
            }
            catch (Exception)
            {
                //Handle error or log issue.
            }
            return result;
        }

        public ContactRepository()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                if (context.Cache[CacheKey] == null)
                {
                    var contacts = new Object[] 
                    {
                        new Object()
                        
                    };
                    context.Cache[CacheKey] = contacts;
                }
            }
        }
    }
}