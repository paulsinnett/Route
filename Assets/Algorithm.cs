using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorithm : MonoBehaviour
{
    public Node nodeTemplate;
    public Route routeTemplate;
    public int nodeCount = 100;
    public float radius = 100f;

    List<Node> nodes = new List<Node>();
    List<Node> route = new List<Node>();

    void Start()
    {
        for (int i = 0; i < nodeCount; ++i)
        {
            Node node = Instantiate(nodeTemplate);
            Vector3 position = Random.onUnitSphere * radius;
            position.y = 0f;
            node.transform.position = position;
            node.transform.rotation = Quaternion.identity;
            node.name = string.Format("Node{0}", i);
            node.SetColour(Color.red);

            nodes.Add(node);
        }

        StartCoroutine(FindRoute());
    }

    IEnumerator FindRoute()
    {
        Node top = null;
        foreach (Node node in nodes)
        {
            node.SetColour(Color.yellow);
            yield return null;
            if (top == null || node.IsHigherThan(top))
            {
                if (top != null)
                {
                    top.SetColour(Color.red);
                }
                top = node;
                top.SetColour(Color.green);
            }
            else
            {
                node.SetColour(Color.red);
            }
        }

        if (nodes.Count == 0)
        {
            // done
        }
        else if (nodes.Count == 1)
        {
            route.Add(nodes[0]);
            // done
        }
        else 
        {
            Vector3 previous = top.transform.position;
            previous.x -= 100f;
            Node next = null;
            Node current = top;
            while (next != top && nodes.Count > 1)
            {
                next = null;
                foreach (Node node in nodes)
                {
                    if (node != current)
                    {
                        node.SetColour(Color.yellow);
                        yield return null;
                    }

                    if (next == null && node != current)
                    {
                        next = node;
                    }
                    else if (next != null && Vector3.Angle(node.transform.position - current.transform.position, current.transform.position - previous) < Vector3.Angle(next.transform.position - current.transform.position, current.transform.position - previous))
                    {
                        next.SetColour(Color.red);
                        next = node;
                    }

                    if (node != current)
                    {
                        node.SetColour(Color.red);
                    }
                    
                    if (next != null)
                    {
                        next.SetColour(Color.green);
                    }
                }

                route.Add(next);
                nodes.Remove(next);

                previous = current.transform.position;
                current = next;
            }

            if (route.Count > 1)
            {
                Route newRoute = Instantiate(routeTemplate);
                newRoute.SetRoute(route);
                route.Clear();
            }

            if (nodes.Count > 1)
            {
                yield return StartCoroutine(FindRoute());
            }
        }
    }
}
