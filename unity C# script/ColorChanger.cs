using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.SceneManagement;

public class ColorChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer[] bodyPart;
    public float colorR;
    public float colorG;
    public float colorB;

    public SpriteRenderer hair;
    public SpriteRenderer eye;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    public SpriteRenderer shoes;
    //public float colorA;

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    void Start()
    {
        //bodyPart.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorChange()
    {
        for(int i = 0; i < bodyPart.Length; i++)
        {
            bodyPart[i].color = new Color(colorR / 255f,colorG / 255f ,colorB / 255f);
        }
    }

    public void GoToField()
    {
        SceneManager.LoadScene("MainFieldScene");
    }

    public void saveCustom()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/user-customizing");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                hairR = hair.color.r,
                hairG = hair.color.g,
                hairB = hair.color.b,
                eyeR = eye.color.r,
                eyeG = eye.color.g,
                eyeB = eye.color.b,
                topR = top.color.r,
                topG = top.color.g,
                topB = top.color.b,
                bottomR = bottom.color.r,
                bottomG = bottom.color.g,
                bottomB = bottom.color.b,
                shoeR = shoes.color.r,
                shoeG = shoes.color.g,
                shoeB = shoes.color.b
            });;

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            string message = jObject["message"].ToString();
           
        }

        //이미 생성되어 있는 경우 패스
        else if (code == 1001)
        {
            string message = jObject["message"].ToString();           
        }
    }
}
