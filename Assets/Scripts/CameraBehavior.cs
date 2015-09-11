using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    GameObject player;
    Vector3 Pivot;


    bool shouldRotate = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        Pivot = player.transform.position;


    }

    // Update is called once per frame
    void Update () {
       


        //transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - 10.0f);

   



    }

    public void RotateCamera(float angle) {
       transform.RotateAround(player.transform.position, Vector3.Cross((player.transform.position - transform.position), transform.right), angle);
       transform.LookAt(player.transform.position);
    }

}
