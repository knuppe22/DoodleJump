using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    private LineRenderer LineDrawer;

    public static JudgeLine instance;

    private float ThetaScale = 0.01f;

    private float basex;
    private float basey;

    public float offset = 0f;

    public void IncreaseY(float y)
    {
        basey += y;
    }

    void Start()
    {
        instance = this;

        LineDrawer = GetComponent<LineRenderer>();
        LineDrawer.startColor = Color.grey;
        LineDrawer.endColor = Color.grey;

        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            GetComponent<RectTransform>(),
            gameObject.transform.position, Camera.main, out worldPoint);

        basex = worldPoint.x;
        basey = worldPoint.y;
    }

    void DrawCircle(float radius)
    {
        float theta = 0f;
        int size = (int)((1f / ThetaScale) + 1f);

        LineDrawer.numPositions = size + 1;

        for (int i = 0; i <= size; i++)
        {
            theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(theta) + basex;
            float y = radius * Mathf.Sin(theta) + basey;
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void Update()
    {
        DrawCircle((1000-JudgeManager.instance.elapsedTime + offset)/4000f);
    }
}