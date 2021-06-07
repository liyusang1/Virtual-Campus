using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
       // PhotonNetwork.ConnectUsingSettings();
    }

    
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public GameObject bulletinBoard;
    public GameObject quitBulletinBoardButton;
    public Transform[] trashPositions;
    //public Transform trashPosition;
    public GameObject trash;
    float ItemSpawnTime = 15f;
    float lastSpawnTime;

    public AudioSource trashPick;

    public money playerMoney = new money();
    public Text[] ChatText;
    public InputField ChatInput;
    public PhotonView PV;

    string nickName;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(2560, 1440, true);
        SpawnPlayer();

        if(PhotonNetwork.IsMasterClient)
        {

        }

        lastSpawnTime = 0;

        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/users");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);

        //이자리에 토큰을 넣어야함
        request.AddHeader("x-access-token", jwt);

        IRestResponse response = client.Execute(request);

        //받아온 데이터를 json형태로 묶음
        var jObject = JObject.Parse(response.Content);

        //DB로부터 실패인지 성공인지 메시지코드를 받아서 messageCode에 저장함
        int messageCode = (int)jObject["code"];

        //DB에서 성공적으로 값을 가져온 경우
        if (messageCode == 1000)
        {
            nickName = jObject["result"][0]["userNickname"].ToString();            
        }

        //DB에서 값을 가져오지 못하는 경우
        else
        {
            Debug.Log("정상적으로 값을 가져오지 못했습니다.");
        }
    }

    void Update()
    {
        if(Time.time >= lastSpawnTime + ItemSpawnTime)
        {
            lastSpawnTime = Time.time;
            SpawnTrash();
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if(hit.collider != null)
            {
                if(hit.collider.gameObject.tag.Equals("trash"))
                {
                    Debug.Log(hit.transform.gameObject);
                    trashPick.Play();
                    playerMoney.addUserMoney(50);
                    Destroy(hit.transform.gameObject);
                }

                if (hit.collider.gameObject.name.Equals("ShopNPC"))
                {
                    Debug.Log(hit.transform.gameObject);
                    
                }

                if (hit.collider.gameObject.tag.Equals("bulletinBoard"))
                {
                    Debug.Log(hit.transform.gameObject);
                    bulletinBoard.SetActive(true);
                    quitBulletinBoardButton.SetActive(true);
                }
               
            }
        }
    }

    private void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }

    void SpawnTrash()
    {
        {
            int pos = Random.Range(0, trashPositions.Length);

            Instantiate(trash, trashPositions[pos].transform);
            Debug.Log("쓰레기 생성");   
        }    
    }

    public override void OnLeftRoom() // 로그아웃 시 사용
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("SignIn");
    }


    // Update is called once per frame

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    public void Send()
    {           
          string msg = ChatInput.text;
          PV.RPC("ChatRPC", RpcTarget.All, nickName + " : " + msg);
          ChatInput.text = "";           
            
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }

        if (!isInput)
        {
            for (int i = 1; i < ChatText.Length; i++)
            {
                ChatText[i - 1].text = ChatText[i].text;
            }
            ChatText[ChatText.Length - 1].text = msg;
        }
    }



}
