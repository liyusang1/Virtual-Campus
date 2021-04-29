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

        var client = new RestClient("https://liyusang1.site/private-room");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", "token");

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            string funiture1 = jObject["roomInfo"][0]["funiture1"].ToString();
            string funiture2 = jObject["roomInfo"][0]["funiture2"].ToString();
            string funiture3 = jObject["roomInfo"][0]["funiture3"].ToString();
            string funiture4 = jObject["roomInfo"][0]["funiture4"].ToString();
            string funiture5 = jObject["roomInfo"][0]["funiture5"].ToString();

            Console.WriteLine("funiture1 : {0}", funiture1);
            Console.WriteLine("funiture2 : {0}", funiture2);
            Console.WriteLine("funiture3 : {0}", funiture3);
            Console.WriteLine("funiture4 : {0}", funiture4);
            Console.WriteLine("funiture5 : {0}", funiture5);
        }

        //예외
        else
        {

        }
    }
}
