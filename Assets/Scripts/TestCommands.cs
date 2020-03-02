using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommands : MonoBehaviour
{

    // for attaching a cube object in the editor
    public GameObject twoByFour;
    public GameObject ladder;
    public GameObject slide;
    public GameObject screw;

    void Start()
    {
        
    }

    // Called by SpeechManager when the user says the "Create Cube" command (creates a pink cube in front of player)
    void OrderTwoByFour()
    {
        Debug.Log("Adding Two By Four to Scene!");

        int distFromCamera = 3;

        Instantiate(twoByFour, transform.position + transform.forward * distFromCamera, Quaternion.identity);    // creates a cube in front of camera
    }
    void OrderLadder()
    {
        int distFromCamera = 3;
        Instantiate(twoByFour, new Vector3(0,0,0), Quaternion.identity);
    }
    void OrderSlide()
    {
        int distFromCamera = 3;
        Instantiate(slide, new Vector3(0, 0, 0), Quaternion.identity);
    }
    void OrderScrew()
    {
        int distFromCamera = 1;
        Instantiate(screw, new Vector3(0, 0, 0), Quaternion.identity);
    }

   
}
