using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class ObjectOrderer : MonoBehaviour
{

    [Serializable]
    public struct OrderableObj
    {
        public string name;
        public GameObject obj;
    }

    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();
    public OrderableObj[] orderableObjs;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < orderableObjs.Length; i++)
        {
            keywords.Add("Order " + orderableObjs[i].name);
        }

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());

        // Register a callback for the KeywordRecognizer and START recognizing!!!!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // important
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        for(int i = 0; i < orderableObjs.Length; i++)
        {
            if(args.text.Substring(6) == orderableObjs[i].name)
            {
                AddObjectToScene(orderableObjs[i].obj);
                return;
            }
        }
    }

    public Boolean AddObjectToScene(string objectName)
    {
        for(int i = 0; i < orderableObjs.Length; i++)
        {
            if(orderableObjs[i].name == objectName)
            {
                Instantiate(orderableObjs[i].obj, new Vector3(0, 0, 0), Quaternion.identity);
                return true;
            }
        }
        return false;
    }

    private void AddObjectToScene(GameObject newObj)
    {
        Instantiate(newObj, transform.position + (transform.forward * 0) + (transform.up * 0.25f) + (transform.right * -2), Quaternion.identity);
    }
}
