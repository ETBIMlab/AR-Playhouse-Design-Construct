using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterAudio : MonoBehaviour
{
    private AudioSource theAudio;
    public AudioClip paintBucket;
    public AudioClip paintObject;
    public float volume = 1F;
    private bool playingAudio = false;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        Debug.Log("Painter Audio Script starting");

    }

    //play audio when coliding with paintbucket or a paintable object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Paintable>() != null && !playingAudio)
        {
            playingAudio = true;
            theAudio.PlayOneShot(paintObject, volume);
            Debug.Log("Audio for non paint bucket...");

        }
        else if (other.GetComponent<isPaintBucket>() != null && !playingAudio)
        {
            playingAudio = true;
            theAudio.PlayOneShot(paintBucket, volume);
            Debug.Log("Audio for paint bucket");

        }

        playingAudio = false;
        Debug.Log("Done playing audio...");

    }
    void Update()
    {

    }
}
