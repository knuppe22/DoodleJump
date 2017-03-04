using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject musicObj;
    private MusicInfo musicInfo;

    public float MsPerBeat
    {
        get { return 60 * 1000 * 1f / musicInfo.bpm; }
    }
    public float MsOffset { get { return musicInfo.offsetms; } }
    
    public void Create(string theme)
    {
        GameObject music = Resources.Load<GameObject>(theme + "/BGM");
        musicObj = Instantiate(music);
        musicInfo = music.GetComponent<MusicInfo>();
    }

    public void Pause()
    {
        musicObj.GetComponent<AudioSource>().Pause();
    }

    public void Resume()
    {
        musicObj.GetComponent<AudioSource>().Play();
    }

    public void Destroy()
    {
        Destroy(musicObj);
    }
}
