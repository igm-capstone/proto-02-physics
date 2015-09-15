using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class FringePath : MonoBehaviour 
{
    //Node_K sourceNode, targetNode;
    AIArea AIarea;
    public LayerMask AILayer;
    public Transform source, target;
    private List<Node_K> path = new List<Node_K>();

    void Start()
    {
        AIarea = GetComponent<AIArea>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fringePath(source.position, target.position);
            //GameObject[] spheres = new GameObject[AIarea.GetMaxSize];
            GameObject path = GameObject.Find("PathSphere");

        }
    }

    public void fringePath(Vector3 startPos, Vector3 endPos)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        LinkedList<Node_K> fringeList = new LinkedList<Node_K>();
        //Dictionary<Node_K, Node_K> cache = new Dictionary<Node_K, Node_K>();
        Dictionary<Node_K, int> cache = new Dictionary<Node_K, int>();
        Node_K sourceNode = AIarea.getNodeAtPos(startPos);
        Node_K targetNode = AIarea.getNodeAtPos(endPos);

        
        fringeList.AddFirst(sourceNode);
        cache.Add(sourceNode, sourceNode.gCost);

        int fLimit = moveCost(sourceNode, targetNode);
        bool found = false;

        while (!found && fringeList.Count > 0 )
        {
            int fMin = int.MaxValue;

            for (var linkedNode = fringeList.First ; linkedNode != null;)
            {
                Node_K fringeNode = linkedNode.Value;
                int fCostFringeNode = fringeNode.fCost + moveCost(fringeNode, targetNode);

                if (fCostFringeNode > fLimit)
                {
                    fMin = Mathf.Min(fCostFringeNode, fMin);
                    linkedNode = linkedNode.Next;
                    continue;
                }

                if(fringeNode == targetNode)
                {
                    found = true;
                    createPath(targetNode);
                    watch.Stop();
                    print("fringe took " + watch.ElapsedMilliseconds + "ms");
                    break;
                }

                // Get node children
                List<Node_K> fringeNodeChildren = AIarea.neighbors(fringeNode);
                fringeNodeChildren.Reverse();

                foreach (Node_K childNode in fringeNodeChildren)
                {
                    //if (childNode.walkable)
                    //{
                        int childNodeGCost = fringeNode.gCost + moveCost(fringeNode, childNode) * childNode.nodeCost;
                        if (cache.ContainsKey(childNode))
                        {
                            if (childNodeGCost >= childNode.gCost)
                                continue;
                        }

                        if (fringeList.Contains(childNode))
                            fringeList.Remove(childNode);

                        fringeList.AddAfter(linkedNode, childNode);

                        childNode.parent = fringeNode;
                        childNode.gCost = childNodeGCost;

                        cache[childNode] = childNode.gCost;
                    //}
                }

                var lastNode = linkedNode;
                linkedNode = lastNode.Next;
                fringeList.Remove(lastNode);
            }
            fLimit = fMin;
        }
    }

    void createPath(Node_K target)
    {
        LinkedList<Node_K> path = new LinkedList<Node_K>();
        Node_K node = target;
        while(node != null)
        {
            path.AddFirst(node);
            node = node.parent;
        }
        AIarea.fringePath = path;

        foreach (Node_K n in path)
        {            
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = GameObject.Find("PathSphere").transform;
            sphere.GetComponent<Renderer>().material.color = Color.black;
            sphere.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            sphere.transform.position = n.myPos;
        }
    }

    int moveCost(Node_K source, Node_K target)
    {
        float zCost = Mathf.Abs(target.myCoord.y - source.myCoord.y);
        float xCost = Mathf.Abs(target.myCoord.x - source.myCoord.x);

        if (xCost > zCost)
            return Mathf.RoundToInt((14 * zCost) + 10 * (xCost - zCost));
        else
            return Mathf.RoundToInt((14 * xCost) + 10 * (zCost - xCost));
    }
}