using UnityEngine;
using System.Collections;

public class PhysicsManager : MonoBehaviour 
{
    [SerializeField]
    private float changeValue;
	void Start () {
	
	}
	void Update () {
	
	}

    //access the mass from rigidbody
    void changeMass()
    {
        //if trigger pressed
        gameObject.GetComponent<Rigidbody>().mass += changeValue;

        //if trigger released
        gameObject.GetComponent<Rigidbody>().mass -= changeValue;
    }
    
    //access physics mat
    void 
}
