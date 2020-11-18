using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
public class ShiftScale : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;


    private GameObject cube;
    public Vector3 scaleChange, positionChange, scaleIn, scaleOut; //shiftRight, shiftLeft;
    public GameObject Environment;

    // Use this for initialization
    void Start()
    {
        //create cube at origin
        //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(0, 0, 5);

        //create plane and adjust down by 0.5
        //GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //plane.transform.position = new Vector3(0, -0.5f, 0);
        scaleChange = new Vector3(5f, 5f, 5f);
        positionChange = new Vector3(0.0f, 5f, 0.0f);

        scaleIn = new Vector3(3.0f, 3.0f, 3.0f);
        scaleOut = new Vector3(-3.0f, -3.0f, -3.0f);

        //shiftRight = new Vector3(1.0f, 0.0f, 0.0f);
        //shiftLeft = new Vector3(-1.0f, 0.0f, 0.0f);
    }

    void Update()
    {

    }
    void Shift()
    {
        Environment.transform.localScale += scaleChange;
        Environment.transform.position += positionChange;
        Debug.Log("this is working");
    }
    void ScaleIn()
    {
        Environment.transform.localScale += scaleIn;
        //Environment.transform.position += positionChange;
        //Debug.Log("this is working");
        Debug.Log("Scale in");
    }
    void ScaleOut()
    {
        Environment.transform.localScale += scaleOut;
        //Environment.transform.position += positionChange;
        Debug.Log("Shifting Down");
    }

}
