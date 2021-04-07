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
        addUserMoney(1000);
    }

    static void addUserMoney(uint money)
    {
        var client = new RestClient("https://liyusang1.site/add-users-money");
        client.Timeout = -1;
        var request = new RestRequest(Method.PATCH);
        request.AddHeader("x-access-token", "token");
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                addMoney = money,
            });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //code 를 resultCode에 저장
        int resultCode = (int)jObject["code"];

        if (resultCode == 1000)
        {
            uint userMoney = (uint)jObject["result"];
            Console.WriteLine(userMoney);
        }
        else
        {
            string errorMessage = jObject["message"].ToString();
            Console.WriteLine(errorMessage);
        }
    }
}
