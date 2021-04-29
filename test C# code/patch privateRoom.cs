using System;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
class Test
{
    static void Main(string[] args)
    {

        var client = new RestClient("https://liyusang1.site/private-room");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "token");
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(
         new
         {
             funiture1 = "00ds0a"
         });

        IRestResponse response = client.Execute(request);
        var jObject = JObject.Parse(response.Content);
        int code = (int)jObject["code"];

        if (code == 1000)
        {
            string message = jObject["message"].ToString();
      
            Console.WriteLine("message : {0}", message);
        }

        //예외
        else
        {

        }
    }
}
