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
        toolGrabbed = true;
    }


    public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<Paintable>() != null && toolGrabbed == true)
        {
            objectToBePainted.GetComponent<Paintable>().ChangeColor(material);
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
        if (other.GetComponent<Paintable>() != null && colorPicked == true && toolGrabbed == true)
        {
            other.GetComponent<MeshRenderer>().material = material;
        }

        else if (other.GetComponent<isPaintBucket>() != null && toolGrabbed == true)
        {
            colorPicked = true;
            material = other.GetComponent<isPaintBucket>().material;
            ren = AssignToBrush.GetComponent<Renderer>();
            mat = ren.materials;
            mat[3] = material;
            AssignToBrush.GetComponent<MeshRenderer>().materials = mat;
        }
    }

    public void OnRelease()
    {
        toolGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
