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
        request.AddHeader("x-access-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEsInVzZXJOaWNrbmFtZSI6Iu2FjOyKpO2KuCIsImlhdCI6MTYxNjg2NjQ3NiwiZXhwIjoxNjQ4NDAyNDc2LCJzdWIiOiJ1c2VySW5mbyJ9.6U7rtn4F2Do_eBqJILH_Axr_CY9Q7_V2h-UxVMllQw8");
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

 