using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Seasons season = Seasons.Spring;

    private string theme;

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
    [SerializeField]
    GameObject gameover;

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
        theme = "Theme/" + Launcher.season;

        GameObject step = Resources.Load<GameObject>(theme + "/Step");
        StepManager.Instance.step = step;
    }

    // Use this for initialization
    void Start()
    {
        BgaManager.Instance.Create(theme);

        lifeText = GameObject.Find("Life Text").GetComponent<Text>();
        comboText = GameObject.Find("Combo").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        cameraTransform = GameObject.Find("Main Camera").transform;

        Sprite bg = Resources.Load<Sprite>(theme + "/Background");
        GameObject.Find("Background").GetComponent<Image>().sprite = bg;
        
        Sprite gameOverBg = Resources.Load<Sprite>(theme + "/GameOverBg");
        GameObject.Find("Game Over Bg").GetComponent<Image>().sprite = gameOverBg;
        Sprite gameOverRank = Resources.Load<Sprite>(theme + "/Rank");
        GameObject.Find("Rank Sprite").GetComponent<Image>().sprite = gameOverRank;
        GameObject.Find("Game Over").SetActive(false);
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

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;

            BgaManager.Instance.Pause();
            StepManager.Instance.ToggleSteps(false);
            hideOnPause.SetActive(false);
            character.SetActive(false);

            menuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;

            BgaManager.Instance.Resume();
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
        gameover.SetActive(true);

        Time.timeScale = 0;

        BgaManager.Instance.Destroy();
        StepManager.Instance.ToggleSteps(false);

        Destroy(GameObject.Find("Character"));
        Destroy(GameObject.Find("Life"));
        Destroy(GameObject.Find("Judge Line"));
        Destroy(GameObject.Find("Judge"));
        Destroy(GameObject.Find("Score"));
        Destroy(GameObject.Find("Pause"));

        GameResult result = new GameResult();
        result.score = -Score;
        result.name = TitleManager.userName;
        result.season = Launcher.season;
        RankManager.Instance.RegisterGameResult(Launcher.season, result);

        GameObject.Find("Final Score").GetComponent<Text>().text = Score.ToString();
        GameObject.Find("Final Combo").GetComponent<Text>().text = MaxCombo.ToString() + " combo";
        int rank = RankManager.Instance.GetRank(Launcher.season, Score);
        Debug.Log(rank);
        GameObject.Find("Rank").GetComponent<Text>().text = rank.ToString();
        string th;
        switch(rank % 10)
        {
            case 1:
                th = "st";
                break;
            case 2:
                th = "nd";
                break;
            case 3:
                th = "nd";
                break;
            default:
                th = "st";
                break;
        }
        GameObject.Find("Th").GetComponent<Text>().text = th;
    }
}
