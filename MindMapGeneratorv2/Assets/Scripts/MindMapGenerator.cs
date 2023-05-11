using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindMapGenerator : MonoBehaviour
{
    public ConnectionsManagerScript connectionsManager;
    public GraphLayout graphLayout;
    public int maxDepth = 3;
    public int maxBranches = 3;

    private List<GameObject> nodes = new List<GameObject>();
    private List<(int, int)> connections = new List<(int, int)>();

    void Start()
    {
        // Create the root node
        GameObject rootNode = connectionsManager.AddNode(Vector3.zero, "Center");
        nodes.Add(rootNode);

        // Generate the mind map recursively
        GenerateMindMap(0, "Node", 1, maxDepth);

        // Apply force-directed layout
        //graphLayout.ApplyForceDirectedLayout(nodes, connections);
    }

void GenerateMindMap(int parentIndex, string prefix, int depth, int maxDepth)
{
    if (depth > maxDepth) return;

    float horizontalSpacing = 0.5f;
    float verticalSpacing = 0.1f;

    for (int i = 0; i < maxBranches; i++)
    {
        // Calculate child node position
        Vector3 parentPosition = nodes[parentIndex].transform.position;
        Vector3 childPosition;

        if (parentIndex == 0)
        {
            childPosition = new Vector3(parentPosition.x + (i % 2 == 0 ? -1 : 1) * horizontalSpacing, parentPosition.y - verticalSpacing, parentPosition.z);
        }
        else
        {
            int direction = nodes[0].transform.position.x < nodes[parentIndex].transform.position.x ? 1 : -1;
            childPosition = parentPosition + new Vector3(direction * horizontalSpacing, -verticalSpacing, 0);
        }

        // Create child node
        GameObject childNode = connectionsManager.AddNode(childPosition, $"{prefix}{depth}-{i}");
        nodes.Add(childNode);

        // Connect child node to parent node
        int childIndex = nodes.Count - 1;
        connections.Add((parentIndex, childIndex));
        connectionsManager.CreateConnection(nodes[parentIndex], childNode, parentIndex, childIndex);

        // Generate child nodes
        GenerateMindMap(childIndex, $"{prefix}{depth}-{i}", depth + 1, maxDepth);
    }
}







}
