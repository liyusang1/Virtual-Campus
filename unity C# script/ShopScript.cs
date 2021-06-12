using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public InventoryManager IM;
    public GameObject shopButton;
    public int userMoney;
    public Text userMoneyText;
    Button itemButton;
    public money playerMoney;
    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }
    // Start is called before the first frame update
    void Start()
    {
        itemButton = GetComponent<Button>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("item"))
                {                  
                    getItem(hit.collider.gameObject.name);
                    Debug.Log("아이템 획득");
                    IM.showInventory();
                }
            }
        }
    }

    public void getItem(string itemName)
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/user-inventory");
        client.Timeout = -1;
        var request = new RestRequest(Method.PATCH);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(
         new
         {
             itemCode = itemName
         }) ;


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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shopButton.SetActive(true);
            Debug.Log("상점");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shopButton.SetActive(false);
            Debug.Log("상점");
        }
    }

    public void buyItem(int price)
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/purchase-merchandise");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(
            new
            {
                merchandisePrice = price,
                itemCode = itemButton.image.sprite.name,
            });

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];
        string message = jObject["message"].ToString();

        if (code == 1000)
        {
            int _userMoney = (int)jObject["userMoney"];

            Debug.Log(message);
            userMoneyText.text = _userMoney.ToString();
            Debug.Log(_userMoney);
            //구입 완료
        }

        //예외 돈이 부족한 경우
        else if(code == 3000)
        {
            
            Debug.Log("돈이 부족합니다");
        }
    }
}
