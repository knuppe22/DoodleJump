using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JudgeManager : MonoBehaviour {
    [SerializeField]
    InputManager InputManagerInstance;
    [SerializeField]
    GameObject player;

    public static JudgeManager instance;
    public enum JudgeList { Poor, Bad, Perfect };
    public JudgeList judge;
    
    public float perfectms = 80f;
    public float badms = 120f;
    public float latency = 225f;
    public float elapsedTime = 0;

    private bool isJumpButtonPressedPrev = false;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        elapsedTime -= GameManager.Instance.MsOffset + latency;
    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime * 1000;

        if (Mathf.Abs(elapsedTime) < perfectms)
        {
            judge = JudgeList.Perfect;
        }
        else if (Mathf.Abs(elapsedTime) < badms)
        {
            judge = JudgeList.Bad;
        }
        else
        {
            judge = JudgeList.Poor;
        }

        if (elapsedTime > badms)
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

            if(judge != JudgeList.Poor)
            {
                elapsedTime -= 2 * GameManager.Instance.MsPerBeat;
            }

            if (judge == JudgeList.Perfect)
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
