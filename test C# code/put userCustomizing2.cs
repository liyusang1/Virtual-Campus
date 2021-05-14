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
                hairR = "1",
                hairG = "1",
                hairB = "1",
                eyeR = "1",
                eyeG ="1",
                eyeB = "1",
                topR ="1",
                topG ="1",
                topB ="1",
                bottomR ="1",
                bottomG = "1",
                bottomB = "1",
                shoeR ="1",
                shoeG ="1",
                shoeB = "2"
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
