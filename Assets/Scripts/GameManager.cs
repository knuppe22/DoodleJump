using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Seasons { Spring, Summer, Autumn, Winter };
    public Seasons season = Seasons.Spring;
    public GameObject music;
    private GameObject _musicObj;
    public MusicInfo musicInfo;
    public int life = 3;
    public int score = 0;
    public int combo = 0;
    private int _lastCombo = 0;
    private bool _isGameOver = false;

    // public enum StepType { Normal, Double, Hold, ... };  // 추후에 추가바람(StepInfo.cs:8)

    public void JumpFinished(bool isJumpSucceeded)
    {
        if (isJumpSucceeded == true)
        {
            combo++;
            score += 100;
            GameObject.Find("Main Camera").transform.Translate(new Vector2(0, 1f));
            StepManager.instance.NextStep();
        }
        else
        {
            Debug.Log("Fail");
            ResetCombo();
            IsGameOver();
        }
    }

    public void ResetCombo()
    {
        _lastCombo = combo;
        combo = 0;
    }

    public void MissJudge()
    {
        ResetCombo();
        life--;
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
        _musicObj = Instantiate(music);
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

        if (!_isGameOver && life < 0)
        {
            IsGameOver();
        }
    }

    void IsGameOver()
    {
        _isGameOver = true;
        Debug.Log("LastCombo=" + _lastCombo);
        score += _lastCombo * 5;
        Destroy(GameObject.Find("Life Text"));
        Destroy(_musicObj);
        GameObject.Find("Game Over Text").GetComponent<Text>().text = "Game Over\n\nScore: " + score;
    }
}
