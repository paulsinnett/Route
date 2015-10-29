using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Route : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void SetRoute(List<Node> nodes)
    {
        lineRenderer.SetVertexCount(nodes.Count + 1);
        for (int i = 0; i < nodes.Count; ++i)
        {
            lineRenderer.SetPosition(i, nodes[i].transform.position);
        }
        lineRenderer.SetPosition(nodes.Count, nodes[0].transform.position);
    }
}
