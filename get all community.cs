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
        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //jObject.Count 는 서버로부터 넘어오는 list의 개수
        for (int i = 0; i < jObject.Count; i++)
        {
            int contentId = (int)jObject["result"][i]["contentId"];
            string contentName = jObject["result"][i]["contentName"].ToString();
            string userNickName = jObject["result"][i]["userNickname"].ToString();
            int replyCount = (int)jObject["result"][i]["replyCount"];
            string createdAt = jObject["result"][i]["createdAt"].ToString();

            Console.Write("글 작성자 :{0} , 글 제목 :{1} ,댓글 수 : {2}, 생성시기 : {3}", userNickName, contentName,replyCount,createdAt);
            Console.WriteLine();
        }
        
       
    }
}

