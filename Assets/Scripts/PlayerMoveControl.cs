using UnityEngine;
using System.Collections;
using System;

public class PlayerMoveControl : MonoBehaviour {
    [SerializeField]
    InputManager InputManagerInstance;

    [SerializeField]
    RuntimeAnimatorController perfect;
    [SerializeField]
    RuntimeAnimatorController bad;
    [SerializeField]
    RuntimeAnimatorController intro;
    [SerializeField]
    GameObject jumpSFX;

    private bool isJumping = false;
    private GridPos jumpGrid;
    private Vector3 direction;
    private float speed;
    private GameObject jumpSFXobj;

    private Animator charactorAnimator;
    private enum Animation { Perfect, Bad, Intro };
    Animation animationState = Animation.Intro;

    void Start()
    {
        charactorAnimator = gameObject.GetComponent<Animator>();
    }

    public void ReadyToJump()
    {
        InputManagerInstance.GetJumpGrid(out jumpGrid);

        isJumping = true;
        if (jumpSFXobj != null)
        {
            Destroy(jumpSFXobj);
        }
        jumpSFXobj = Instantiate(jumpSFX);

        int gridDiff = (int)jumpGrid - (int)StepManager.Instance.CurrentGrid;
        direction = Vector2.up * 1f + Vector2.right * gridDiff * 1f;
        speed = direction.magnitude / BgaManager.Instance.MsPerBeat;
    }

    public void AnimationState(JudgeManager.JudgeList judge)
    {
        switch (judge)
        {
            case JudgeManager.JudgeList.Perfect:
            case JudgeManager.JudgeList.Great:
                charactorAnimator.runtimeAnimatorController = perfect;
                animationState = Animation.Perfect;
                break;
            case JudgeManager.JudgeList.Bad:
            case JudgeManager.JudgeList.Poor:
                charactorAnimator.runtimeAnimatorController = bad;
                animationState = Animation.Bad;
                break;
        }
    }

    void Update()
    {
        if (isJumping)
        {
            gameObject.transform.Translate(direction.normalized * speed * 1000 * Time.deltaTime);

            if (transform.position.y > StepManager.Instance.NextPosition.y)
            {
                isJumping = false;

                gameObject.transform.position = direction
                    + StepManager.Instance.CurrentPosition + Vector3.back;

                bool isSucceed = (jumpGrid == StepManager.Instance.NextGrid);
                GameManager.Instance.JumpFinished(isSucceed);
            }
        }
        else if(animationState == Animation.Perfect)
        {
            animationState = Animation.Intro;
            charactorAnimator.runtimeAnimatorController = intro;
        }
    }
}