using UnityEngine;
using System.Collections;

public class CameraTriggerBehavior : MonoBehaviour {

    public int CameraState;
    public Vector3 CameraOffsetPosition;
    public Vector3 CameraStartRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
            camObject.transform.rotation = Quaternion.Euler(CameraStartRotation);
            camObject.GetComponent<CameraBehavior>().PlayerOffset = CameraOffsetPosition;
        }

    }

}
