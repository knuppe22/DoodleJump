using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public enum Seasons { Spring, Summer, Autumn, Winter };
    public Seasons season = Seasons.Spring;

    public GameObject music;
    private GameObject musicObj;
    private MusicInfo musicInfo;

    [SerializeField]
    InputManager InputManagerInstance;

    public int life = 3;
    private int height = 0;
    private int combo = 0;
    private int judgeScore = 0;
    private int lastCombo = 0;

    private bool isGameOver = false;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public float MsPerBeat
    {
        get { return 60 * 1000 * 1f / musicInfo.bpm; }
    }
    public float MsOffset { get { return musicInfo.offsetms; } }

    public int JudgeScore
    {
        get { return judgeScore; }
        set
        {
            judgeScore = value;
        }
    }
    public int Combo
    {
        get { return combo; }
        set
        {
            combo = value;
        }
    }
    public int Score
    {
        get { return Combo * 5 + height * 100 + JudgeScore; }
    }

    // public enum StepType { Normal, Double, Hold, ... };  // 추후에 추가바람(StepInfo.cs:8)

    public void JumpFinished(bool isJumpSucceeded)
    {
        if (isJumpSucceeded)
        {
            IncrementCombo();

            GameObject.Find("Main Camera").transform.Translate(new Vector2(0, 1f));
            JudgeLine.instance.IncreaseY(1f);
            StepManager.Instance.NextStep();
        }
        else
        {
            Debug.Log("Fail");
            ResetCombo();
            IsGameOver();
        }
    }

    public void IncrementCombo()
    {
        Combo++;
        height++;
        JudgeScore += 100;
    }

    public void ResetCombo()
    {
        lastCombo = Combo;
        Combo = 0;
    }

    public void MissJudge()
    {
        ResetCombo();
        life--;
    }

    // Use this for initialization
    void Start()
    {
        musicObj = Instantiate(music);
        musicInfo = music.GetComponent<MusicInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject scoreText = GameObject.Find("Life Text");
        if(scoreText != null)
        {
            scoreText.GetComponent<Text>().text = "Life: " + life;
        }

        if (!isGameOver && life < 0)
        {
            IsGameOver();
        }
        else if (isGameOver)
        {
            bool isJumpButtonPressed;
            InputManagerInstance.GetJumpButtonPressed(out isJumpButtonPressed);

            if (isJumpButtonPressed)
            {
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }

    void IsGameOver()
    {
        isGameOver = true;
        Debug.Log("LastCombo=" + lastCombo);
//        score += lastCombo * 5;

        Destroy(GameObject.Find("Life Text"));
        Destroy(GameObject.Find("Judge Line"));
        Destroy(musicObj);

        GameObject.Find("Game Over Text").GetComponent<Text>().text
            = "Game Over\n\nScore: " + Score
            + "\n\n"
            + "Press Jump\n"
            + "to restart";
    }
}
