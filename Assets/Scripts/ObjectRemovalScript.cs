using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class ObjectRemovalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Removable>() != null)
        {
            other.gameObject.GetComponent<Removable>().RemoveObject();
        }
    }

}
   