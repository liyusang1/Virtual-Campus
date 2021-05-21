using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;

public class bulletinBoard : MonoBehaviour
{
    string Token;
    public GameObject boardActiveButton;
    public GameObject Auth;
    public Transform articlePosition;
    public GameObject articlePrefab;

    public InputField title;
    public InputField content;

    public GameObject writeArticle;
    public Text articlePostErrorText;

    public GameObject articleBtnPrefab;
    public Transform panelPos;

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

    /*public void Post()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();


        Debug.Log(jwt);

        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);

        
        request.AddHeader("Content-Type", "application/json");

      
        request.AddJsonBody(
            new
            {
                contentName = "abcd",
                content = "efgh",
            });

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //그걸 출력해 보자.
        Debug.Log(response.Content);
    } */

    public void showArticle()
    {
        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);
        int countContent = (int)jObject["countContent"];

        int contentId = (int)jObject["result"][0]["contentId"];
        string contentName = jObject["result"][0]["contentName"].ToString();
        string userNickName = jObject["result"][0]["userNickname"].ToString();
        int replyCount = (int)jObject["result"][0]["replyCount"];
        string createdAt = jObject["result"][0]["createdAt"].ToString();
    }

    public void postArticle()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("x-access-token", jwt);
        request.AddHeader("Content-Type", "application/json");

        string getContentName;
        string getContent;

       
        getContentName = title.text;
        getContent = content.text;

        if(title.text.Length == 0 || content.text.Length == 0)
        {
            Debug.Log("내용을 입력하지 않음");
            articlePostErrorText.text = "내용을 입력하시 않음";
            return;
        }

        if (title.text.Length > 40)
        {
            Debug.Log("최대글자");
            articlePostErrorText.text = "제목 글자수 40자 초과";
            return;
        }

        if (content.text.Length > 990)
        {
            Debug.Log("최대글자");
            articlePostErrorText.text = "본문 글자수 1000자 초과";
            return;
        }

        request.AddJsonBody(
            new
            {
                contentName = getContentName,
                content = getContent,
            });

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int messageCode = (int)jObject["code"];

        if (messageCode == 1000)
        {
            string message = jObject["message"].ToString();
            
        }
        else
        {
            string message = jObject["message"].ToString();
        }


    }

    public void CreateBtns()
    {
        var client = new RestClient("https://liyusang1.site/community");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);
        int countContent = (int)jObject["countContent"];

        for (int i = 0; i < countContent; i++)
        {
            GameObject button = Instantiate(articleBtnPrefab);
            button.transform.parent = panelPos.transform;

            RectTransform btnpos = button.GetComponent<RectTransform>();
            
            button.transform.position = gameObject.transform.position;
            btnpos.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, (-200 * i), 100);
        }
    }
}
