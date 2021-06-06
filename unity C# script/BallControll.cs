using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public PlayerControllerScript pcs;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb2d.AddForce(new Vector3(pcs._moveVector.x * 100f, pcs._moveVector.y * 100f, 0f));
        }
    }
}
