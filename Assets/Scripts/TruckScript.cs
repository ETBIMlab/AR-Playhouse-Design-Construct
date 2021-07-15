using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class TruckScript : MonoBehaviour
{
    GameObject itemInfo, li;

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
           
        }


        RemoveCost();

    }

    private void RemoveCost()
    {
        ItemInfo myInfo = (ItemInfo)itemInfo.GetComponent(typeof(ItemInfo));
        laptopInterface myLaptop = (laptopInterface)li.GetComponent(typeof(laptopInterface));

        myLaptop.removeitemCost(  myInfo.itemPrice);

    }
    /*private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {

    }*/


}
