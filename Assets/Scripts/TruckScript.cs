using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    GameObject itemInfo, li;
    ObjectOrderer.OrderableObj[] data;    //accessing struct from Object Orderer script

    public void ImportData(ObjectOrderer source)
    {
        this.data = source.ExportData();
    }

    void Start()
    {
        //Debug.Log(orderableObj.getValues(name));
        //OrderableObj OrderableObj1 = new OrderableObj();
        //Debug.Log(OrderableObj1.getValues(OrderableObj1));
        Debug.Log(data);
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
    }
   
}