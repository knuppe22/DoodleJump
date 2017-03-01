﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public enum Seasons { Spring, Summer, Autumn, Winter };
    public Seasons season = Seasons.Spring;

    private string theme;

    private GameObject musicObj;
    private MusicInfo musicInfo;

    [SerializeField]
    InputManager InputManagerInstance;

    private Text lifeText;
    private Text comboText;
    private Text scoreText;

    private Transform cameraTransform;

    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject character;
    [SerializeField]
    GameObject hideOnPause;

    public bool isPaused = false;

    private bool isGameOver = false;

    public int life = 3;
    private int height = 0;
    private int combo = 0;
    private int judgeScore = 0;
    private int maxCombo = 0;

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

            if (value > maxCombo)
                maxCombo = value;
        }
    }
    public int MaxCombo
    {
        get { return maxCombo; }
    }
    public int Score
    {
        get { return MaxCombo * 5 + height * 100 + JudgeScore; }
    }

    // public enum StepType { Normal, Double, Hold, ... };  // 추후에 추가바람(StepInfo.cs:8)

    private bool isMoving = false;

    private float camAcc = 10f;
    private float camSpeed = 0f;
    private float camMoved = 0f;

    public void JumpFinished(bool isJumpSucceeded)
    {
        if (isJumpSucceeded)
        {
            IncrementCombo();
            scoreText.text = Score.ToString();

            isMoving = true;

            StepManager.Instance.NextStep();
        }
        else
        {
            ResetCombo();
            GameOver();
        }
    }

    public void IncrementCombo()
    {
        Combo++;
        height++;
        JudgeScore += 100;
        comboText.text = combo + " combo";
    }

    public void ResetCombo()
    {
        Combo = 0;
        comboText.text = "";
    }

    public void MissJudge()
    {
        ResetCombo();
        life--;
    }

    private void Awake()
    {
        theme = "Theme/" + season;

        GameObject step = Resources.Load<GameObject>(theme + "/Step");
        StepManager.Instance.step = step;
    }

    // Use this for initialization
    void Start()
    {
        lifeText = GameObject.Find("Life Text").GetComponent<Text>();
        comboText = GameObject.Find("Combo").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        cameraTransform = GameObject.Find("Main Camera").transform;
        
        Sprite bg = Resources.Load<Sprite>(theme + "/Background");
        GameObject.Find("Background").GetComponent<Image>().sprite = bg;
        
        GameObject music = Resources.Load<GameObject>(theme + "/BGM");
        musicInfo = music.GetComponent<MusicInfo>();
        musicObj = Instantiate(music);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (camMoved >= 1f)
            {
                camSpeed = 0f;
                camMoved = 0f;
                isMoving = false;

                StepManager.Instance.DestroyPrevStep();
            }
            else if (camMoved <= 0.5f)
                camSpeed += camAcc * Time.deltaTime;
            else
                camSpeed -= camAcc * Time.deltaTime;

            cameraTransform.Translate(new Vector2(0, camSpeed * Time.deltaTime));
            JudgeLine.instance.IncreaseY(camSpeed * Time.deltaTime);

            camMoved += camSpeed * Time.deltaTime;

            StepManager.Instance.DestroyPrevStep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeText != null)
        {
            lifeText.text = "X " + life;
        }

        if (!isGameOver && life < 0)
        {
            GameOver();
        }
        else if (isGameOver)
        {
            if (Input.anyKey)
            {
                Time.timeScale = 1;

                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }
    
    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;

            musicObj.GetComponent<AudioSource>().Pause();
            Time.timeScale = 0;

            StepManager.Instance.ToggleSteps(false);
            hideOnPause.SetActive(false);
            character.SetActive(false);

            menuPanel.SetActive(true);
        }
        else
        {
            isPaused = false;

            musicObj.GetComponent<AudioSource>().Play();
            Time.timeScale = 1;

            StepManager.Instance.ToggleSteps(true);
            hideOnPause.SetActive(true);
            character.SetActive(true);

            menuPanel.SetActive(false);
        }
    }

    public void ToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Launcher");
    }

    void GameOver()
    {
        isGameOver = true;
        isPaused = true;

        Time.timeScale = 0;

        StepManager.Instance.ToggleSteps(false);

        Destroy(GameObject.Find("Character"));
        Destroy(GameObject.Find("Life"));
        Destroy(GameObject.Find("Judge Line"));
        Destroy(GameObject.Find("Judge"));
        Destroy(GameObject.Find("Score"));
        Destroy(GameObject.Find("Pause"));
        Destroy(musicObj);

        GameObject.Find("Game Over Text").GetComponent<Text>().text
            = "Game Over\n\nScore: " + Score
            + "\n\n"
            + "Press Jump\n"
            + "to restart";
    }
}
