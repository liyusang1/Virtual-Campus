using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] itemImages;  
    public Text[] itemCounts;
    //public bool[] isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    public void showInventory()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/user-inventory");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", jwt);
        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);
        int code = (int)jObject["code"];
        int count = (int)jObject["itemCount"];

        if (code == 1000)
        {
            string itemCode;
            int itemCount;

            for (int i = 0; i < 16; i++)
            {               
                itemImages[i].sprite = Resources.Load<Sprite>("Shop/test/item_0");
                itemCounts[i].text = "";
            }

            for (int i = 0; i < count; i++)
            {
                itemCode = jObject["result"][i]["itemCode"].ToString();
                itemCount = (int)jObject["result"][i]["itemCount"];
                
                itemImages[i].sprite = Resources.Load<Sprite>("Shop/test/" + itemCode);                
                itemCounts[i].text = itemCount.ToString();               
            }            
        }

        else
        {

        }
    }

    public void closeInventory()
    {
        for(int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].sprite = Resources.Load<Sprite>("Shop/test/item_0");
        }
    }

    /*public void useItem()
    {
        //Debug.Log();
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
            //itemCode =
        }) ;

       IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            showInventory();
        }

        //예외
        else
        {

        }
    } */
        
    
}
