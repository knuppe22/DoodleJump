using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInfo : MonoBehaviour {
    public float bpm;
    public float offsetms = 0;

    // Use this for initialization
    void Start () {

    }

    public float GetMsPerBeat()
    {
        return 1.0f / bpm * 60 * 1000;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
