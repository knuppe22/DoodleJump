using UnityEngine;
using System.Collections;
using System;

public class PlayerMoveControl : MonoBehaviour {
    [SerializeField]
    InputManager InputManagerInstance;

    private bool isJumping = false;
    private GridPos jumpGrid;
    private Vector3 direction;
    private float speed;

    void Start()
    {

    }

    public void ReadyToJump()
    {
        InputManagerInstance.GetJumpGrid(out jumpGrid);

        isJumping = true;

        int gridDiff = (int)jumpGrid - (int)StepManager.Instance.CurrentGrid;
        direction = Vector2.up * 1f + Vector2.right * gridDiff * 1f;
        speed = direction.magnitude / GameManager.Instance.MsPerBeat
            * 1000 * Time.deltaTime;
    }

    void Update()
    {
        if(isJumping)
        {
            gameObject.transform.Translate(direction.normalized * speed);

            if (transform.position.y > StepManager.Instance.NextPosition.y)
            {
                isJumping = false;

                gameObject.transform.position
                    = StepManager.Instance.CurrentPosition + direction;

                bool isSucceed = (jumpGrid == StepManager.Instance.NextGrid);
                GameManager.Instance.JumpFinished(isSucceed);
            }
        }
    }
}