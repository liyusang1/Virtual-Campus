using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactiveText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("대화종료");
                deactiveTextBox();
            }
        }
    }

    public void deactiveTextBox()
    {
        gameObject.SetActive(false);
    }
}
