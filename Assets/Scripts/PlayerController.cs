using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
public class PlayerController : MonoBehaviour 
{

    [SerializeField]
    [Range(1f, 20f)]
    public float speed = 5;

    public Vector3 PlyrStartPos;

    [SerializeField]
    [Range(1, 2)]
    short playerId = 1;
    int availablePlaybacks = 0;

    ActorBehaviour actor;

    Vector3 currentVelocity;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        actor.setSpeed(speed);
    }

    public void Update()
    {
        float horizontal, vertical, leftTrigger, rightTrigger;
        bool jump;
        ReadPlayerInput(out horizontal, out vertical, out leftTrigger, out rightTrigger, out jump);
        actor.PerformActions(horizontal, vertical, jump);
    }

    private void ReadPlayerInput(out float horizontal, out float vertical, out float leftTrigger, out float rightTrigger, out bool jump)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);
        leftTrigger = Input.GetAxis("Left_Trigger_P" + playerId);
        rightTrigger = Input.GetAxis("Right_Trigger_P" + playerId);
        jump = Input.GetButton("Jump_P" + playerId);
    }
}
