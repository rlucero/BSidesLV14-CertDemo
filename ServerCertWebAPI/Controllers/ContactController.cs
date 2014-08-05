using ServerCertWebAPI.Models;
using ServerCertWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerCertWebAPI.Controllers
{
    public class ContactController : ApiController
    {
        private ContactRepository contactRepository;
        
        public Contact[] Get()
        {
            return this.contactRepository.GetContacts();
        }

        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            bool success = this.contactRepository.PostContact(contact);
            HttpResponseMessage response = null;
            if (success)
            {
                response = Request.CreateResponse<Contact>(HttpStatusCode.Created, contact);
            }
            else
                response = Request.CreateResponse<Contact>(HttpStatusCode.InternalServerError, contact);

            return response;
        }

        public ContactController ()
        {
            this.contactRepository = new ContactRepository();
        }
    }
}
