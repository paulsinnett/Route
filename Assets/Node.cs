using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    public Renderer myRenderer;

    public void SetColour(Color colour)
    {
        myRenderer.material.color = colour;
    }

    internal bool IsHigherThan(Node top)
    {
        return transform.position.z > top.transform.position.z;
    }
}
