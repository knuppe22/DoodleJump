using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    Button LeftButton;
    [SerializeField]
    Button JumpButton;
    [SerializeField]
    Button RightButton;

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
		
	}
	
	// Update is called once per frame
	void Update () {
        // This if for PC
        /*
        jumpGrid = _JumpGrid();
        if (Input.GetKey(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }
        */
	}
}
