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
    GameObject itemInfo;
    string objName = null;
    string filteredString = "";
    //public ObjectOrderer Orderer;
    //string value;
    //GameObject gameObj;
   
        [Serializable]
        public struct ReturnableObj
        {
            public string name;
            public double price;
            public int deliveryTime;
            public int instalTime;
            public double sustainability;
            public int fun;
            public GameObject obj;
        }
    private TestActivityLogger logger;
    public ReturnableObj[] returnableObjs = new ReturnableObj[100];
    //ObjectOrderer oo = new ObjectOrderer();
    public bool functionIsCalled = false;
    public GameObject laptopinterface;
    void Start()
    {
        //sam added
        //Orderer = GetComponent<ObjectOrderer>();
        logger = GetComponent<TestActivityLogger>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (functionIsCalled == true)
        {
            oo.GetObjName(FilterString(objName));
            functionIsCalled = false;
        }*/
    }

    public void OnCollisionEnter(Collision col)
    {
        //functionIsCalled = false;
        if (col.gameObject.tag == "item")
        {
            // public GameObject(game) == col.gameObject;
            //Debug.Log(col.gameObject);

            //  value = col.gameObject.name.ToString();
            //value = col.gameObject;
            // value = value.Replace("(Clone)","");

            //Debug.Log(value);

            //Orderer.ReturnValues(value);
            //Debug.Log("Returned item");
            //functionIsCalled = true;
            Destroy(col.gameObject);
            objName = col.gameObject.name;
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
        for (int i = 0; i < returnableObjs.Length; i++)
        {
            if (returnableObjs[i].name == objectName)
            {
                li.removeitemCost(returnableObjs[i].price, returnableObjs[i].instalTime);
                Debug.Log(objectName + " deleted from scene");
                logger.ReturnObjectLog(returnableObjs[i]);
            }
        }
    }
    
}