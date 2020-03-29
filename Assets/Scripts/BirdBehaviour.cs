using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    public float ForwardThrust;
    public float UpwardThrust;
    public float DownardThrust;
    public float BackwardThrust;
    public float SidewardThrust;
    public float RotationSpeed;
    public Vector3 MyRotation = new Vector3();
    public Rigidbody rb;
    public Animator animator;
    private float LastYPosition;
    private enum BirdStates
    {
        Ground,
        Landing,
        Flying,
    }
    private BirdStates BirdState;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LastYPosition = .5f;
    }

    void Update()
    {
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(MyRotation);
        #region input button
        if (Input.GetKey(KeyCode.W))
        {
            OnForwardPress();
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnUpwardPress();
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnLeftwardPress();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            OnReleaseLeftPress();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnRightwardPress();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            OnReleaseRightPress();
        }
        if (Input.GetKey(KeyCode.X))
        {
            OnDownwardPress();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            OnBackwardPress();
        }
        #endregion
        if (BirdState == BirdStates.Flying)
        {
            rb.AddForce(transform.forward * ForwardThrust);
        }
    }

    private void OnReleaseRightPress()
    {
        animator.SetBool("flyRight", false);
    }

    private void OnReleaseLeftPress()
    {
        animator.SetBool("flyLeft", false);
    }

    private void OnDownwardPress()
    {
        rb.AddForce(transform.up * DownardThrust * -1);
    }

    private void FixedUpdate()
    {
        float CurrentYPosition = gameObject.GetComponent<Transform>().position.y;
        if ( CurrentYPosition > .6 && LastYPosition <= .6)
        {
            LandToFlyingPosition();
        }
        if ( CurrentYPosition > .8 && LastYPosition <= .8)
        {
            LandingPositionToFlying();
        }
        else if (CurrentYPosition < .8 && LastYPosition >= .8)
        {
            FlyingToLandingPosition();
        }
        else if(CurrentYPosition <= .6 && LastYPosition >= .6)
        {
            LandingPositionToLand();
        }
        LastYPosition = CurrentYPosition;
    }

    private void OnForwardPress()
    {
        if (BirdState != BirdStates.Ground)
        {
            rb.AddForce(transform.forward * ForwardThrust);
        }
    }

    private void OnUpwardPress()
    {
        rb.AddForce(transform.up * UpwardThrust);
    }

    private void OnLeftwardPress()
    {
        //.AddForce(transform.right * SidewardThrust * -1);
        MyRotation.y = MyRotation.y - RotationSpeed;
        animator.SetBool("flyLeft", true);
    }
    
    private void OnRightwardPress()
    {
        //rb.AddForce(transform.right * SidewardThrust);
        MyRotation.y = MyRotation.y + RotationSpeed;
        animator.SetBool("flyRight", true);
    }

    private void OnBackwardPress()
    {
        rb.AddForce(transform.forward * ForwardThrust * -1);
    }

    private void LandingPositionToFlying()
    {
        animator.SetBool("landing", false);
        animator.SetBool("flying", true);
        BirdState = BirdStates.Flying;
    }
    
    private void FlyingToLandingPosition()
    {
        animator.SetBool("landing", true);
        animator.SetBool("flying", false);
        BirdState = BirdStates.Landing;
    }

    private void LandingPositionToLand()
    {
        animator.SetBool("landing", false);
        animator.SetBool("flying", false);
        BirdState = BirdStates.Ground;
    }

    private void LandToFlyingPosition()
    {
        animator.SetBool("flying", true);
        BirdState = BirdStates.Flying;
    }
}
