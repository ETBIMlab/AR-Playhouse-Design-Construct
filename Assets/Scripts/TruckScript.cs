using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class TruckScript : MonoBehaviour
{
    //public gameObject();
    //GameObject itemInfo;
    string objName = null;
    string filteredString = "";
    public ObjectOrderer Orderer;
    string value;
    GameObject gameObj;
    /*
        [Serializable]
        public struct OrderableObj
        {
            public string name;
            public double price;
            public int deliveryTime;
            public int instalTime;
            public double sustainability;
            public int fun;
            public GameObject obj;
        }
         public OrderableObj[] orderableObjs = new OrderableObj[100];

    */

    public GameObject laptopinterface;
    void Start()
    {
        //sam added
        Orderer = GetComponent<ObjectOrderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        //functionIsCalled = false;
        if (col.gameObject.tag == "item")
        {
            // public GameObject(game) == col.gameObject;
            Debug.Log(col.gameObject);

            //  value = col.gameObject.name.ToString();
            value = col.gameObject;
            value = value.Replace("(Clone)","");
            
            Debug.Log(value);
           
           Orderer.ReturnValues(value);
            Debug.Log("Returned item");

            Destroy(col.gameObject);
            //  FilterString(objName);
        }

       
    }

    //Filter string to match the current string in array
    /*
    public void FilterString(string objName)
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < objName.Length; i++)
        {
            if (objName[i] == '(')
            {
                break;
                Debug.Log("there is a paren");
            }
            sb.Append(objName[i]);
        }
        filteredString = sb.ToString();
       // RemoveObjectFromLaptop(filteredString);
    }
    
    public void RemoveObjectFromLaptop(string objectName)
    {
        //call reference to laptop script 
        laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));
        for (int i = 0; i < orderableObjs.Length; i++)
        {
            if (orderableObjs[i].name == objectName)
            {
                li.removeitemCost(orderableObjs[i].price, orderableObjs[i].instalTime);
                Debug.Log(objectName + " item deleted");
            }
        }
    }
    */
}