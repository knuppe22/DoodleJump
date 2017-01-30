using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour {
    public static StepManager instance;
    public GameObject step;

    public GameObject prev;
    public GameObject cur;
    public GameObject next;
    public GameObject nnext;

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        cur = Instantiate(step, Vector3.zero, Quaternion.identity);
        Spawn(ref next, ref cur);
        Spawn(ref nnext, ref next);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextStep()
    {
        if(prev != null)
        {
            Destroy(prev);
        }
        prev = cur;
        cur = next;
        next = nnext;
        Spawn(ref nnext, ref next);
    }

    public void Spawn(ref GameObject over, ref GameObject under)
    {
        int grid = Random.Range(-1, 2);
        Vector2 pos = under.transform.position;
        pos.y += 1f;
        pos.x = grid * 1f;
        over = Instantiate(step, pos, Quaternion.identity);
        over.GetComponent<StepInfo>().xGrid = (GridPos)grid;
    }
}
