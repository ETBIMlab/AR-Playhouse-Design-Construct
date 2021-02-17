using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterAudio : MonoBehaviour
{
    private AudioSource theAudio;
    public AudioClip paintBucket;
    public AudioClip paintObject;
    public float volume = 1F;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    //play audio when coliding with paintbucket or a paintable object
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Paintable>() != null)
        {
            theAudio.PlayOneShot(paintObject, volume);
        }
        else if(other.GetComponent<isPaintBucket>() != null)
        {
            theAudio.PlayOneShot(paintBucket, volume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
