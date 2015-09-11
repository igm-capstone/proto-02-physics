using UnityEngine;
using System.Collections;

public class PhysicsManager : MonoBehaviour 
{
    // Mass
    [SerializeField]
    private float minMass;
    [SerializeField]
    private float maxMass;
    private float currentMass;

    Material plyrMaterial;
    [SerializeField]
    Color LightColor;
    [SerializeField]
    Color HeavyColor;

    // Bouncyness
    private PhysicMaterial playerPhyscMat;

	void Start ()
    {
	    currentMass = gameObject.GetComponent<Rigidbody>().mass;
        playerPhyscMat = gameObject.GetComponent<BoxCollider>().material;

        plyrMaterial = gameObject.GetComponent<Renderer>().material;

        // Starting Color
        plyrMaterial.color = LightColor;

    }

    //access the mass from rigidbody
    public void changeMass(float triggerValue)
    {
        // Change Player Mass
        gameObject.GetComponent<Rigidbody>().mass = Mathf.Lerp(minMass, maxMass, triggerValue);
        // Darken player as mass changes.
        plyrMaterial.color = Color.Lerp(LightColor, HeavyColor, triggerValue);
    }
    
    //access physics mat
    public void changeBounciness(float triggerValue)
    {
        playerPhyscMat.bounciness = triggerValue;

    }
}
