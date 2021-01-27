using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class DragObject : MonoBehaviour
{
    public Camera cam;
    public Transform thingy;
    public float distanceFromCamera;
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        distanceFromCamera = Vector3.Distance(thingy.position, cam.transform.position);
        r = thingy.GetComponent<Rigidbody>();
    }
    Vector3 lastPos;
    // Update is called once per frame
    void Update()
    {
        //if(IMixedRealityPointerHandler.OnPointerD)
    }
}
