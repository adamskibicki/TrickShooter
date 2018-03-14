using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image bgImg;
    Image joystickImg;
    Vector3 inputVector;

    protected void Awake()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Jostick IMG

            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2.5f), inputVector.y * (bgImg.rectTransform.sizeDelta.y / 2.5f));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    //te dwie funkcje mozna ustawic do poruszania sie w innj plaszczyznie - wystarczy zmienic w danej funkcji wspolrzedna na inna
    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Horizontal");
    }

}
