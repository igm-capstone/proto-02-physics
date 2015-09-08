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
        float horizontal, vertical;
        bool jump;
        ReadPlayerInput(out horizontal, out vertical, out jump);
        actor.PerformActions(horizontal, vertical, jump);
    }

    private void ReadPlayerInput(out float horizontal, out float vertical, out bool jump)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);
        jump = Input.GetButton("Jump_P" + playerId);
    }
}
