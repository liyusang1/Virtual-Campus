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

        var client = new RestClient("https://liyusang1.site/user-inventory");
        client.Timeout = -1;
        var request = new RestRequest(Method.DELETE);
        request.AddHeader("x-access-token", "token");
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
      new
      {
          itemCode = "itemcode"
      });

        IRestResponse response = client.Execute(request);
 
        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            
        }

        //예외
        else
        {
            
        }
    }
}
