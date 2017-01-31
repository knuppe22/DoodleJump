using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputManager.instance.jumpGrid--;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.instance.jumpGrid++;
    }
}
