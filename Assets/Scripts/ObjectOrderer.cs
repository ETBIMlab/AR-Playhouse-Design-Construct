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
        public double price;
        public int deliveryTime;
        public int instalTime;
        public GameObject obj;
    }

    public GameObject laptopinterface;

    KeywordRecognizer keywordRecognizer = null;
    List<string> keywords = new List<string>();
    public OrderableObj[] orderableObjs;
    List<string> numlist = new List<string>()  {
                        "zero","one","two","three","four","five","six","seven","eight","nine","ten","eleven","twelve"};



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            keywords.Add("Order " + orderableObjs[i].name);
            keywords.Add("Order a dozen " + orderableObjs[i].name+"s");
            for (int j = 0; j < numlist.Count; j++)
            {
                keywords.Add("Order "+ numlist[j] + " " + orderableObjs[i].name+"s");
            }
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
        laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));

        string temp = args.text.Substring(args.text.LastIndexOf(" ") + 1);
        temp = temp.Substring(0, temp.Length-1);
        for (int i = 0; i < orderableObjs.Length; i++)
        {

            if (args.text.Substring(6) == orderableObjs[i].name)
            {
                li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, orderableObjs[i].name);
                AddObjectToScene(orderableObjs[i].obj);
                return;
            }
            else if(temp == orderableObjs[i].name)
            {
                string h = args.text.Substring(6);
                h = h.Substring(0,h.IndexOf(" "));
                Debug.Log(h);
                int hold = numlist.IndexOf(h);
                if (hold != -1)
                {
                    li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, args.text);
                    for (int j = 0; j < hold; j++)
                    {
                        AddObjectToScene(orderableObjs[i].obj);
                    }
                }
                else if(h.Equals("a"))
                {
                    li.additem(orderableObjs[i].price, orderableObjs[i].deliveryTime, args.text);
                    for (int j = 0; j < 12; j++)
                    {
                        AddObjectToScene(orderableObjs[i].obj);
                    }
                }
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
