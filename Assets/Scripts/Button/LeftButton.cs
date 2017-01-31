using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputManager inputManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        inputManager.jumpGrid--;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputManager.jumpGrid++;
    }
}
