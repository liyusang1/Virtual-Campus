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
        Console.Write("enter Email :  ");
        string userEmail;
        userEmail = Console.ReadLine();

        Console.Write("enter Password :  ");
        string userPassword;
        userPassword = Console.ReadLine();

        var client = new RestClient("");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                email = userEmail,
                password = userPassword,
            });

        IRestResponse response = client.Execute(request);
    
        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);


        //code 를 resultCode에 저장
        int resultCode = (int)jObject["code"];

        if (resultCode == 1000)
        {
            string jwtToken = jObject["jwt"].ToString();
            Console.WriteLine(jwtToken);
        }

        else
            Console.WriteLine("실패 : {0}", response.Content);
            
    }
}

