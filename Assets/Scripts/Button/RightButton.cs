using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputManager inputManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        inputManager.jumpGrid++;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputManager.jumpGrid--;
    }
}
