using UnityEngine;

public class MusicInfo : MonoBehaviour {
    public float bpm;
    public float offsetms = 0;
    
    public float GetMsPerBeat()
    {
        return 1.0f / bpm * 60 * 1000;
    }
	
}
