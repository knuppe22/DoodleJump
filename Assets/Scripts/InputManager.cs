using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;
    public static InputManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }

            return instance;
        }
    }

    public bool isJumpButtonPressed = false;
    public GridPos jumpGrid = GridPos.Center;

    // For PC use
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            isJumpButtonPressed = true;

        if (Input.GetKey(KeyCode.LeftArrow))
            jumpGrid = GridPos.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            jumpGrid = GridPos.Right;
        else
            jumpGrid = GridPos.Center;
    }

}
