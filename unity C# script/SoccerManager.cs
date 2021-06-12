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

public class SoccerManager : MonoBehaviour
{
    public Transform[] spawnPositions;
    public Transform ballPosition;
    public GameObject playerPrefab;
    public GameObject ball;
    
    public int AteamScore = 0;
    public int BteamScore = 0;

    public Text AteamScoreText;
    public Text BteamScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(2560, 1440, true);
        SpawnPlayer();

        if (PhotonNetwork.IsMasterClient)
        {
            spawnBall();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        AteamScoreText.text = AteamScore.ToString();
        BteamScoreText.text = BteamScore.ToString();

    }

    private void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }

    public void spawnBall()
    {        
        PhotonNetwork.Instantiate(ball.name, ballPosition.position, ballPosition.rotation);
    }
}
