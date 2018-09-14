using Domain.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter devide id");
            var deviseId = Console.ReadLine();
            SendMessagesToRestApi(deviseId);
            Console.ReadKey();
        }

        private static void SendMessagesToRestApi(string deviceId = "dev1")
        {
            try
            {
                var hostUrl = ConfigurationManager.AppSettings["apiUrl"];
                var relativeUrl = ConfigurationManager.AppSettings["relativeUrl"];


                var client = new RestClient(hostUrl);
                var request = new RestRequest(relativeUrl, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new CreateDeviceRequestModel()
                {
                    RUDI = deviceId
                });
                Console.WriteLine("Sending to Rest Api :");
                var result = client.Execute(request);

                Console.WriteLine("Response : \n" + result.StatusCode + "\t" + result.Content);
            }
            catch (Exception exp)
            {
                var ex = JsonConvert.SerializeObject(exp);
                Console.WriteLine(ex);
            }
        }
    }
}
