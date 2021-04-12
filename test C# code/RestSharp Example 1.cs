using System;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

//https://www.toolsqa.com/rest/deserialize-json-response-2/ 참고 자료

class Test
{
    static void Main(string[] args)
    {
        var client = new RestClient("url");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //받아온 데이터에서 result값을 추출하는 예시
        string city = jObject.GetValue("result").ToString();

        //받아온 데이터에서 result - 첫번째 location을 추출하는 예시
        string etc = jObject["result"][0]["location"].ToString();

        //그걸 출력해 보자.
        Console.WriteLine(etc);

    }
}

