using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System; 

public class ShiftScale : MonoBehaviour
{
<<<<<<< HEAD
    private KeywordRecognizer keywordRecognizer;
   
   

    private GameObject cube;
    private Vector3 scaleChange, positionChange;
    public GameObject environment; 
=======
    private GameObject cube; 
    private Vector3 scaleChange, positionChange; 
>>>>>>> fc1262ef06d6034df73950bf78aea51e5e578535
    // Use this for initialization
    void Start()
    {
        //create cube at origin
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
<<<<<<< HEAD
        cube.transform.position = new Vector3(0, 0, 5);
=======
        cube.transform.position = new Vector3(0, 0, 0);
>>>>>>> fc1262ef06d6034df73950bf78aea51e5e578535

        //create plane and adjust down by 0.5
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, -0.5f, 0);
<<<<<<< HEAD

        scaleChange = new Vector3(5f, 5f, 5f);
        positionChange = new Vector3(0.0f, 5f, 0.0f);
=======
        
        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        positionChange = new Vector3(0.0f, -0.005f, 0.0f);
>>>>>>> fc1262ef06d6034df73950bf78aea51e5e578535
    }
 
    void Update()
    {
        
    }
    void Shift()
    {
        environment.transform.localScale += scaleChange;
        environment.transform.position += positionChange;
        //Debug.Log("this is working");
    }
}
