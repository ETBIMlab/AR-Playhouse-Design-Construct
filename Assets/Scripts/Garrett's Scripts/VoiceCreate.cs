using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MOST CODE WAS COPIED FROM "TestCommands.cs" SCRIPT

public class VoiceCreate : MonoBehaviour
{

    //Audio Source Object to play a audio clip when spawning an object
    private AudioSource audioSource;


    // GameObject copies of all objects that can be ordered by voice
    public GameObject twoByFour;
    public GameObject ladder;
    public GameObject slide;
    public GameObject screw;

    public AudioClip spawnClip;//clip to be played

    //when spawning objects, instantiate 3 units in front of camera
    private int distFromCamera = 3;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();//get the audio source from the object for the audio to be played
    }

    // Called by SpeechManager when the user says the "Order two by four" command
    void OrderTwoByFour()
    {
        Debug.Log("Adding two by four to scene!");
        //Instantiate function takes in an object original to copy, position of the new copy, and rotation as a quaternion)
        Instantiate(twoByFour, transform.position + transform.forward * distFromCamera, Quaternion.identity);    // creates a cube in front of camera

        audioSource.PlayOneShot(spawnClip, 1F);//play audio clip
    }
    // Called by SpeechManager when the user says the "Order ladder" command
    void OrderLadder()
    {
        Debug.Log("Adding ladder to scene!");
        Instantiate(ladder, transform.position + transform.forward * distFromCamera, Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, 1F);//play audio clip
    }
    // Called by SpeechManager when the user says the "Order slide" command
    void OrderSlide()
    {
        Debug.Log("Adding slide to scene!");
        Instantiate(slide, transform.position + transform.forward * distFromCamera, Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, 1F);//play audio clip
    }
    // Called by SpeechManager when the user says the "Order screw" command
    void OrderScrew()
    {
        Debug.Log("Adding screw to scene!");
        Instantiate(screw, transform.position + transform.forward * distFromCamera, Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, 1F);//play audio clip
    }
}
