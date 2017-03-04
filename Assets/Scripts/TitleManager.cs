using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    [SerializeField]
    GameObject nameField;

    public static string userName;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}

    public void GotoLauncherScene()
    {
        userName = nameField.GetComponent<Text>().text;

        SceneManager.LoadScene("Launcher");
    }

    public void GotoRankingScene()
    {
        SceneManager.LoadScene("Scoreboard");
    }
}
