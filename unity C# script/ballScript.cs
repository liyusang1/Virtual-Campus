using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public SoccerManager soccerManager;
    // Start is called before the first frame update
    void Start()
    {
        soccerManager = FindObjectOfType<SoccerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "goalpost_1")
        {
            soccerManager.AteamScore++;
            soccerManager.spawnBall();
            Destroy(this.gameObject);
        }

        if (collision.name == "goalpost_2")
        {
            soccerManager.BteamScore++;
            soccerManager.spawnBall();
            Destroy(this.gameObject);
        }
    }
}
