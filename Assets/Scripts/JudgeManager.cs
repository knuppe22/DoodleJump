using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JudgeManager : MonoBehaviour {
    [SerializeField]
    GameObject player;

    public static JudgeManager instance;
    public enum JudgeList { Poor, Bad, Perfect };
    public JudgeList judge;
    
    public float perfectms = 80f;
    public float badms = 120f;
    public float latency = 225f;
    private float _elapsedTime = 0;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        /*
        if (GameManager.instance.musicInfo.offsetms > 0)
        {
            _elapsedTime = (-1 * GameManager.instance.musicInfo.offsetms) % GameManager.instance.musicInfo.GetMsPerBeat() + GameManager.instance.musicInfo.GetMsPerBeat();
        }
        */
        _elapsedTime -= GameManager.instance.musicInfo.offsetms;
        _elapsedTime -= latency;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _elapsedTime += Time.deltaTime * 1000;

        if (Mathf.Abs(_elapsedTime) < perfectms)
        {
            judge = JudgeList.Perfect;
        }
        else if (Mathf.Abs(_elapsedTime) < badms)
        {
            judge = JudgeList.Bad;
        }
        else
        {
            judge = JudgeList.Poor;
        }

        if (_elapsedTime > badms)
        {
            GameManager.instance.MissJudge();
            _elapsedTime -= 2 * GameManager.instance.musicInfo.GetMsPerBeat();
            Debug.Log("Miss");
        }

        if (InputManager.instance.isJumpButtonPressed)
        {
            Debug.Log(judge + " (" + _elapsedTime +")");
            if(judge != JudgeList.Poor)
            {
                _elapsedTime -= 2 * GameManager.instance.musicInfo.GetMsPerBeat();
            }
            InputManager.instance.isJumpButtonPressed = false;

            if (judge == JudgeList.Perfect)
            {
                player.GetComponent<PlayerMoveControl>().ReadyToJump();
            }
            else
            {
                GameManager.instance.ResetCombo();
                GameManager.instance.life--;
            }
        }
    }
}
