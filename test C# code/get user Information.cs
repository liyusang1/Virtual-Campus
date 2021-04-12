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
        var client = new RestClient("https://liyusang1.site/users");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        
        //이자리에 토큰을 넣어야함
        request.AddHeader("x-access-token", "토큰넣는곳");

        IRestResponse response = client.Execute(request);
    
        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //DB로부터 실패인지 성공인지 메시지코드를 받아서 messageCode에 저장함
        int messageCode = (int)jObject["code"];

        //DB에서 성공적으로 값을 가져온 경우
        if (messageCode == 1000)
        {
            //userMoney라는 변수에 db로부터 받아온 사용자의 돈이 저장됨 , (돈은 음수값이 없기 때문에 자료형은 unsigned int)
            uint userMoney = (uint)jObject["result"][0]["userMoney"];
            string userNickname = jObject["result"][0]["userNickname"].ToString();

            //사용자의 현재 돈 출력
            Console.WriteLine(userMoney);

            //사용자 이름 출력
            Console.WriteLine(userNickname);
        }

        //DB에서 값을 가져오지 못하는 경우
        else
        {
            Console.WriteLine("정상적으로 값을 가져오지 못했습니다.");
            Console.WriteLine(jObject);
        }
    }
}
