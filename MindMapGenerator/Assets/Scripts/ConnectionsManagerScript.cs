using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectionsManagerScript : MonoBehaviour
{
    public GameObject nodePrefab;
    public Material connectionMaterial;
    public float connectionWidth = 0.1f;

    private Dictionary<(int, int), GameObject> connections = new Dictionary<(int, int), GameObject>();

    public GameObject AddNode(Vector3 position, string word)
    {
        GameObject newNode = Instantiate(nodePrefab, position, Quaternion.identity);
        newNode.transform.SetParent(transform);
        CubeText newNodeScript = newNode.GetComponent<CubeText>();
        newNodeScript.text = word;

        return newNode;
    }

    public GameObject CreateConnection(GameObject fromNode, GameObject toNode, int fromIndex, int toIndex)
    {
        GameObject connection = new GameObject("Connection");
        connection.transform.SetParent(transform);
        LineRenderer lineRenderer = connection.AddComponent<LineRenderer>();
        lineRenderer.material = connectionMaterial;
        lineRenderer.startWidth = connectionWidth;
        lineRenderer.endWidth = connectionWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, fromNode.transform.position);
        lineRenderer.SetPosition(1, toNode.transform.position);

        connections.Add((fromIndex, toIndex), connection);

        return connection;
    }

    public GameObject GetConnection(int fromIndex, int toIndex)
    {
        return connections.TryGetValue((fromIndex, toIndex), out var connection) ? connection : null;
    }
}
