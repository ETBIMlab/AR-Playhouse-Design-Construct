using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class Painter : MonoBehaviour
{
    public GameObject objectToBePainted;
    public Material material;
    public GameObject AssignToBrush;
    public bool colorPicked = false;
    private bool toolGrabbed = false;

    public Renderer ren;
    public Material[] mat;

  

    public void OnGrab()
    {
        Debug.Log("Tool has been grabbed!");
        toolGrabbed = true;
    }
    public void PaintObject(Material paint)
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<IsWoodOrPlastic>() != null && toolGrabbed == true) //need to add check if it can be painted here
        {
            Debug.Log("Painter successful");

            objectToBePainted.GetComponent<IsWoodOrPlastic>().ChangeColor(material);
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

        if (other.GetComponent<IsWoodOrPlastic>() != null && colorPicked == true && toolGrabbed == true)
        {
            Debug.Log("I collided with a non paint bucket!");

            other.GetComponent<MeshRenderer>().material = material;
        }

        else if (other.GetComponent<IsWoodOrPlastic>() != null && toolGrabbed == true)
        {
            Debug.Log("I collided with a paint bucket!");

            colorPicked = true;
            material = other.GetComponent<IsWoodOrPlastic>().paintColorMaterial;
            ren = AssignToBrush.GetComponent<Renderer>();
            mat = ren.materials;
            mat[3] = material;
            AssignToBrush.GetComponent<MeshRenderer>().materials = mat;
        }
    }
   
    public void OnRelease()
    {
        Debug.Log("Paintbrush dropped");
        toolGrabbed = false;
    }

  
}
