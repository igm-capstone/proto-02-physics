using UnityEngine;
using System;

[Flags]
public enum Actions
{
    None = 0,

    MoveRight = (1 << 0),
    MoveLeft = (1 << 1),
    StopHorizontal = (1 << 2),

    MoveUp = (1 << 3),
    MoveDown = (1 << 4),
    StopVertical = (1 << 5),
}

public class ActorBehaviour : MonoBehaviour
{
    float speed = 5;

    [SerializeField]
    float jumpForce = 20;

    [SerializeField]
    LayerMask groundMask;

    public new Rigidbody rigidbody { get; private set; }

    Vector3 velocity;
    
    //impulse available for the current jump
    float jumpImpulse;

    bool isJumping;
    public bool isGrounded { get { return groundTicks > 0; } }
    bool isHittingOther { get { return hittingOtherTicks > 0; } }
    private Vector3 otherNormal;

    private int groundTicks;
    private int hittingOtherTicks;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rigidbody.AddForce(Vector3.up * (jumpImpulse * .2f), ForceMode.VelocityChange);
            jumpImpulse *= .8f;
        }

        if (isGrounded)
        {
            jumpImpulse = jumpForce;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        groundTicks = Mathf.Min(0, groundTicks - 1);
        hittingOtherTicks = Mathf.Min(0, hittingOtherTicks - 1);

    }

    public void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundMask) > 0)
        {

            // is only grounded if touched the ground from the top (positive normal y component)
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0)
                {
                    groundTicks = 3;
                    //break;
                }

                if (contact.normal.z != 0 || contact.normal.x != 0)
                {
                    otherNormal = contact.normal;
                    // Hit something else that is not the Ground, like a Wall or an Enemy.
                    hittingOtherTicks = 3;
                    //break;
                }

                if (isHittingOther || isGrounded) {
                    Debug.Log("ENTER PLAt");
                }
            }
        }
    }
    //public void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.tag == "Platform")
    //    {
    //        Debug.Log("EXIT PLAT");
    //        isGrounded = false;
    //        isHittingOther = false;
    //    }
    //}

    public void PerformActions(float horizontal, float vertical, bool jump = false)
    {
        

        var dir = transform.forward;
        var move = dir * speed;

        if (Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon)
        {
            transform.rotation =  Quaternion.Euler(0.0f, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0.0f);

            if (isHittingOther)
            {
                dir.x = otherNormal.x * dir.x < 0.01f ? 0 : dir.x;
                dir.z = otherNormal.z * dir.z < 0.01f ? 0 : dir.z;

                move = dir * speed;
                move.y = velocity.y;
            }
        //    else if (isGrounded)
        //    {
        //        var move = dir * speed;
        //        move.y = velocity.y;

        //        velocity = move;
        //    }
        //    else
        //    {
        //        var move = dir * speed;
        //        move.y = velocity.y;

        //        velocity = move;
        //        //rigidbody.AddForce(transform.forward * 8, ForceMode.Impulse);
        //    }
        }
        else
        {
            move = Vector3.zero;
        }

        move.y = velocity.y;
        velocity = move;

        this.isJumping = jump;
    }

    public void setSpeed (float value) {
        speed = value;
    }
}
