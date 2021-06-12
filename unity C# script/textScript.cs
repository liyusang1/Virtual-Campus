using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{
    public GameObject tb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        //GameObject tb = GameObject.Find("NPC1_script");
        GameObject tb = GameObject.FindGameObjectWithTag("textbox");

        tb.SetActive(false);
        //textb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(tb.activeSelf == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("대화종료");
                deactiveTextBox();
            }
        }
    }

    public void activeButton()
    {
        gameObject.SetActive(true);
    }

    public void activeTextBox()
    {
        if(tb.activeSelf == false)
        {
            gameObject.SetActive(false);
            tb.SetActive(true);
        }
        
    }

    public void deactiveTextBox()
    {
        tb.SetActive(false);
    }
}
