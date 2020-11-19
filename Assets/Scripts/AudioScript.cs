using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;


//script made by Albert De La Cruz
//child of Mixed Reality Pointer Handler as a base 
public class AudioScript : MonoBehaviour, IMixedRealityPointerHandler
{

    private AudioSource theAudio1;
    public AudioClip grabClip;
    public AudioClip releaseClip;
    public AudioClip dragClip;
    public AudioClip clickClip;
    public float volume = 1F;


    // Start is called before the first frame update
    void Start()
    {
        theAudio1 = GetComponent<AudioSource>();//gets the audio file to be played on grab
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //plays audio file when the object is grabbed by the user
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        theAudio1.PlayOneShot(grabClip, volume);
    }

    //plays audio file when object is let go by the user
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        theAudio1.PlayOneShot(releaseClip, volume);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        theAudio1.PlayOneShot(dragClip, volume);
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        theAudio1.PlayOneShot(clickClip, volume);
    }

   
}
