using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommands : MonoBehaviour
{
    //Audio Source Object to play a audio clip when spawning an object
    private AudioSource audioSource;
    public float volume = 1F;
    public AudioClip spawnClip;
    public string audioTarget;
    private GameObject gameLog;

    // for attaching a cube object in the editor
    public GameObject twoByFour;
    public GameObject ladder;
    public GameObject slide;
    public GameObject screw;
    public GameObject playhouseEnvironment;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();//get the audio source from the object for the audio to be played
        gameLog = GameObject.Find(audioTarget);
        audioSource = gameLog.GetComponent<AudioSource>();
    }

    // Called by SpeechManager when the user says the "Create Cube" command (creates a pink cube in front of player)
    void OrderTwoByFour()
    {
        Debug.Log("Adding Two By Four to Scene!");

        int distFromCamera = 3;

        Instantiate(twoByFour, transform.position + transform.forward * distFromCamera, Quaternion.identity);    // creates a cube in front of camera

        audioSource.PlayOneShot(spawnClip, volume);//play audio clip

    }
    void OrderLadder()
    {
        int distFromCamera = 3;
        Instantiate(ladder, new Vector3(56,-20,150), Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, volume);//play audio clip
    }
    void OrderSlide()
    {
        int distFromCamera = 3;
        Instantiate(slide, new Vector3(0, -20, 150), Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, volume);//play audio clip
    }
    void OrderScrew()
    {
        int distFromCamera = 1;
        Instantiate(screw, new Vector3(48, -29, 101), Quaternion.identity);

        audioSource.PlayOneShot(spawnClip, volume);//play audio clip
    }

    void ChangeSize()
    {
        playhouseEnvironment.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
    }
   
}
