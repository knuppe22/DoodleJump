using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    InputManager InputManagerInstance;

    public void OnPointerDown(PointerEventData eventData)
    {
        InputManagerInstance.isJumpButtonPressed_ = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManagerInstance.isJumpButtonPressed_ = false;
    }
}
