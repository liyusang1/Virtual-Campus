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

        var client = new RestClient("https://liyusang1.site/user-inventory");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", "token");
        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);
        int code = (int)jObject["code"];
        int count = (int)jObject["itemCount"];

        if (code == 1000)
        {
            string itemCode;
            int itemCount;
            for (int i = 0; i < count; i++)
            {
                itemCode = jObject["result"][i]["itemCode"].ToString();
                itemCount = (int)jObject["result"][i]["itemCount"];
                Console.WriteLine("itemCode : {0}", itemCode);
                Console.WriteLine("itemCount : {0}", itemCount);

            }
        }

        //예외
        else
        {

        }
    }
}
