using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.UI;

public class money : MonoBehaviour
{
    public uint userMoney;
    public Text moneyText;
    public Text nickName;

    // Start is called before the first frame update
    void Start()
    {
        getMoney();
    }

    // Update is called once per frame
    public void Update()
    {
        //moneyText.text = userMoney.ToString();
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    public void getMoney()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/users");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        //이자리에 토큰을 넣어야함
        request.AddHeader("x-access-token", jwt);

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
            Debug.Log("유저 금액 가져오기");
            //moneyText.text = userMoney.ToString();
            this.userMoney = userMoney;
            this.nickName.text = userNickname;
            moneyText.text = userMoney.ToString();
        }

        //DB에서 값을 가져오지 못하는 경우
        else
        {
            Debug.Log("정상적으로 값을 가져오지 못했습니다.");   
        }
    }

    public void addUserMoney(uint money)
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/add-users-money");
        client.Timeout = -1;
        var request = new RestRequest(Method.PATCH);
        request.AddHeader("x-access-token", jwt);
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
            this.userMoney = userMoney;
            //Console.WriteLine(userMoney);

            moneyText.text = userMoney.ToString();
        }
        else
        {
            string errorMessage = jObject["message"].ToString();
            //Console.WriteLine(errorMessage);
        }
    }
}
