using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour {
    private static StepManager instance;
    public GameObject step;

    private GameObject prev;
    private GameObject cur;
    private GameObject next;
    private GameObject nnext;

    public static StepManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StepManager>();
            }

            return instance;
        }
    }

    public GridPos CurrentGrid
    {
        get { return cur.GetComponent<StepInfo>().xGrid; }
    }
    public GridPos NextGrid
    {
        get { return next.GetComponent<StepInfo>().xGrid; }
    }

    public Vector3 CurrentPosition
    {
        get { return cur.transform.position; }
    }
    public Vector3 NextPosition
    {
        get { return next.transform.position; }
    }


    // TODO: Two steps with same height

    // Use this for initialization
    void Start()
    {
        cur = Instantiate(step, Vector3.zero, Quaternion.identity);
        Spawn(ref next, ref cur);
        Spawn(ref nnext, ref next);
    }
	
    public void NextStep()
    {
        if (prev != null)
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
