using System;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

class Test
{
    static void Main(string[] args)
    {
        int communityNumber = 1;

        var client = new RestClient("https://liyusang1.site/community/"+communityNumber);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        string contentName = jObject["result"]["content"][0]["contentName"].ToString();
        string content = jObject["result"]["content"][0]["content"].ToString();
        string userNickName = jObject["result"]["content"][0]["userNickname"].ToString();
        int replyCount = (int)jObject["result"]["content"][0]["replyCount"];
        string createdAt = jObject["result"]["content"][0]["createdAt"].ToString();

        Console.WriteLine(contentName);
        Console.WriteLine(content);
        Console.WriteLine(userNickName);
        Console.WriteLine(replyCount);
        Console.WriteLine(createdAt);
        Console.WriteLine("\n");

        int i = 0;

        while (jObject["result"]["reply"].First != null)
        {
            int replyId = (int)jObject["result"]["reply"][i]["replyId"];
            string replyUserNickname = jObject["result"]["reply"][i]["userNickname"].ToString();
            string replyContent = jObject["result"]["reply"][i]["content"].ToString();
            string replyCreatedAt = jObject["result"]["reply"][i]["createdAt"].ToString();

            Console.WriteLine(replyId);
            Console.WriteLine(replyUserNickname);
            Console.WriteLine(replyContent);
            Console.WriteLine(replyCreatedAt);
            Console.WriteLine("\n");

            i++;

            if (jObject["result"]["reply"][i].Next == null)
            {
               replyId = (int)jObject["result"]["reply"][i]["replyId"];
               replyUserNickname = jObject["result"]["reply"][i]["userNickname"].ToString();
               replyContent = jObject["result"]["reply"][i]["content"].ToString();
               replyCreatedAt = jObject["result"]["reply"][i]["createdAt"].ToString();

                Console.WriteLine(replyId);
                Console.WriteLine(replyUserNickname);
                Console.WriteLine(replyContent);
                Console.WriteLine(replyCreatedAt);
                break;
            }
        }
    }
}
