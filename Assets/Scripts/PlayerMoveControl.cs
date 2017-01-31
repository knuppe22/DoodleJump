using UnityEngine;
using System.Collections;
using System;

public class PlayerMoveControl : MonoBehaviour {
    private bool _isJumping = false;
    private GridPos _jumpGrid;
    private Vector2 _direction;
    private float _speed;

    void Start()
    {

    }

    public void ReadyToJump()
    {
        _isJumping = true;
        _jumpGrid = InputManager.instance.jumpGrid;

        int gridDiff = (int)_jumpGrid - (int)StepManager.instance.cur.GetComponent<StepInfo>().xGrid;
        _direction = Vector2.up * 1f + Vector2.right * gridDiff * 1f;
        _speed = _direction.magnitude / GameManager.instance.musicInfo.GetMsPerBeat() * 1000 * Time.deltaTime;
    }

    void Update()
    {
        if(_isJumping)
        {
            gameObject.transform.Translate(_direction.normalized * _speed);
            if (transform.position.y > StepManager.instance.next.transform.position.y)
            {
                gameObject.transform.position = (Vector2)StepManager.instance.cur.transform.position + _direction;
                _isJumping = false;
                GameManager.instance.JumpFinished(_jumpGrid == StepManager.instance.next.GetComponent<StepInfo>().xGrid);
            }
        }
    }
}