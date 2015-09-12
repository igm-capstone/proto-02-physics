using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    GameObject player;
    public Vector3 PlayerOffset;

    public int state = 0;
    bool shouldRotate = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
    }

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + PlayerOffset;
    }

    public void RotateCamera(float angle) {
    //   transform.RotateAround(player.transform.position, Vector3.Cross((player.transform.position - transform.position), transform.right), angle);
     //  transform.LookAt(player.transform.position);
    }

}
