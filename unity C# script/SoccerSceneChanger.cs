using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoccerSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void GoToSoccer()
    {
        SceneManager.LoadScene("SoccerLobby");
    }
}
