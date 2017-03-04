using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour {
    [SerializeField]
    GameObject latencyField;
    [SerializeField]
    GameObject stagesBase;
    [SerializeField]
    GameObject title;
    [SerializeField]
    GameObject canvas;

    public static float latency;

    public static Seasons season = Seasons.Spring;

    private float scale;

    private void next()
    {
        int size = System.Enum.GetValues(typeof(Seasons)).Length;

        if (season == Seasons.Summer)
            season = Seasons.Spring;
        else
            season += 1;
    }

    private void prev()
    {
        int size = System.Enum.GetValues(typeof(Seasons)).Length;

        if (season == Seasons.Spring)
            season = Seasons.Summer;
        else
            season -= 1;
    }

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this);

        scale = canvas.GetComponent<RectTransform>().localScale.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (canvas)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                next();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                prev();
            else
                return;

            Vector3 pos = stagesBase.transform.position;
            pos.x = -(int)season * 650 * scale;

            stagesBase.transform.position = pos;
            title.GetComponent<Text>().text = season.ToString();
        }
    }

    public void GameStart()
    {
        float.TryParse(latencyField.GetComponent<Text>().text, out latency);
        
        SceneManager.LoadScene("InGame");
    }
}
