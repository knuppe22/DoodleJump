using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgaManager : MonoBehaviour
{
    private static BgaManager instance;
    public static BgaManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BgaManager>();
            }

            return instance;
        }
    }

    private Text countInText;
    private GameObject uiCanvasObj;
    private GameObject musicObj;
    private MusicInfo musicInfo;
    private GameObject cinObj;

    public float MsPerBeat
    {
        get { return 60 * 1000 * 1f / musicInfo.bpm; }
    }
    public float MsOffset { get { return musicInfo.offsetms; } }
    public float NumBeats { get { return musicInfo.beatsPerBar; } }

    float lastCreateTime = Mathf.Infinity;
    bool paused = false;

    void Start()
    {
        countInText = GameObject.Find("CountInText").GetComponent<Text>();
        uiCanvasObj = GameObject.Find("UI Canvas");
    }

    void Update()
    {
        if (!paused)
        {
            if (lastCreateTime + MsPerBeat * NumBeats < Time.time * 1000)
            {
                countInText.gameObject.SetActive(false);
                uiCanvasObj.SetActive(true);
                if (musicObj.activeSelf && !musicObj.GetComponent<AudioSource>().isPlaying)
                {
                    Resume();
                }
                Destroy(cinObj);
            }
            else
            {
                countInText.gameObject.SetActive(true);
                uiCanvasObj.SetActive(false);
                float deltaTime = Time.time * 1000 - lastCreateTime;
                countInText.text = ((int)(1 + NumBeats - deltaTime / MsPerBeat)).ToString();
            }
        }
    }
    
    public void Create(string theme)
    {
        GameObject music = Resources.Load<GameObject>(theme + "/BGM");
        musicObj = Instantiate(music);
        musicInfo = music.GetComponent<MusicInfo>();
        
        lastCreateTime = Time.time * 1000;

        GameObject cin = Resources.Load<GameObject>(theme + "/Count In");
        cinObj = Instantiate(cin);
    }

    public void Pause()
    {
        musicObj.GetComponent<AudioSource>().Pause();
        paused = true;
    }

    public void Resume()
    {
        musicObj.GetComponent<AudioSource>().Play();
        paused = false;
    }

    public void Destroy()
    {
        Pause();
        //Destroy(musicObj);
    }
}
