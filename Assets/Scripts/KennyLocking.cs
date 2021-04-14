using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class KennyLocking : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();
    public float snappingRadius;
    bool collided;
    bool saidDrill = false;

    //audio components
    private AudioSource theAudio1;
    public AudioClip drillClip;
    public float volume = 1F;

    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("Drill");

        theAudio1 = GetComponent<AudioSource>();//gets the audio file to be played on drill

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if(collided == true)
        {
            saidDrill = true;
        }
        else
        {
            saidDrill = false;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        collided = true;
        if (saidDrill)
        {
            saidDrill = false;
            if (other.gameObject.GetComponent("Lockable") as Lockable != null)
            {
                if (other.gameObject.GetComponent<Lockable>().getIsLocked())
                {
                    Debug.Log("Remove lock");
                    other.gameObject.GetComponent<Lockable>().removeLock(gameObject);
                    removeScrews(other.gameObject);
                    theAudio1.PlayOneShot(drillClip, volume);
                }
                else if (other.gameObject.GetComponent<Lockable>().getIsLocked() == false)
                {
                    Debug.Log("Add lock");
                    other.gameObject.GetComponent<Lockable>().addLock(gameObject);
                    addScrews(other.gameObject);
                    theAudio1.PlayOneShot(drillClip, volume);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collided = false;
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    collided = true;
    //    if (saidDrill)
    //    {
    //        saidDrill = false;
    //        if (other.gameObject.GetComponent("Lockable") as Lockable != null)
    //        {
    //            if (other.gameObject.GetComponent<Lockable>().getIsLocked())
    //            {
    //                Debug.Log("Remove lock");
    //                other.gameObject.GetComponent<Lockable>().removeLock(gameObject);
    //                removeScrews(other.gameObject);
    //            }
    //            else if(other.gameObject.GetComponent<Lockable>().getIsLocked() == false)
    //            {
    //                Debug.Log("Add lock");
    //                other.gameObject.GetComponent<Lockable>().addLock(gameObject);
    //                addScrews(other.gameObject);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider collison)
    //{
    //    collided = false;
    //}

    private void addScrews(GameObject collided)
    {
        GameObject lockingPoints = collided.transform.Find("Locking Points").gameObject;
        //foreach (Transform child in lockingPoints.transform)
        //{
        //    child.gameObject.SetActive(true);
        //}
        lockingPoints.gameObject.SetActive(true);
    }
    private void removeScrews(GameObject collided)
    {
        GameObject lockingPoints = collided.transform.Find("Locking Points").gameObject;
        //foreach (Transform child in lockingPoints.transform)
        //{
        //    child.gameObject.SetActive(false);
        //}
        lockingPoints.gameObject.SetActive(false);
    }

    public bool getSaidDrill()
    {
        return saidDrill;
    }
}
