using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class KennyLocking : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();
    public float snappingRadius;
    bool collided;
    bool saidDrill = false;

    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("Drill");

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
    private void OnTriggerStay(Collider other)
    {
        collided = true;
        if (saidDrill)
        {
            if(other.gameObject.GetComponent<Lockable>() != null)
            {
                if (other.gameObject.GetComponent<Lockable>().getIsLocked())
                {
                    Debug.Log("Remove lock");
                    other.gameObject.GetComponent<Lockable>().removeLock(gameObject);
                }
                else if(other.gameObject.GetComponent<Lockable>().getIsLocked() == false)
                {
                    Debug.Log("Add lock");
                    other.gameObject.GetComponent<Lockable>().addLock(gameObject);
                }
            }
            saidDrill = false;
        }
    }

    private void OnTriggerExit(Collider collison)
    {
        Debug.Log("We exit collision");
        collided = false;
    }
}
