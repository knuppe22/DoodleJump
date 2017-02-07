using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour {
    [SerializeField]
    GameObject latencyField;

    public static float latency;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void GameStart()
    {
        float.TryParse(latencyField.GetComponent<Text>().text, out latency);
        
        SceneManager.LoadScene("InGame");
    }
}
