﻿using UnityEngine;
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

    public new Rigidbody rigidbody { get; private set; }

    Vector3 velocity;
    
    //impulse available for the current jump
    float jumpImpulse;

    bool isJumping;
    public bool isGrounded;
    bool isHittingOther;

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
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {

            // is only grounded if touched the ground from the top (positive normal y component)
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0)
                {
                    isGrounded = true;
                    //break;
                }

                if (contact.normal.z != 0)
                {
                    // Hit something else that is not the Ground, like a Wall or an Enemy.
                    isHittingOther = true;
                    //break;
                }

                if (isHittingOther || isGrounded) { break; }
            }
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {
            isGrounded = false;
            isHittingOther = false;
        }
    }

    public void PerformActions(float horizontal, float vertical, bool jump = false)
    {
        if (isHittingOther && !isGrounded ) {
            velocity = Vector3.zero;
            return;

        }

        if (Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon)
        {
            GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
            Vector3 v = new Vector3(horizontal, vertical, 0.0f);
            Vector3 r = c.transform.worldToLocalMatrix.MultiplyVector(v);
            Debug.Log("R " + r);
             transform.localRotation = Quaternion.Euler(0.0f, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg, 0.0f);
            //     transform.localRotation =  Quaternion.Euler(0.0f, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0.0f);
            if (isGrounded)
            {
                velocity = transform.forward * speed;
            }
            else
            {
                velocity = transform.forward * speed;
                //rigidbody.AddForce(transform.forward * 8, ForceMode.Impulse);
            }
        }
        else
        {
            velocity = Vector3.zero;
        }

        this.isJumping = jump;
    }

    public void setSpeed (float value) {
        speed = value;
    }
}
