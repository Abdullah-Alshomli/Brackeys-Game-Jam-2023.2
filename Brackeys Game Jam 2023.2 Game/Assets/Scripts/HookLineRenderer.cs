using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer)
        {
            lineRenderer.SetPosition(1,transform.position);
            lineRenderer.SetPosition(0,PlayerCotrol.hookPostion);
        }

    }
}
