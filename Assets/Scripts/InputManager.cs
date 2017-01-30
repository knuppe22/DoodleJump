using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    Button LeftJumpButton;
    Button CenterJumpButton;
    Button RightJumpButton;

    public GridPos _JumpGrid()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return GridPos.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            return GridPos.Right;
        else
            return GridPos.Center;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
