using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JudgeManager : MonoBehaviour {
    public static JudgeManager instance;
    public enum JudgeList { Miss, Perfect };
    public JudgeList judge;
    
    public float judgems = 200f;
    public float latency = 155f;
    private float _elapsedTime = 0;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (GameManager.instance.musicInfo.offsetms > 0)
        {
            _elapsedTime = (-1 * GameManager.instance.musicInfo.offsetms) % GameManager.instance.musicInfo.GetMsPerBeat() + GameManager.instance.musicInfo.GetMsPerBeat();
        }

        _elapsedTime -= latency;
    }
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime * 1000;
        if (_elapsedTime > 2 * GameManager.instance.musicInfo.GetMsPerBeat())
        {
            _elapsedTime -= 2 * GameManager.instance.musicInfo.GetMsPerBeat();
            Debug.Log("On");
        }

        if (_elapsedTime > judgems && _elapsedTime < 2 * GameManager.instance.musicInfo.GetMsPerBeat() - judgems)
        {
            judge = JudgeList.Miss;
        }
        else
        {
            judge = JudgeList.Perfect;
        }
    }
}
