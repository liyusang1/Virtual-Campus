using System;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
class Test
{
    static void Main(string[] args)
    {
        var client = new RestClient("https://liyusang1.site/user-quest1/1");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", "token");
        IRestResponse response = client.Execute(request);
        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];
        string message = jObject["message"].ToString();

        //퀘스트를 받지 않은 상태
        if (code == 1000)
        {
            Console.WriteLine(message);

        }

        //퀘스트를 진행중인 상태
        else if (code == 1001)
        {
            Console.WriteLine(message);

        }

        //퀘스트를 완료한 상태
        else if (code == 1002)
        {
            Console.WriteLine(message);

        }
    } 
}

 
