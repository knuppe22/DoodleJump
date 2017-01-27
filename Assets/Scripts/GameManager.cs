using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public enum Seasons { Spring, Summer, Autumn, Winter };
    public Seasons season = Seasons.Spring;
    public GameObject music;
    public MusicInfo musicInfo;
    public int score = 0;

    // public enum StepType { Normal, Double, Hold, ... };  // 추후에 추가바람(StepInfo.cs:8)

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
        GameObject.Find("Main Camera").transform.Translate(new Vector2(0, 0.5f) * Time.deltaTime);

    }
}
