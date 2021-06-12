using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class SignUp : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public InputField pwCheck;
    public Button joinButton;
    public InputField nickname;
    public Text signUpComfirmText;
    public GameObject SignUpCanvas;

    //public Canvas signUpCanvas;

    // Update is called once per frame
     public void Join()
     {        
        var client = new RestClient("https://liyusang1.site/sign-up");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                email = emailField.text,
                password = passwordField.text,
                passwordCheck = pwCheck.text,
                userNickname = nickname.text
            });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //code 를 resultCode에 저장
        int resultCode = (int)jObject["code"];

        if (resultCode == 1000)
        {
            Debug.Log("회원가입성공");
            signUpComfirmText.text = "회원가입 성공!";

            SignUpCanvas.SetActive(false);
        }

        else
        {
            string errorMessage = jObject["message"].ToString();
            Debug.Log("실패 : " + errorMessage);
            signUpComfirmText.text = errorMessage;

        }
    }
}
