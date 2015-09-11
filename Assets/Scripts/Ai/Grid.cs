using UnityEngine;
using System.Collections;

// Grid of nodes used by A*
public class Grid : MonoBehaviour {

    public Transform Player;


    public LayerMask unwalkableMask;        // Reference to which mask is unwalkable
    public Vector2 gridWorldSize;           // Size of Grid
    int nodesInX, nodesInY;                 // Total Number of nodes in each axis of the grid
    public float nodeRad;                   // Radius of a single node
    float nodeDiameter;                     // Diameter of a single node.

    Node[,] grid;                           // 2D Node Array, i.e. the actual Grid

    

    void Start()
    {
        nodeDiameter = nodeRad * 2;
        // No half-nodes therefore round to int.
        nodesInX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        nodesInY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        //Creates the Node Grid
        CreateGrid();
    }


    // Creates a Grid of nodes. Node size and radius are set up in the editor.
    void CreateGrid()
    {
        grid = new Node[nodesInX, nodesInX];
        // Calculates the world position of the botton left of the grid (position 0,0)
        Vector3 gridBotLeftWorldPos = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y/2;

        // is Created Node Walkable?
        bool isWalkable;

        for (int x = 0; x < nodesInX; x++)
        {
            for (int y = 0; y < nodesInY; y++)
            {                           // Bot left position +                  Center of node position for x                Center of node position for y (z on world)
                Vector3 nodeWorldPos = gridBotLeftWorldPos + Vector3.right * (x * nodeDiameter + nodeRad) + Vector3.forward * (y * nodeDiameter + nodeRad);
                // Test for colision on node point. if no colision node  is walkable. Walkability determined by a Layer Maks.
                isWalkable = !(Physics.CheckSphere(nodeWorldPos, nodeRad, unwalkableMask));
                // Creates node 
                grid[x, y] = new Node(isWalkable, nodeWorldPos);
            }
        }
    }


    // Takes a pposition in the world and return the grid's X,Y coordinates of the node in that position
    public Node GetNodeFromWorld(Vector3 worldPosition)
    {

        // Transform World position into a percentage into the node.
        float percentX = (worldPosition.x / gridWorldSize.x) + 0.5f ;
        float percentY = (worldPosition.z / gridWorldSize.y) + 0.5f;    // Y on grid => Z on world

        // Clamp percentual value between 0 and 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Get node coordinates by the percentage of the position is inside the node.
        int gridCoordX = Mathf.RoundToInt((nodesInX - 1) * percentX);
        int gridCoordY = Mathf.RoundToInt((nodesInY - 1) * percentY);

        return grid[gridCoordX, gridCoordY];
    }

    // Draw Visualization gizmos.
    void OnDrawGizmos()
    {
        // Draw the whole grid in unity editor. Top down view meas y size is represented in z axis iun 3D space.
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        // Draw all nodes:
        if (grid != null)
        {
            // Get node player is standing on.
            Node playerNode = GetNodeFromWorld(Player.position);

            foreach (Node node in grid)
            {
                // Change Gizmo color if node is walkable
                Gizmos.color = (node.isWalkable) ? Color.white : Color.red;
                // Color player node
                if (playerNode == node) Gizmos.color = Color.yellow;

                // Draw cube at node position with size Diameter - 0.1f
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }

    }
   
}
