using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    public float ForwardThrust;
    public float UpwardThrust;
    public float BackwardThrust;
    public float SidewardThrust;
    public Vector3 MyRotation = new Vector3();
    public Rigidbody rb;
    private enum BirdStates
    {
        Ground,
        SemiFly,
        FullFly,
    }
    private BirdStates BirdState;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(MyRotation);
        if (gameObject.GetComponent<Transform>().position.y < 2)
        {
            BirdState = BirdStates.Ground;
        }
        else if (gameObject.GetComponent<Transform>().position.y < 8)
        {
            BirdState = BirdStates.SemiFly;
        }
        else
        {
            BirdState = BirdStates.FullFly;
        }

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
        if (Input.GetKey(KeyCode.D))
        {
            OnRightwardPress();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            OnBackwardPress();
        }
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
        rb.AddForce(transform.right * SidewardThrust * -1);
    }
    
    private void OnRightwardPress()
    {
        rb.AddForce(transform.right * SidewardThrust);
    }

    private void OnBackwardPress()
    {
        rb.AddForce(transform.forward * ForwardThrust * -1);
    }
}
