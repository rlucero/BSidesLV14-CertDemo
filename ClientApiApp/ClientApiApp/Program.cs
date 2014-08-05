using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientApiApp
{
    /// <summary>
    /// API Demo Client App
    /// </summary>
    class Program
    {
        static HttpClient httpClient;
        static void Main(string[] args)
        {
            ConfigureHttpClient();
            //Loop forever until ctrl-c out
            while(true)
            {
                PostToApiClient();
                
            }
        }

        /// <summary>
        /// Configure HTTP client with Base URL in config.
        /// </summary>
        private static void ConfigureHttpClient()
        {
            httpClient = new HttpClient();
            string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            Console.WriteLine("Connecting to: {0}", baseUrl);
            httpClient.BaseAddress = new Uri(baseUrl);
            
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Method for making APIClient Post Call
        /// </summary>
        static void PostToApiClient()
        {
            //Collect info for post

            Console.WriteLine("POST to API Client");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Credit Card Number: ");
            string crediCardNumber = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("ID: {0}/r/n Name: {1}/r/n Credit Card Number {2} /r/n", id.ToString(), name, crediCardNumber);
            Contact contact = new Contact();
            contact.CreditCardNumber = crediCardNumber;
            contact.Name = name;
            contact.Id = id;

            //Post data via async call to endpoint
            string url = "certapi/api/contact";
            RunPost(url, contact).Wait(1000);
        }

        /// <summary>
        /// Async post method to endpoint
        /// </summary>
        /// <param name="url">API Endpoint URL</param>
        /// <param name="newContact">Contact in request body</param>
        /// <returns>Task with response</returns>
        private static async Task RunPost(string url, Contact newContact)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, newContact);
            }
            catch(HttpRequestException ex)
            {
                //Post excption, the inner exception usually has the specific details
                Console.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}
