using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

//by Albert De La Cruz
public class AudioRoot : MonoBehaviour
{

    Root root;

    private AudioSource theAudio1;
    public AudioClip changeSpace;
    public AudioClip changeLevel;
    public AudioClip changeSize;
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

    public void setSpace()
    {
        theAudio1.PlayOneShot(changeSpace, volume);
    }

    public void shiftLevel()
    {
        theAudio1.PlayOneShot(changeLevel, volume);
    }

    public void changeView()
    {
        theAudio1.PlayOneShot(changeSize, volume);
    }


}
