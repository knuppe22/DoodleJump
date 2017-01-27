using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {
    private bool _isJumping = false;

    private PositionManager.Grid3 _jumpGrid;
    private PositionManager.Grid3 _JumpGrid()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return PositionManager.Grid3.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            return PositionManager.Grid3.Right;
        else
            return PositionManager.Grid3.Center;
    }
    private Vector2 _direction;
    private float _speed;
    
    private void _JumpFinished(bool isJumpSuccessed)
    {
        if (isJumpSuccessed == true)
        {
            GameManager.instance.score++;
                StepManager.instance.NextStep();
        }
        else
        {
            Debug.Log("Fail");
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(JudgeManager.instance.judge);
        }
        if(JudgeManager.instance.judge != JudgeManager.JudgeList.Miss && Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
            _jumpGrid = _JumpGrid();

            int gridDiff = (int)_jumpGrid - (int)StepManager.instance.cur.GetComponent<StepInfo>().xGrid;
            _direction = Vector2.up * PositionManager.yInterval
                         + Vector2.right * gridDiff * PositionManager.xInterval;
            _speed = _direction.magnitude / GameManager.instance.musicInfo.GetMsPerBeat() * 1000 * Time.deltaTime;
        }
        if(_isJumping)
        {
            gameObject.transform.Translate(_direction.normalized * _speed);
            if (transform.position.y > StepManager.instance.next.transform.position.y)
            {

                gameObject.transform.position = (Vector2)StepManager.instance.cur.transform.position + _direction;
                _isJumping = false;
                _JumpFinished(_jumpGrid == StepManager.instance.next.GetComponent<StepInfo>().xGrid);
            }
        }
    }
}