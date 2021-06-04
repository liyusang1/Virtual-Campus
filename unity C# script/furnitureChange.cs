using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnitureChange : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite fur_sprite;
    // Start is called before the first frame update
    void Start()
    {
       //sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        sr.sprite = fur_sprite;
    }
}
