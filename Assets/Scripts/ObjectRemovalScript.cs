using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class ObjectRemovalScript : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip removeClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision!");
        //if(other.GetComponent<Removable>() == null)
       // {
            //other.GetComponent<Removable>().RemoveObject();
       // }
        other.gameObject.BroadcastMessage("Destroy");
        audioSource.PlayOneShot(removeClip, 1F);
        Destroy(other.gameObject);
    }

}
   