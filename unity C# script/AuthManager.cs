using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;

public class Data
{
    public string Token;
    public Data(string Token)
    {
        this.Token = Token;
    }  
}

public class AuthManager : MonoBehaviour
{
   
    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;
    public Canvas connectingCanvas;
    public Text errorText;
    public string Token = null;
   
    public GameObject ConnectingManager;
    public GameObject Auth;

    private void Start()
    {
        Screen.SetResolution(2560, 1440, true);
    }

    void Save(Data data)
    {
        JsonData jsonData = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\data.json", jsonData.ToString());
    }

    void SignIn()
    {
        var client = new RestClient("https://liyusang1.site/sign-in");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                email = emailField.text,
                password = passwordField.text,
            });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        int resultCode = (int)jObject["code"];
        string errorMessage = jObject["message"].ToString();

        if(resultCode == 1000)
        {
            Token = jObject["jwt"].ToString();
            Debug.Log(response.Content);
            Debug.Log(Token);

            Data data = new Data(Token);
            Save(data);
            /////
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Debug.Log(errorMessage);
            errorText.text = errorMessage;
        }
        //string code = jObject["code"].ToString();

        //그걸 출력해 보자.
    }    
}

