using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitChanger_paircase : MonoBehaviour
{
    [Header("Sprite To Change")]
    public SpriteRenderer bodyPart1;
    public SpriteRenderer bodyPart2;

    [Header("Sprite To Cycle Through")]
    public List<Sprite> options1 = new List<Sprite>();
    public List<Sprite> options2 = new List<Sprite>();

    private int currentOptions = 0;

    public void NextOption()
    {
        currentOptions++;
        if (currentOptions >= options1.Count)
        {
            currentOptions = 0;
        }

        bodyPart1.sprite = options1[currentOptions];
        bodyPart2.sprite = options2[currentOptions];
    }

    public void PreviousOption()
    {
        currentOptions--;
        if (currentOptions <= 0)
        {
            currentOptions = options1.Count - 1;
        }

        bodyPart1.sprite = options1[currentOptions];
        bodyPart2.sprite = options2[currentOptions];
    }
}
