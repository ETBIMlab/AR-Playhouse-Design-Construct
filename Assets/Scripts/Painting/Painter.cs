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
    public Material newColor;

   public GameObject AssignToBrush; // we want to assign it to the brush but we do that in unity
    public bool colorPicked = false;
    private bool toolGrabbed = false;
    //public Color newColor; //color we want to paint it
    //public Color objCurrentColor; //before painting
    public MeshRenderer ren;
    public MeshRenderer pren;

    public Material[] mat;
    public Material[] pmat;
    //public Renderer ren; //renderer for paint brush - new color IGNORE THIS
    //public Renderer objectRen; //renderer for object so we  can change the color IGNORE

    void Start()
    {
       // newColor = AssignToBrush.GetComponent<IsWoodOrPlastic>().paintColor;
        //Debug.Log("Got the new color!");
        //ren = AssignToBrush.GetComponent<Renderer>();
        //ren.material.color = newColor;
        //objCurrentColor = objectToBePainted.GetComponent<Renderer>().material.color; //the current color is the current color
        //Debug.Log("Got the old color!");

    }

    public void OnGrab()
    {
        Debug.Log("Tool has been grabbed!");
        toolGrabbed = true;
    }


    private void OnTriggerEnter(Collider other) //for contact between two game objects
    {

        Debug.Log("Collided!");
        if (other != null && toolGrabbed == true)
        {

            if (other.GetComponent<IsWoodOrPlastic>() != null && colorPicked == true && toolGrabbed == true)
            {
                Debug.Log("I collided with a non paint bucket!");

                //we are going to set the color as the color of the paint which is on the brush
                mat = other.GetComponent<MeshRenderer>().materials;
                mat[0] = newColor;
                Debug.Log("I gave it a new color");

            }
            //the below is if we come into contact with a paint bucket, the above is if we come into contact with
            //another object (wood for example)
            else if (other.GetComponent<IsWoodOrPlastic>() != null && toolGrabbed == true) //checking we had a paint bucket and if we grabbed the tool
            {
                Debug.Log("Collided with paint bucket!");

                colorPicked = true;//yes we picked a color
               //we want the object to keep it's current material, we just want to change the color
                mat = other.GetComponent<MeshRenderer>().materials;
                mat[2] = newColor;
                Debug.Log("Grabbed new color");        
                pmat = AssignToBrush.GetComponent<MeshRenderer>().materials;
                pmat[3] = newColor;
                Debug.Log("Assigned Brush the new color!");
            }
        }
        else if (other == null)
        {
            Debug.Log("Object to be painted is null");
        }
        else
        {
            Debug.Log("Paintable is null");
        }
    }

    public void OnRelease()
    {
        Debug.Log("Paintbrush dropped");
        toolGrabbed = false;
    }

  
}
