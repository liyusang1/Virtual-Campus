using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.UI;

public class showArticle : MonoBehaviour
{
    public Article article = new Article();

    public Text userNickname;
    public Text articleName;
    public Text replyCount;
    public Text createdAt;
    public Text content;
    public Text replies;
    public InputField myReply;

    // Start is called before the first frame update
    void Start()
    {
        int communityNumber = article.contentID;

        var client = new RestClient("https://liyusang1.site/community/" + communityNumber);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        articleName.text = jObject["result"]["content"][0]["contentName"].ToString();
        content.text = jObject["result"]["content"][0]["content"].ToString();
        userNickname.text = jObject["result"]["content"][0]["userNickname"].ToString();
        replyCount.text = jObject["result"]["content"][0]["replyCount"].ToString();
        createdAt.text = jObject["result"]["content"][0]["createdAt"].ToString();

        int replyCounts = (int)jObject["result"]["content"][0]["replyCount"]; //int형으로 총 댓글 수를 받아오기 위함

        for (int i = 0; i < replyCounts; i++)
        {
            //int replyId = (int)jObject["result"]["reply"][i]["replyId"];
            string replyUserNickname = jObject["result"]["reply"][i]["userNickname"].ToString();
            string replyContent = jObject["result"]["reply"][i]["content"].ToString();
            string replyCreatedAt = jObject["result"]["reply"][i]["createdAt"].ToString();

            replies.text = replies.text + replyUserNickname + " " +  replyContent + " " +  replyCreatedAt + "\n";
        }
    }

    public void UpdateReply()
    {
        int communityNumber = article.contentID;

        var client = new RestClient("https://liyusang1.site/community/" + communityNumber);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        articleName.text = jObject["result"]["content"][0]["contentName"].ToString();
        content.text = jObject["result"]["content"][0]["content"].ToString();
        userNickname.text = jObject["result"]["content"][0]["userNickname"].ToString();
        replyCount.text = jObject["result"]["content"][0]["replyCount"].ToString();
        createdAt.text = jObject["result"]["content"][0]["createdAt"].ToString();

        int replyCounts = (int)jObject["result"]["content"][0]["replyCount"]; //int형으로 총 댓글 수를 받아오기 위함

        for (int i = 0; i < replyCounts; i++)
        {
            //int replyId = (int)jObject["result"]["reply"][i]["replyId"];
            string replyUserNickname = jObject["result"]["reply"][i]["userNickname"].ToString();
            string replyContent = jObject["result"]["reply"][i]["content"].ToString();
            string replyCreatedAt = jObject["result"]["reply"][i]["createdAt"].ToString();

            replies.text = replies.text + replyUserNickname + " " + replyContent + " " + replyCreatedAt + "\n";
        }
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

    public void WriteReply()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        //reply ID
        int contentId = article.contentID;

        var client = new RestClient("https://liyusang1.site/reply/community/" + contentId);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(
         new
         {
             replyContent = myReply.text
         }); ;

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        int messageCode = (int)jObject["code"];

        if (messageCode == 1000)
        {
            string message = jObject["message"].ToString();
            Debug.Log(message);
            replies.text = null;
            UpdateReply();
        }
        else
        {
            string message = jObject["message"].ToString();
            Debug.Log(message);
        }
    }
}
