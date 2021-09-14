using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using ObjectOrderer;
public class TruckScript : MonoBehaviour
{
    
    GameObject itemInfo;
    string objName = null;
    string filteredString = "";

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
    public GameObject laptopinterface;
*/
   

    void Start()
    {
    
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
            Destroy(col.gameObject);
            objName = col.OrderableObj.name;
            Debug.log(objName);
            FilterString(objName);
        }
    }

    //Filter string to match the current string in array
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
        RemoveObjectFromLaptop(filteredString);
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
}