using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLayout : MonoBehaviour
{
    public ConnectionsManagerScript connectionsManager;
    public float repulsionForce = 1.0f;
    public float springForce = 0.1f;
    public float springLength = 1.0f;
    public float deltaTime = 0.01f;
    public int iterations = 1000;

    public void ApplyForceDirectedLayout(List<GameObject> nodes, List<(int, int)> connections)
    {
        StartCoroutine(LayoutCoroutine(nodes, connections));
    }

    private void UpdateConnections(List<GameObject> nodes, List<(int, int)> connections)
    {
        foreach (var (i, j) in connections)
        {
            GameObject connection = connectionsManager.GetConnection(i, j);
            LineRenderer lineRenderer = connection.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, nodes[i].transform.position);
            lineRenderer.SetPosition(1, nodes[j].transform.position);
        }
    }


    private IEnumerator LayoutCoroutine(List<GameObject> nodes, List<(int, int)> connections)
    {
        for (int k = 0; k < iterations; k++)
        {
            // Apply repulsion force
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    Vector3 delta = nodes[i].transform.position - nodes[j].transform.position;
                    float distance = delta.magnitude;
                    if (distance > 0)
                    {
                        Vector3 force = delta.normalized * repulsionForce / (distance * distance);
                        nodes[i].transform.position += force * deltaTime;
                        nodes[j].transform.position -= force * deltaTime;
                    }
                }
            }

            // Apply spring force
            foreach (var (i, j) in connections)
            {
                Vector3 delta = nodes[i].transform.position - nodes[j].transform.position;
                float distance = delta.magnitude;
                Vector3 force = delta.normalized * (distance - springLength) * springForce;
                nodes[i].transform.position -= force * deltaTime;
                nodes[j].transform.position += force * deltaTime;
            }

            UpdateConnections(nodes, connections);

            yield return null;
        }
    }
}
