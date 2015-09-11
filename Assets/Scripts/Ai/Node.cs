using UnityEngine;
using System.Collections;

public class Node {

    // Class Variables
    public bool isWalkable;
    public Vector3 worldPos;

    // Constructor:
    public Node(bool _isWalkable, Vector3 _worldPos)
    {
        isWalkable = _isWalkable;
        worldPos = _worldPos;
    }

}
