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

        var client = new RestClient("https://liyusang1.site/purchase-merchandise");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "token");
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                merchandisePrice = 30,
                itemCode = "getContent",
            });

        IRestResponse response = client.Execute(request);
 
        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];
        string message = jObject["message"].ToString();

        if (code == 1000)
        {
            Console.WriteLine(message);
            //구입 완료
        }

        //예외 돈이 부족한 경우
        else
        {
            Console.WriteLine("돈이 부족합니다");
        }
    }
}
