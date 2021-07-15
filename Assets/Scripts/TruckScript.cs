using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class TruckScript : MonoBehaviour
{
    public GameObject laptopinterface;
    //private readonly object laptopinterface;
    //laptopInterface li = new laptopInterface();
    //ObjectOrderer orderObj = new ObjectOrderer();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "item")
        {
            Destroy(col.gameObject);
            RemoveCost();
        }
    }

    private void RemoveCost()
    {
        double cost = 8.50; // need to know how to get the individual item cost from unity

        laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));

        li.removeitemCost(cost);

    }
    /*private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {

    }*/


}
