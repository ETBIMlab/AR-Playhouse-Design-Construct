using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject objectToBePainted;
    public Material material;
    public GameObject AssignToBrush;
    public bool colorPicked = false;
    private bool toolGrabbed = false;

    public Renderer ren;
    public Material[] mat;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnGrab()
    {
        Debug.Log("Paint brushed grabbed!");

        toolGrabbed = true;
    }


    public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<Paintable>() != null && toolGrabbed == true)
        {
            objectToBePainted.GetComponent<Paintable>().ChangeColor(material);
            Debug.Log("I painted!");

        }
        else if (objectToBePainted == null)
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
        Debug.Log("Collided!");

        if (other.GetComponent<Paintable>() != null && colorPicked == true && toolGrabbed == true)
        {
            Debug.Log("I collided with non object I want to paint");

            other.GetComponent<MeshRenderer>().material = material;

        }

        else if (other.GetComponent<isPaintBucket>() != null && toolGrabbed == true)
        {
            Debug.Log("I collided with paint bucket");

            colorPicked = true;
            material = other.GetComponent<isPaintBucket>().material;
            ren = AssignToBrush.GetComponent<MeshRenderer>();
            mat = ren.materials;
            mat[3] = material;
            AssignToBrush.GetComponent<MeshRenderer>().materials[2] = material;
            Debug.Log("I grabbed new color and put it on brush");

        }
    }

    public void OnRelease()
    {
        Debug.Log("Paint brush dropped");

        toolGrabbed = false;
    }
    void Update()
    {

    }


}
