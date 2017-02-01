using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputManager InputManagerInstance;

    public void OnPointerDown(PointerEventData eventData)
    {
        InputManagerInstance.jumpGrid_ = GridPos.Right;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManagerInstance.jumpGrid_ = GridPos.Center;
    }
}
