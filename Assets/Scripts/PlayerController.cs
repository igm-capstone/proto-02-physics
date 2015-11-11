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
    PlayerAnimationBehavior animator;
    PhysicsManager physicsmg;
    GameObject mCamera;

    Vector3 currentVelocity;
    bool shouldRotateCamera = false;

    Collider camTrigger;

    public void Awake()
    {
        animator = GetComponent<PlayerAnimationBehavior>();
        actor = GetComponent<ActorBehaviour>();
        actor.setSpeed(speed);
        physicsmg = GetComponent<PhysicsManager>();
    }

    public void Start()
    {
        mCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void Update()
    {
        float horizontal, vertical, leftTrigger, rightTrigger;
        bool jump;
        ReadPlayerInput(out horizontal, out vertical, out leftTrigger, out rightTrigger, out jump);
        actor.PerformActions(horizontal, vertical, jump);
        animator.Scale(rightTrigger);
        physicsmg.changeMass(rightTrigger);
        physicsmg.changeBounciness(leftTrigger);
    }

    private void ReadPlayerInput(out float horizontal, out float vertical, out float leftTrigger, out float rightTrigger, out bool jump)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);
        leftTrigger = Input.GetAxis("Left_Trigger_P" + playerId);
        rightTrigger = Input.GetAxis("Right_Trigger_P" + playerId);
        jump = Input.GetButton("Jump_P" + playerId);
    }

    void OnTriggerEnter(Collider other)
    {
        shouldRotateCamera = true;
        camTrigger = other;
    }

    void OnTriggerExit(Collider other)
    {
        camTrigger = null;
        shouldRotateCamera = false;
    }

    public void OnTriggerStay(Collider other)
    {
        float minZ = camTrigger.gameObject.transform.position.z - camTrigger.bounds.extents.z;
        float boxZPercentage = (transform.position.z - minZ) / (camTrigger.bounds.extents.z * 2.0f);
        mCamera.GetComponent<CameraBehavior>().RotateCamera(90.0f * boxZPercentage);
    }
}
