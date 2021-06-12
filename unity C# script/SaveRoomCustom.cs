using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.SceneManagement;

public class SaveRoomCustom : MonoBehaviour
{
    //public Sprite[] bedSprites;
    //public Sprite[] tableSprites;
    //public Sprite[] doorSprites;
    //public Sprite[] sofaSprites;
    //public Sprite[] drawerSprites;

    public SpriteRenderer bedSprite;
    public SpriteRenderer doorSprite;
    public SpriteRenderer tableSprite;
    public SpriteRenderer drawerSprite;
    public SpriteRenderer sofaSprite;

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    // Start is called before the first frame update
    void Start()
    {       
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        

        var client = new RestClient("https://liyusang1.site/private-room");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", jwt);

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

            //Debug.Log (funiture1);

            bedSprite.sprite = Resources.Load<Sprite>("furnitures/bed/" + funiture1);
            drawerSprite.sprite = Resources.Load<Sprite>("furnitures/furniture1/" + funiture2);
            sofaSprite.sprite = Resources.Load<Sprite>("furnitures/furniture2/" + funiture3);
            doorSprite.sprite = Resources.Load<Sprite>("furnitures/furniture3_door/" + funiture4);
            tableSprite.sprite = Resources.Load<Sprite>("furnitures/furniture4/" + funiture5);

            //Debug.Log(bedSprite.sprite.name);
        }

        //예외
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveRoom()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        Debug.Log("saved");


        var client = new RestClient("https://liyusang1.site/private-room");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(
         new
         {
             funiture1 = bedSprite.sprite.name.ToString(),
             funiture2 = drawerSprite.sprite.name.ToString(),
             funiture3 = sofaSprite.sprite.name.ToString(),
             funiture4 = doorSprite.sprite.name.ToString(),
             funiture5 = tableSprite.sprite.name.ToString()
         });

        IRestResponse response = client.Execute(request);
        var jObject = JObject.Parse(response.Content);
        int code = (int)jObject["code"];

        if (code == 1000)
        {
            string message = jObject["message"].ToString();

            Debug.Log("message : " + message);
        }

        //예외
        else
        {
            string message = jObject["message"].ToString();

            Debug.Log("message : " + message);
        }
    }

    public void ChangeRoom()
    {
        Debug.Log(bedSprite.sprite.name);
    }

    public void GoToField()
    {
        SceneManager.LoadScene("MainFieldScene");
    }
}
