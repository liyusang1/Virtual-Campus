using System;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//https://www.toolsqa.com/rest/deserialize-json-response-2/ 참고 자료

class Test
{
    static void Main(string[] args)
    {
        Console.Write("입력해 이메일");
        string emails;
        emails = Console.ReadLine();


        var client = new RestClient("url");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                email = emails,
                password = "test123",
                passwordCheck = "test123",
                userNickname = "동동주"
            });

        IRestResponse response = client.Execute(request);
    
        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //그걸 출력해 보자.
        Console.WriteLine(response.Content);

    }
}

