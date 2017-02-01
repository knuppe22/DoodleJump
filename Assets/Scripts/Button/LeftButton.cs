using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputManager InputManagerInstance;

    public void OnPointerDown(PointerEventData eventData)
    {
        InputManagerInstance.jumpGrid_ = GridPos.Left;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManagerInstance.jumpGrid_ = GridPos.Center;
    }
}
