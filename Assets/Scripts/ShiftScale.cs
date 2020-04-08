using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftScale : MonoBehaviour
{
    private GameObject cube; 
    private Vector3 scaleChange, positionChange; 
    // Use this for initialization
    void Awake()
    {
        //create cube at origin
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0, 0);

        //create plane and adjust down by 0.5
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, -0.5f, 0);
        
        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        positionChange = new Vector3(0.0f, -0.005f, 0.0f);
    }
    void Update()
    {
        cube.transform.localScale += scaleChange;
        cube.transform.position += positionChange;

        // Move upwards when the cube hits the floor or downwards
        // when the sphere scale extends 1.0f.
        if (cube.transform.localScale.y < 0.1f || cube.transform.localScale.y > 1.0f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
        }
    }
}
