using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool isJumpButtonPressed = false;
    public GridPos jumpGrid = GridPos.Center;

    // This is for PC
    /*
    private GridPos _JumpGrid()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return GridPos.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            return GridPos.Right;
        else
            return GridPos.Center;
    }
    */

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // This if for PC
        /*
        jumpGrid = _JumpGrid();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }
        */
	}
}
