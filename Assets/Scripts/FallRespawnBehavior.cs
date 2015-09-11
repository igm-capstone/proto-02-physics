using UnityEngine;
using System.Collections;

public class FallRespawnBehavior : MonoBehaviour {

    GameObject[] checkpoints;
    Vector3 respawnPosition;

	// Use this for initialization
	void Start () {
        respawnPosition = gameObject.transform.position;
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
	}
	
	// Update is called once per frame
	void Update () {
        


        if (gameObject.transform.position.y < -20.0f) {

            Vector3 closestPosition = respawnPosition;
            float minDistance = Vector3.Distance(transform.position, respawnPosition);
            foreach (GameObject checkpoint in checkpoints) {
                if (checkpoint.transform.position.magnitude < transform.position.magnitude) {
                    var distance = Vector3.Distance(transform.position, checkpoint.transform.position);
                    if (distance < minDistance)
                    {
                        closestPosition = checkpoint.transform.position;
                        minDistance = distance; 
                    }
                }
            }
            gameObject.transform.position = closestPosition;
        }

	}
}
