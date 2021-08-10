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
    public isPaintBucket pbScript;
    public IsWoodOrPlastic iwop;
    public Paintable pScript;
   // private bool pb;

    // Start is called before the first frame update
    void Start()
    {

       // pb = gameObject.GetComponent<IsWoodOrPlastic>.isPaintBucket;
        
    }

    public void OnGrab()
    {
        Debug.Log("Paint brushed grabbed!");

        toolGrabbed = true;
    }

    
   /* public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (gameObject != null && gameObject.GetComponent<IsWoodOrPlastic>() != null && toolGrabbed == true)
        {
            gameObject.GetComponent<Paintable>().ChangeColor(material);
            Debug.Log("I painted!");

        }
        else if (gameObject == null)
        {
            Debug.Log("Object to be painted is null");
        }
        else
        {
            Debug.Log("Paintable is null");
        }
    } */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!");
        if (other.gameObject.GetComponent<Paintable>() != null && colorPicked == true && toolGrabbed == true) //this is the same as PainterAudio where it has consistently been getting it
        {
            Debug.Log("I collided with non object I want to paint");

            other.GetComponent<MeshRenderer>().material = material; //not working either
            //PaintObject(material); this essentiall calls a method which does the same as above line 

        }

        else if (other.gameObject.GetComponent<Paintable>() != null && toolGrabbed == true)
        {
            Debug.Log("I collided with paint bucket");

            colorPicked = true;
            material = pbScript.paintColor; //nope
            ren = AssignToBrush.GetComponent<Renderer>();
            mat = ren.materials;
            mat[3] = material;
            AssignToBrush.GetComponent<Renderer>().materials[3] = material;
            Debug.Log("I grabbed new color and put it on brush");

        }
    }

    public void OnRelease()
    {
        Debug.Log("Paintbrush dropped");

        toolGrabbed = false;
    }
    void Update()
    {

    }


}
