using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool isJumpButtonPressed_ = false;
    public GridPos jumpGrid_ = GridPos.Center;

    public float threshold = 0.2f;

    public void GetJumpGrid(out GridPos jumpGrid)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            jumpGrid = GridPos.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            jumpGrid = GridPos.Right;
        }
        else
        {
            jumpGrid = GridPos.Center;
        }
#elif UNITY_ANDROID
        jumpGrid = jumpGrid_;
#else
        // TODO: not cared platform
#endif

        float acx = Input.acceleration.x;

        if (Mathf.Abs(acx) > threshold)
        {
            jumpGrid = (GridPos)Mathf.Sign(acx);
        }

        return;
    }

    public void GetJumpButtonPressed(out bool isJumpButtonPressed)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        isJumpButtonPressed = Input.GetKeyDown(KeyCode.Space);
#elif UNITY_ANDROID
        isJumpButtonPressed = isJumpButtonPressed_;
#else
        // TODO: not cared platform
#endif

        return;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
