using UnityEngine;
using System.Collections;
using System;

public class PlayerMoveControl : MonoBehaviour {
    private bool isJumping = false;
    private GridPos jumpGrid;
    private Vector3 direction;
    private float speed;

    public void ReadyToJump()
    {
        isJumping = true;

        jumpGrid = InputManager.Instance.jumpGrid;

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