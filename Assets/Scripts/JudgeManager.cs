using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class JudgeManager : MonoBehaviour {
    [SerializeField]
    InputManager InputManagerInstance;
    [SerializeField]
    GameObject player;

    public static JudgeManager instance;

    public enum JudgeList
    {
        Perfect,
        Good,
        Bad,
        Poor
    };
    public readonly float[] judgeTiming = { 80f, 100f, 120f, 300f };
    public readonly int[] judgeScores = { 10, 7, 3, 0 };

    public float latency = 225f;
    public float elapsedTime = 0;

    private bool isJumpButtonPressedPrev = false;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        latency = Launcher.latency;
        elapsedTime -= GameManager.Instance.MsOffset + latency;
    }
	
	// Update is called once per frame
	void Update ()
    {
        JudgeList judge = JudgeList.Poor;

        elapsedTime += Time.deltaTime * 1000;

        foreach (JudgeList j in Enum.GetValues(typeof(JudgeList)))
        {
            if (Mathf.Abs(elapsedTime) < judgeTiming[(int)j])
            {
                judge = j;
                GameManager.Instance.JudgeScore += judgeScores[(int)j];
                break;
            }
        }

        if (elapsedTime > judgeTiming[(int)JudgeList.Poor])
        {
            GameManager.Instance.MissJudge();
            elapsedTime -= 2 * GameManager.Instance.MsPerBeat;
            Debug.Log("Miss");
        }
        
        bool isJumpButtonPressed;
        InputManagerInstance.GetJumpButtonPressed(out isJumpButtonPressed);
        
        if (isJumpButtonPressed && !isJumpButtonPressedPrev)
        {
            Debug.Log(judge + " (" + elapsedTime +")");

            if (judge != JudgeList.Poor)
            {
                elapsedTime -= 2 * GameManager.Instance.MsPerBeat;
            }

            if (new[] { JudgeList.Perfect, JudgeList.Good }.Contains(judge))
            {
                player.GetComponent<PlayerMoveControl>().ReadyToJump();
            }
            else
            {
                GameManager.Instance.ResetCombo();
                GameManager.Instance.life--;
            }
        }
        isJumpButtonPressedPrev = isJumpButtonPressed;
    }
}
