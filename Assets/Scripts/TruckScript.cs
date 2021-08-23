using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
//using ObjectOrderer;

public class TruckScript : MonoBehaviour
{
    GameObject itemInfo, li;
    //public ObjectOrderer.OrderableObj[] orderableObjs;
    // string name = ObjectOrderer.orderableObjs[0].name;
    // ObjectOrderer 
    // [SerializeField] private ObjectOrderer scriptAReference;
    ObjectOrderer oo = new ObjectOrderer();
    string objName = null;
    string filteredString = "";

    public bool functionIsCalled = false;

    void Start()
    {
        //Debug.Log(orderableObj.getValues(name));
        //OrderableObj OrderableObj1 = new OrderableObj();
        //Debug.Log(OrderableObj1.getValues(OrderableObj1));
      //  scriptAReference = (scriptAReference)scriptAReference.GetComponent(typeof(ObjectOrderer));
    }

    // Update is called once per frame
    void Update()
    {
       if (functionIsCalled == true)
        {
            oo.GetObjName(FilterString(objName));
            functionIsCalled = false;
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        //functionIsCalled = false;
        if (col.gameObject.tag == "item")
        {
            functionIsCalled = true;
            Destroy(col.gameObject);
            objName = col.gameObject.name;
            FilterString(objName);
        }
    }

    //Filter string to match the current string in array
    public string FilterString(string objName)
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
        return sb.ToString();
    }

    /*
    public void FunctionHasBeenCalled(bool functionIsCalled)
    {
        /*if (functionIsCalled == true)
        {
            Debug.Log("bool is true");
        }
        return; 

    }
    */
}