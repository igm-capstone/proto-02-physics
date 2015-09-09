using UnityEngine;
using System.Collections;

public class PhysicsManager : MonoBehaviour 
{
    [SerializeField]
    private float changeValue;
    [SerializeField]
    private float minMass;
    [SerializeField]
    private float maxMass;
    private float currentMass;
    private PhysicMaterial playerPhyscMat;

	void Start () {
	    currentMass = gameObject.GetComponent<Rigidbody>().mass;
        playerPhyscMat = gameObject.GetComponent<BoxCollider>().material;
	}
	void Update () 
    {

    }

    //access the mass from rigidbody
    public void changeMass(float triggerValue)
    {
        gameObject.GetComponent<Rigidbody>().mass = Mathf.Lerp(minMass, maxMass, triggerValue);
    }
    
    //access physics mat
    public void changeBounciness(float triggerValue)
    {
        playerPhyscMat.bounciness = triggerValue;
    }
}
