using UnityEngine;
using System.Collections;

public class PhysicsManager : MonoBehaviour 
{
    [SerializeField]
    private float changeValue;
    private float minMass;
    private float maxMass;
    private float currentMass;
    private PhysicMaterial playerPhyscMat;

	void Start () {
	    currentMass = gameObject.GetComponent<Rigidbody>().mass;
        playerPhyscMat = gameObject.GetComponent<BoxCollider>().material;
	}
	void Update () {
	
	}

    //access the mass from rigidbody
    void changeMass(float triggerValue)
    {
        float prvFrameTriggerValue = triggerValue;

        //if trigger pressed
        if(triggerValue >= prvFrameTriggerValue)
        {
            if(currentMass < maxMass)
            {
                currentMass += changeValue * triggerValue * Time.deltaTime;
            }
            else
            {
                currentMass -= changeValue * triggerValue * Time.deltaTime;
            }
        }
    }
    
    //access physics mat
    void changeBounciness(float triggerValue)
    {
        playerPhyscMat.bounciness *= triggerValue * Time.deltaTime;
    }
}
