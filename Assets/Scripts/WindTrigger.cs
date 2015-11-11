using UnityEngine;
using System.Collections;

public class WindTrigger : MonoBehaviour
{
    public Vector3 force = new Vector3(0, 0.3f, 0);

    public void OnTriggerStay(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        var height = (other.transform.position.y - transform.position.y) / 2;

        if (height > 1)
        {
            height = 1 + ((height - 1) / 5f);
        }

        rb.AddForce(force * 1 / Mathf.Abs(height), ForceMode.Force);
    }

}
