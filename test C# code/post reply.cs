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
        //reply ID
        int contentId = 2;

        var client = new RestClient("https://liyusang1.site/reply/community/"+contentId);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "JWT");
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(
         new
         {
             replyContent="댓글"
         });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        int messageCode = (int)jObject["code"];

        if(messageCode == 1000)
        {
            string message = jObject["message"].ToString();
            Console.WriteLine(message);
        }
        else
        {
            string message = jObject["message"].ToString();
            Console.WriteLine(message);
        }
    }
}