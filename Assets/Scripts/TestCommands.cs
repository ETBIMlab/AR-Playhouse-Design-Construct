using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommands : MonoBehaviour
{

    // for attaching a cube object in the editor
    [SerializeField] GameObject testCube;

    void Start()
    {
        
    }

    // Called by SpeechManager when the user says the "Create Cube" command (creates a pink cube in front of player)
    void CreateCube()
    {
        Debug.Log("Creating Pink Cube!");

        int distFromCamera = 3;

        Instantiate(testCube, transform.position + transform.forward * distFromCamera, Quaternion.identity);    // creates a cube in front of camera
    }

   
}
