using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float jumpVelocity = 5f;
    public bool grounded = true;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();   
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        var horizontal = Input.GetAxis ("Horizontal");
        var vertical = Input.GetAxis ("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            m_Rigidbody.velocity = Vector3.up * jumpVelocity;
        }
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        var hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        var hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        var isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        m_Animator.SetBool("IsJumping" , Input.GetButtonDown("Jump"));
        var desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    private void OnCollisionStay(Collision other)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
