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
    }

    //play audio when coliding with paintbucket or a paintable object
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IsWoodOrPlastic>().paintWithPaint == true && !playingAudio)
        {
            playingAudio = true;
            theAudio.PlayOneShot(paintObject, volume);

        }
        else if(other.GetComponent<IsWoodOrPlastic>().isPaintBucket == true && !playingAudio)
        {
            playingAudio = true;
            theAudio.PlayOneShot(paintBucket, volume);
        }
        
        playingAudio = false;
    }

}
