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
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", "");
        request.AddHeader("Content-Type", "application/json");

        string getContentName;
        string getContent;

        Console.Write("글 제목을 입력하세요 : ");
        getContentName = Console.ReadLine();

        Console.Write("글을 입력하세요 : ");
        getContent = Console.ReadLine();

        request.AddJsonBody(
            new
            {
                contentName = getContentName,
                content = getContent,
            });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //그걸 출력해 보자.
        Console.WriteLine(response.Content);
    }
}
