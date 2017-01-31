using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    InputManager inputManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        inputManager.isJumpButtonPressed = true;
    }
}
