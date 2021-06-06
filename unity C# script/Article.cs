using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;

public class Article : MonoBehaviour
{
    public int contentID;
    public Text userNickname;
    public Text articleName;
    public Text replyCount;
    public Text createdAt;
    public Text content;

    // Start is called before the first frame update
    void Start()
    {
        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);
        int countContent = (int)jObject["countContent"];

        contentID = (int)jObject["result"][0]["contentId"];
        articleName.text = jObject["result"][0]["contentName"].ToString();
        userNickname.text = jObject["result"][0]["userNickname"].ToString();
        replyCount.text = jObject["result"][0]["replyCount"].ToString();
        createdAt.text = jObject["result"][0]["createdAt"].ToString();
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
}
