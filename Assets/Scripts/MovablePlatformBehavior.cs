using UnityEngine;
using System.Collections;

public class MovablePlatformBehavior : MonoBehaviour {

    bool shouldWeighPlayer;
    public float mMinimumY;

    GameObject player;
    Vector3 velocity;
    Rigidbody rBody;
    Vector3 prevPos;
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rBody = GetComponent<Rigidbody>();
        prevPos = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!shouldWeighPlayer)
        {
            rBody.velocity = Vector3.zero;
        }


	}

    // Henrique - Note: This should be Collision but when corrected platform only stops fallig if player jumps
    public void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shouldWeighPlayer = true;
        }

    }

    public void OnCollisionExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shouldWeighPlayer = false;
        }

    }

}
