using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class AStarPath : MonoBehaviour 
{
    
    //Node_K sourceNode, targetNode;
    AIArea AIarea;
    public LayerMask AILayer;
    public Transform source, target;
    private List<Node_K> path = new List<Node_K>();
    public string time = "";

    void Start()
    {
        AIarea = GetComponent<AIArea>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            aStar(source.position, target.position);
    }

    public void aStar(Vector3 startPos, Vector3 endPos)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        //List<Node_K> openSet = new List<Node_K>();
        Heap<Node_K> openSet = new Heap<Node_K>(AIarea.GetMaxSize);
        HashSet<Node_K> closedSet = new HashSet<Node_K>();
        Node_K sourceNode = AIarea.getNodeAtPos(startPos);
        Node_K targetNode = AIarea.getNodeAtPos(endPos);
        openSet.Add(sourceNode);        

        while(openSet.Count > 0)
        {
            Node_K currentNode = openSet.RemoveFirst();
            //Node_K currentNode = openSet[0];
            //for (int i = 1; i < openSet.Count; i++)
            //{
            //    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
            //    {
            //        currentNode = openSet[i];
            //    }
            //}

            //openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                reversePath(sourceNode, targetNode);
                watch.Stop();
                time = watch.ElapsedMilliseconds.ToString();
                print("Astar " + watch.ElapsedMilliseconds + "ms");
                return;
            }

            List<Node_K> neighbors = new List<Node_K>();
            neighbors = AIarea.neighbors(currentNode);
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (neighbors[i].walkable == false || closedSet.Contains(neighbors[i]))
                    continue;

                int cost = currentNode.gCost + moveCost(currentNode, neighbors[i]);
                if (!openSet.Contains(neighbors[i]) || (cost < neighbors[i].gCost))
                {
                    neighbors[i].gCost = cost;
                    neighbors[i].hCost = moveCost(neighbors[i], targetNode);
                    neighbors[i].parent = currentNode;

                    if (!openSet.Contains(neighbors[i]))
                        openSet.Add(neighbors[i]);
                    else
                        openSet.UpdateItem(neighbors[i]);
                }
            }
        }
    }

    void reversePath(Node_K source, Node_K target)
    {
        List<Node_K> revPath = new List<Node_K>();
        Node_K node = target;
        while(node != source)
        {
            revPath.Add(node);
            node = node.parent;
        }
        revPath.Reverse();

        GameObject spherePath = GameObject.Find("SpherePathAStar");
        if (spherePath.transform.FindChild("sphere"))
        {
            foreach (Transform child in spherePath.transform)
                Destroy(child.gameObject);
        }

        foreach (Node_K n in revPath)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.layer = LayerMask.NameToLayer("red");
            sphere.transform.parent = spherePath.transform;
            sphere.GetComponent<Renderer>().material.color = Color.red;
            sphere.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            sphere.transform.position = n.myPos;
        }
        AIarea.aStarPath = revPath;
    }

    int moveCost(Node_K source, Node_K target)
    {
        float zCost = Mathf.Abs(target.myCoord.y - source.myCoord.y);
        float xCost = Mathf.Abs(target.myCoord.x - source.myCoord.x);

        if (xCost > zCost)
            return Mathf.RoundToInt((14 * zCost) + 10*(xCost - zCost));
        else
            return Mathf.RoundToInt((14 * xCost) + 10 * (zCost - xCost));
    }
}
