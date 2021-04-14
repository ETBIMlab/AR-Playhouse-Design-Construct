using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script made by Albert De La Cruz
public class AudioLock : MonoBehaviour
{
    private AudioSource theAudio1;
    public AudioClip drillClip;
    public float volume = 1F;
    private KennyLocking kenny;
    // Start is called before the first frame update
    void Start()
    {
        theAudio1 = GetComponent<AudioSource>();//gets the audio file to be played on grab
        kenny = GetComponent<KennyLocking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (kenny.getSaidDrill())
        {
            if (other.gameObject.GetComponent("Lockable") as Lockable != null)
            {
                if (other.gameObject.GetComponent<Lockable>().getIsLocked())
                {
                    theAudio1.PlayOneShot(drillClip, volume);
                }
                else if (other.gameObject.GetComponent<Lockable>().getIsLocked() == false)
                {
                    theAudio1.PlayOneShot(drillClip, volume);
                }
            }
        }
    }
}
