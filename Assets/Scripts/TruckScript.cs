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
    GameObject returnObj;
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
    public ReturnableObj[] returnableObjs;
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

    public void OnTriggerEnter(Collider col)
    {
        //functionIsCalled = false;
        if (col.gameObject.tag == "item" && col.gameObject != null)
        {
            // public GameObject(game) == col.gameObject;
            //Debug.Log(col.gameObject);

            //  value = col.gameObject.name.ToString();
            //value = col.gameObject;
            // value = value.Replace("(Clone)","");

            //Debug.Log(value);

            //Orderer.ReturnValues(value);
            Debug.Log("Entering return");
            //functionIsCalled = true;
            //Destroy(col.gameObject);
            objName = col.gameObject.name;
            objName = objName.Replace("(Clone)","");
            //objName = objName.Replace("(Fixed)", "");
            returnObj = col.gameObject;
            Debug.Log("Name acquired and fixed");

            //FilterString(objName);
            //call reference to laptop script 
            laptopInterface li = (laptopInterface)laptopinterface.GetComponent(typeof(laptopInterface));
            for (int i = 0; i < returnableObjs.Length; i++)
            {
                Debug.Log("Iterating...");


                if (returnableObjs[i].name == objName && returnObj != null)
                {
                  
                    Debug.Log("Sending to Laptop Interface" + returnableObjs[i].name);
                    li.removeitemCost(returnableObjs[i].price, returnableObjs[i].instalTime);
                    Debug.Log("Sending to logger");
                    logger.ReturnObjectLog(returnableObjs[i]);
                    Debug.Log("Destroying the object...");
                    Destroy(col.gameObject);
                    break;
                }

            }
            Debug.Log("Out of iteration...");

        }


    }
    /*
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
    
    public void RemoveObjectFromLaptop(ReturnableObj returnable)
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
    */
}