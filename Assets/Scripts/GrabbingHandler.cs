using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingHandler : MonoBehaviour
{
    public Transform handler;
    public void ResetPosition()
    {
        Debug.Log("Resetting Positon");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;
    }
}
