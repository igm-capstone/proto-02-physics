using UnityEngine;
using System.Collections;

public class ForceTunnelBehavior : MonoBehaviour
{

    public Vector3 direction;
    public float magnitude;
    GameObject player;
    bool isActive;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            player.GetComponent<Rigidbody>().AddForce(direction * magnitude);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        isActive = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isActive = false;
    }
}