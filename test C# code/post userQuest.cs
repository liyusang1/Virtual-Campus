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

        var client = new RestClient("https://liyusang1.site/user-quest1/1");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "token");
        IRestResponse response = client.Execute(request);
        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];
        string message = jObject["message"].ToString();

        //퀘스트 받기 완료
        if (code == 1001)
        {
            Console.WriteLine(message);

        }

        //퀘스트 미완료 요구 조건 미 충족
        else if (code == 1002)
        {
            Console.WriteLine(message);

        }

        //퀘스트 완료
        else if (code == 1003)
        {
            Console.WriteLine(message);

        }

        //퀘스트 이미 완료함
        else if (code == 1004)
        {
            Console.WriteLine(message);

        }

    }
}
