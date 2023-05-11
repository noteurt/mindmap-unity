using UnityEngine;

public class MindMapManager : MonoBehaviour
{
    public GameObject centerNodePrefab;
    public GameObject nodePrefab;
    public int maxDepth;

    private void Start()
    {
        GameObject centerNode = Instantiate(centerNodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GenerateMindMap(centerNode, "Node-", 1, maxDepth, true);
    }

void GenerateMindMap(GameObject parentNode, string prefix, int depth, int maxDepth, bool growRight)
{
    if (depth > maxDepth) return;

    float baseHorizontalSpacing = 1.5f;
    float verticalSpacing = 1.0f;

    // Calculate child node position
    Vector3 parentPosition = parentNode.transform.position;

    // Determine the number of branches at this depth level
    int numBranches = Mathf.FloorToInt(Mathf.Pow(2, depth - 1));

    // Calculate the total horizontal span required for the branches
    float totalHorizontalSpan = baseHorizontalSpacing * (numBranches - 1);

    // Calculate the initial position of the leftmost child node
    Vector3 initialChildPosition = parentPosition + new Vector3(-totalHorizontalSpan / 2f, (growRight ? -1 : 1) * verticalSpacing, 0);

    // Generate child nodes
    for (int i = 0; i < numBranches; i++)
    {
        // Calculate the position of each child node based on the index and spacing
        Vector3 childPosition = initialChildPosition + new Vector3(i * baseHorizontalSpacing, 0, 0);

        // Create child node
        GameObject childNode = Instantiate(nodePrefab, childPosition, Quaternion.identity);
        childNode.transform.SetParent(parentNode.transform);
        CubeText childNodeScript = childNode.GetComponent<CubeText>();
        childNodeScript.text = $"{prefix}{depth}-{i}";

        // Generate child nodes recursively
        GenerateMindMap(childNode, $"{prefix}{depth}-{i}", depth + 1, maxDepth, !growRight);
    }
}




}
