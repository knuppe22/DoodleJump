using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    private LineRenderer LineDrawer;

    public static JudgeLine instance;

    private float ThetaScale = 0.001f;

    public float radius;
    public float width;
    
    private float basey = -1.5f;

    public void IncreaseY(float y)
    {
        basey += y;
    }

    void Start()
    {
        instance = this;

        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.material = new Material(Shader.Find("Particles/Additive"));
        LineDrawer.startColor = Color.grey;
        LineDrawer.endColor = Color.grey;
        LineDrawer.startWidth = width;
    }

    void DrawCircle(float radius)
    {
        float theta = 0f;
        int size = (int)((1f / ThetaScale) + 1f);

        LineDrawer.numPositions = size + 1;

        for (int i = 0; i <= size; i++)
        {
            theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta) + basey;
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void Update()
    {
        DrawCircle((1000-JudgeManager.instance.elapsedTime)/4000f);
    }
}