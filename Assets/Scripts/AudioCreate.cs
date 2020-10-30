using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCreate : MonoBehaviour
{
   

    public  void playSpawnClip(AudioSource audioSource, AudioClip spawnClip)
    {
        audioSource.PlayOneShot(spawnClip, 1F);//play audio clip
    }
}
