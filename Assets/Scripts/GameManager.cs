using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Seasons { Spring, Summer, Autumn, Winter };
    public Seasons season = Seasons.Spring;
    public GameObject music;
    public MusicInfo musicInfo;
    public int score = 0;

    // public enum StepType { Normal, Double, Hold, ... };  // 추후에 추가바람(StepInfo.cs:8)

    public void JumpFinished(bool isJumpSucceeded)
    {
        if (isJumpSucceeded == true)
        {
            score += 100;
            GameObject.Find("Main Camera").transform.Translate(new Vector2(0, 1f));
            StepManager.instance.NextStep();
        }
        else
        {
            Debug.Log("Fail");
        }
    }
    public void MissJudge()
    {
        //life--;
    }

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Instantiate(music);
        musicInfo = music.GetComponent<MusicInfo>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
