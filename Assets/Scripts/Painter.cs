using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject objectToBePainted;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<Paintable>() != null)
        {
            objectToBePainted.GetComponent<Paintable>().ChangeColor(material);
        } 
        else if(objectToBePainted == null)
        {
            Debug.Log("Object to be painted is null");
        }
        else
        {
            Debug.Log("Paintable is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Paintable>() != null)
        {
            other.GetComponent<MeshRenderer>().material = material;
        }

        else if (other.GetComponent<isPaintBucket>() != null)
        {
            material = other.GetComponent<isPaintBucket>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
