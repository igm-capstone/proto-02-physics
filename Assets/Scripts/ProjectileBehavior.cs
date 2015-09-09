using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {

    // Time to self destruct
    public float projDestroyTime = 3.0f;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(KillSelf());
	}
	

    // Kills itself after some time.
    IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(projDestroyTime);
        //GameObject.Destroy(this);
        Destroy(this.gameObject);
    }
}
