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
    [SerializeField]
    GameObject leftButton;
    [SerializeField]
    GameObject rightButton;

    public static float latency;

    public static Seasons season = Seasons.Spring;

    private float scale;

    public void Next()
    {
        int size = System.Enum.GetValues(typeof(Seasons)).Length;

        if (season != Seasons.Summer)
            season++;
    }

    public void Prev()
    {
        int size = System.Enum.GetValues(typeof(Seasons)).Length;

        if (season != Seasons.Spring)
            season--;
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
                Next();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                Prev();

            Vector3 pos = stagesBase.transform.position;
            pos.x = -(int)season * 650 * scale;

            stagesBase.transform.position = pos;
            title.GetComponent<Text>().text = season.ToString();

            if (season == Seasons.Spring)
            {
                leftButton.SetActive(false);
                rightButton.SetActive(true);
            }
            else
            {
                leftButton.SetActive(true);
                rightButton.SetActive(false);
            }
        }

    }

    public void GameStart()
    {
        float.TryParse(latencyField.GetComponent<Text>().text, out latency);
        
        SceneManager.LoadScene("InGame");
    }

    public void GotoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
