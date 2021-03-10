using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDumpster : MonoBehaviour
{
    private AudioSource theAudio1;
    public AudioClip dumpsterAudio;
    public float volume = 1F;

    // Start is called before the first frame update
    void Start()
    {
        theAudio1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "item")
        {
            theAudio1.PlayOneShot(dumpsterAudio, volume);
        }
    }
}
