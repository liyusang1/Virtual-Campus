using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;

public class PlayerControllerScript : MonoBehaviourPun
{
    public JoyStick joystick;
    public float MoveSpeed;
    public PhotonView PV;

    public SpriteRenderer hair;
    public SpriteRenderer eye1;
    public SpriteRenderer eye2;
    public SpriteRenderer top;
    public SpriteRenderer bottom1;
    public SpriteRenderer bottom2;
    public SpriteRenderer shoe1;
    public SpriteRenderer shoe2;

    public Vector3 _moveVector;
    private Transform _transform;
    SpriteRenderer spriteRenderer;
    //public GameObject talkButton;   
    public Animator animator;

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _moveVector = Vector3.zero;
        //talkButton.SetActive(false);

        if(!PV.IsMine)
        {
            return;
        }
        else
        {
            loadCharactorCustomizing();
            //PV.RPC("loadCharactorCustomizing", RpcTarget.AllBuffered);
        }

        
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<JoyStick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {         
            PV.RPC("FlipRPC", RpcTarget.AllBuffered, PoolInput());           
        }
        else
        {
            return;
        }
        
        HandleInput();        
    }

     void FixedUpdate()
    {
        Move();
        //this.animator.SetBool("isWalking", false);
    }

    [PunRPC]
    public void HandleInput()
    {
        _moveVector = PoolInput();
    }

    [PunRPC]
    void FlipRPC(Vector3 mv)
    {
        this.animator.SetBool("isWalking", false);

        if (mv.x < 0)
        {
           // Debug.Log("뒤집기1");
            //spriteRenderer.flipX = true; 
            transform.localScale = new Vector3(-0.08f, 0.08f, 1f);
            animator.SetBool("isWalking", true);
        }

        if (mv.x > 0)
        {
            //Debug.Log("뒤집기2");
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.08f, 0.08f, 1f);
            animator.SetBool("isWalking", true);
        }
    }

    [PunRPC]
    public Vector3 PoolInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        
        Vector3 moveDIr = new Vector3(h, v, 0).normalized;

        if(moveDIr.x < 0)
        {
            //Debug.Log("뒤집기1");
            //spriteRenderer.flipX = true; 
            transform.localScale = new Vector3(-0.08f,0.08f,1f);
        }

        if (moveDIr.x > 0)
        {
            //Debug.Log("뒤집기2");
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.08f,0.08f,1f);
        }
        return moveDIr;
    }

    public void Move()
    {
        _transform.Translate(_moveVector * MoveSpeed * Time.deltaTime);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("벽 충돌");
        }
    }

    [PunRPC]
    public void loadCharactorCustomizing()
    {
        JsonData jsdata = Load();
        string jwt = jsdata["Token"].ToString();

        var client = new RestClient("https://liyusang1.site/user-customizing");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("x-access-token", jwt);
        IRestResponse response = client.Execute(request);

        var jObject = JObject.Parse(response.Content);

        int code = (int)jObject["code"];

        if (code == 1000)
        {
            float hairR = (float)jObject["result"][0]["hairR"];
            float hairG = (float)jObject["result"][0]["hairG"];
            float hairB = (float)jObject["result"][0]["hairB"];
            float eyeR = (float)jObject["result"][0]["eyeR"];
            float eyeG = (float)jObject["result"][0]["eyeG"];
            float eyeB = (float)jObject["result"][0]["eyeB"];
            float topR = (float)jObject["result"][0]["topR"];
            float topG = (float)jObject["result"][0]["topG"];
            float topB = (float)jObject["result"][0]["topB"];
            float bottomR = (float)jObject["result"][0]["bottomR"];
            float bottomG = (float)jObject["result"][0]["bottomG"];
            float bottomB = (float)jObject["result"][0]["bottomB"];
            float shoeR = (float)jObject["result"][0]["shoeR"];
            float shoeG = (float)jObject["result"][0]["shoeG"];
            float shoeB = (float)jObject["result"][0]["shoeB"];

            hair.color = new Color(hairR, hairG, hairB);
            eye1.color = new Color(eyeR, eyeG, eyeB);
            eye2.color = new Color(eyeR, eyeG, eyeB);
            top.color = new Color(topR, topG, topB);
            bottom1.color = new Color(bottomR, bottomG, bottomB);
            bottom2.color = new Color(bottomR, bottomG, bottomB);
            shoe1.color = new Color(shoeR, shoeG, shoeB);
            shoe2.color = new Color(shoeR, shoeG, shoeB);
        }
        else
        {
            return;
        }
    }
}

    
