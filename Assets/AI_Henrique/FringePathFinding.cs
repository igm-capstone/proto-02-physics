using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Grid))]
public class FringePathFinding : MonoBehaviour {
    // Class Variables
    // Start(Seeker) and End(target) points
    public Transform seeker, target;

    // Reference the node grid.
    Grid nodeGrid;

    // A* cost variables
    public int DiagCost = 14;               // Default = 14 because square root of 2 *10
    public int StraightCost = 10;           // Default = 10 because I assume a 1x1 square for node

    void Awake()
    {
        nodeGrid = GetComponent<Grid>();
    }

    void Update()
    {
        // Calls the Pathfinding function every frame
        FindPath(seeker.position, target.position);
    }

    // Finds path between the two positions
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Converts World Position into Grid Coordinates
        Node startNode = nodeGrid.GetNodeFromWorld(startPos);
        Node targetNode = nodeGrid.GetNodeFromWorld(targetPos);

        // Lists for Fringe Search
        LinkedList<Node> fringeList = new LinkedList<Node>();   // Fringe List
        Dictionary<Node, Node> cache = new Dictionary<Node, Node>();          // Chosen nodes for the path that have being looked at.

        // Adds starting Node to list
        fringeList.AddFirst(startNode);

        cache[startNode] = null;

        int fLimit = GetNodeDistance(startNode, targetNode);

        bool found = false;

        while (!found && fringeList.Count > 0 )
        {
            int fmin = int.MaxValue;

            for (var linkedNode = fringeList.First ; linkedNode != null;)
            {
                Node node = linkedNode.Value;

                Node parent = cache[node];
                int  g = parent != null ? parent.gCost: 0;

                int  f = g + GetNodeDistance(node, targetNode);

                if (f > fLimit)
                {
                    fmin = Mathf.Min(f, fmin);
                    continue;
                }

                if (node == targetNode)
                {
                    found = true;
                    break;
                }

                // Get node children
                List<Node> children = nodeGrid.GetNodeNeighbours(startNode);
                children.Reverse();      // reverse to read right to left

                foreach (Node child in children)
                {
                    int g_child = g + GetNodeDistance(node, child);
                    if (cache[child] != null)
                    {

                        parent = cache[child];
                        //g_chached = parent.gCost;

                        // assossiate aprent so we can retrace the path alter, probably wrong
                        parent.ParentNode = child;

                        if (g_child >= parent.gCost)
                            continue;
                    }

                    if (fringeList.Contains(child))
                        fringeList.Remove(child);

                    fringeList.AddAfter(fringeList.Find(child), node);

                    cache[child] = node;
                    cache[child].gCost = g_child;
                }
                fringeList.Remove(node);
            }
            fLimit = fmin;
        }
    }

    // Returns the path as an ordered list.
    void retraceNodePath (Node startNode, Node endNode)
    {
        List<Node> retracedPath = new List<Node>();
        Node currentNode = endNode;

        // Go from end node to start node by going looping through the node's parent
        while(currentNode != startNode)
        {
            retracedPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }
        // Path gets retraced in reverse inside loop so un-reverse it.
        retracedPath.Reverse();

        // Draw Path using gizmos
        nodeGrid.drawnPath = retracedPath;
        
    }

    // Get Distance between two different Nodes
    int GetNodeDistance (Node nodeA, Node nodeB)
    {
        // Calculates Absulotue distance
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        // Check to see which axis-distance is shorter
        // Equation = Distance = DiagCost*ShortestDist + StraightCos*(LongestDist - ShortestDist)
        if (distX > distY)
            return DiagCost * distY + StraightCost * (distX - distY);
        // Else
        return DiagCost * distX + StraightCost * (distY - distX);
    }
}
