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

        var client = new RestClient("https://liyusang1.site/user-customizing");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjEsInVzZXJOaWNrbmFtZSI6Iu2FjOyKpO2KuCIsImlhdCI6MTYxNjg2NjQ3NiwiZXhwIjoxNjQ4NDAyNDc2LCJzdWIiOiJ1c2VySW5mbyJ9.6U7rtn4F2Do_eBqJILH_Axr_CY9Q7_V2h-UxVMllQw8");
        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            string custom1 = jObject["result"][0]["custom1"].ToString();
            string custom2 = jObject["result"][0]["custom2"].ToString();
            string custom3 = jObject["result"][0]["custom3"].ToString();
            string custom4 = jObject["result"][0]["custom4"].ToString();
            string custom5 = jObject["result"][0]["custom5"].ToString();

            Console.WriteLine(custom1);
            Console.WriteLine(custom2);
            Console.WriteLine(custom3);
            Console.WriteLine(custom4);
            Console.WriteLine(custom5);
        }

    }
}
