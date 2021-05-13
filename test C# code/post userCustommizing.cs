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

        var client = new RestClient("https://liyusang1.site/user-customizing");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "token");
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                custom1 = "custom1",
                custom2 = "custom1",
                custom3 = "custom1",
                custom4 = "custom1",
                custom5 = "custom1",
            });

        IRestResponse response = client.Execute(request);
    
        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        
        if (code == 1000)
        {
            string message = jObject["message"].ToString();
            Console.WriteLine(message);
        }

        //이미 생성되어 있는 경우 패스
        else if (code == 1001)
        {
            string message = jObject["message"].ToString();
            Console.WriteLine(message);
        }

    }
}
