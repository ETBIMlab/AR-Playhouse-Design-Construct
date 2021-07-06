using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
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
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            Destroy(other.gameObject);
        }
    }*/
}
