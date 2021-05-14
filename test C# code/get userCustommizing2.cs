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
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", "token");
        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];



        if (code == 1000)
        {
            float hairR = (float)jObject["result"][0]["hairR"];
            float hairG = (float)jObject["result"][0]["hairG"];
            float hairB = (float)jObject["result"][0]["hairB"];
            float eyeR = (float)jObject["result"][0]["eyeR"];
            float eyeG = (float)jObject["result"][0]["eyeG"];
            float eyeB = (float)jObject["result"][0]["eyeB"];
            float topR = (float)jObject["result"][0]["topR"];
            float topG = (float)jObject["result"][0]["topG"];
            float topB = (float)jObject["result"][0]["topB"];
            float bottomR = (float)jObject["result"][0]["bottomR"];
            float bottomG = (float)jObject["result"][0]["bottomG"];
            float bottomB = (float)jObject["result"][0]["bottomB"];
            float shoeR = (float)jObject["result"][0]["shoeR"];
            float shoeG = (float)jObject["result"][0]["shoeG"];
            float shoeB = (float)jObject["result"][0]["shoeB"];


            Console.WriteLine(hairR);
            Console.WriteLine(hairG);
            Console.WriteLine(hairB);
            Console.WriteLine(eyeR);
            Console.WriteLine(eyeG);
            Console.WriteLine(eyeB);

        }
    }
}
