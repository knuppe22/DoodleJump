using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class JudgeManager : MonoBehaviour {
    [SerializeField]
    InputManager InputManagerInstance;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject judgeObject;

    Text judgeText;

    private PlayerMoveControl playerControl;

    public static JudgeManager instance;

    public enum JudgeList
    {
        Perfect,
        Great,
        Bad,
        Poor
    };
    private readonly float[] judgeTiming = { 80f, 100f, 120f, 0f };
    private readonly int[] judgeScores = { 10, 7, 3, 0 };
    private readonly Color[] judgeColors = { Color.cyan, Color.yellow, Color.blue, Color.red };

    public float latency = 225f;
    public float elapsedTime = 0;

    private bool isJumpButtonPressedPrev = false;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        latency = Launcher.latency;
        elapsedTime -= 2 * BgaManager.Instance.MsPerBeat + BgaManager.Instance.MsOffset + latency;

        playerControl = player.GetComponent<PlayerMoveControl>();
        judgeText = judgeObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.isPaused)
            return;

        JudgeList judge = JudgeList.Poor;

        elapsedTime += Time.deltaTime * 1000;

        foreach (JudgeList j in Enum.GetValues(typeof(JudgeList)))
        {
            if (Mathf.Abs(elapsedTime) < judgeTiming[(int)j])
            {
                judge = j;
                break;
            }
        }

        if (judge == JudgeList.Poor && elapsedTime > judgeTiming[(int)JudgeList.Bad])
        {
            GameManager.Instance.MissJudge();
            elapsedTime -= 2 * BgaManager.Instance.MsPerBeat;

            judgeText.text = judge.ToString().ToUpper();
            judgeText.color = judgeColors[(int)judge];

            playerControl.AnimationState(judge);
        }
        
        bool isJumpButtonPressed;
        InputManagerInstance.GetJumpButtonPressed(out isJumpButtonPressed);
        
        if (isJumpButtonPressed && !isJumpButtonPressedPrev)
        {
            judgeText.text = judge.ToString().ToUpper();
            judgeText.color = judgeColors[(int)judge];

            GameManager.Instance.JudgeScore += judgeScores[(int)judge];

            Debug.Log(judge + " (" + elapsedTime +")");

            if (judge != JudgeList.Poor)
            {
                elapsedTime -= 2 * BgaManager.Instance.MsPerBeat;
            }

            playerControl.AnimationState(judge);

            if (new[] { JudgeList.Perfect, JudgeList.Great }.Contains(judge))
            {
                playerControl.ReadyToJump();
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
