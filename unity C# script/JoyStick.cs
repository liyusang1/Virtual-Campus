using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImage;
    private Vector3 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    [PunRPC]
    public virtual void OnDrag(PointerEventData ped)
    {
        //Debug.Log("JoyStick >>> OnDrag");

        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 , pos.y * 2 , 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y) / 3);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    [PunRPC]
    public float GetHorizontalValue()
    {
        return inputVector.x;
    }
    [PunRPC]
    public float GetVerticalValue()
    {
        return inputVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
