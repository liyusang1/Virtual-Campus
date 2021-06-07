using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.UI;

public class itemManager : MonoBehaviour
{
    public InventoryManager IM;
    Image im;

    //public string itemCode;
    //public string itemPrice;
    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useItem()
    {
        Debug.Log(this.name);
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/user-inventory");
        client.Timeout = -1;
        var request = new RestRequest(Method.DELETE);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
        new
        {
            itemCode = im.sprite.name
        }) ;

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            IM.showInventory();
        }

        //예외
        else
        {

        }
    }
}
