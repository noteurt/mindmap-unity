using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;

public class MindMapGenerator : MonoBehaviour
{
    //public TextAsset xmlData;
    public GameObject nodePrefab;
    public float nodeSpacing = 2.0f;
    public float connectionWidth = 0.1f;
    public Material connectionMaterial;



    private class NodeData
    {
        public string content;
        public List<NodeData> children = new List<NodeData>();
    }

    string xmlString = "<map><node TEXT=\"ChatGPT Unity Integration\"><node TEXT=\"Brainstorm Assistant\"><node TEXT=\"Summarize Ideas\"></node><node TEXT=\"MindMap\"></node><node TEXT=\"Keyword finder\" ></node></node><node TEXT=\"NPCs\"></node><node TEXT=\"Unity Assistant\" ></node><node TEXT=\"Story Teller\"></node></node></map>";


    void Start()
    {
        NodeData rootNode = ParseXML(xmlString);
        GenerateMindMap(rootNode, transform.position, 0);
    }

    NodeData ParseXML(string xmlContent)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        XmlNode rootNodeXML = xmlDoc.SelectSingleNode("//map/node");
        return ParseNode(rootNodeXML);
    }

    NodeData ParseNode(XmlNode xmlNode)
    {
        NodeData nodeData = new NodeData { content = xmlNode.Attributes["TEXT"].Value };

        XmlNodeList childNodes = xmlNode.SelectNodes("node");
        foreach (XmlNode childNode in childNodes)
        {
            nodeData.children.Add(ParseNode(childNode));
        }

        return nodeData;
    }

    void GenerateMindMap(NodeData nodeData, Vector3 position, int depth)
    {
        GameObject node = Instantiate(nodePrefab, position, Quaternion.identity);
        CubeText spawenedCubeScript = node.GetComponent<CubeText>();
        spawenedCubeScript.text =  nodeData.content;

        //node.transform.GetChild(0).GetComponent<TextMeshPro>().text = nodeData.content;

        Vector3 childPosition = position + new Vector3(nodeSpacing, 0, 0);
        int numChildren = nodeData.children.Count;

        for (int i = 0; i < numChildren; i++)
        {
            NodeData childNodeData = nodeData.children[i];
            Vector3 offset = new Vector3(0, -(nodeSpacing * i) + (nodeSpacing * (numChildren - 1) / 2.0f), -nodeSpacing * depth);
            GenerateMindMap(childNodeData, childPosition + offset, depth + 1);
            DrawConnection(position, childPosition + offset);
        }
    }

    void DrawConnection(Vector3 start, Vector3 end)
    {
        GameObject connection = new GameObject("Connection");
        connection.transform.SetParent(transform);
        LineRenderer lineRenderer = connection.AddComponent<LineRenderer>();
        lineRenderer.material = connectionMaterial;
        lineRenderer.startWidth = connectionWidth;
        lineRenderer.endWidth = connectionWidth;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
