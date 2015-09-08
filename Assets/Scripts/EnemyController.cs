using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int rotationSpeed = 3;
    Transform myTransform;
    Transform plyrTransform;
    Vector3 lookDir;

    // Use this for initialization
	void Start ()
    {
        // Reference owen transform
        myTransform = this.transform;
        
        //Find Player Transform
        plyrTransform = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Calculates the Look at position.
        lookDir = plyrTransform.position - myTransform.position;

        // Sets Look dir y to zero so that enemy does not tumble toward player.
        lookDir.y = 0;

        // Draws Line from enemy position to the player.
        Debug.DrawLine(myTransform.position, plyrTransform.position, Color.green);

        //Rotate towards the Look At position
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);



	}
}
