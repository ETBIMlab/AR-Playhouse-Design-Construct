using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Painter : MonoBehaviour
{
    public GameObject objectToBePainted;
    public Material material;
    public GameObject AssignToBrush; 
    public bool colorPicked = false;
    private bool toolGrabbed = false;
    public Color newColor; 
    public Color objCurrentColor; //before painting
    public Material[] mat;
    public Renderer ren; //renderer for paint brush - new color
    public Renderer objectRen; //renderer for object so we  can change the color

    void Start()
    {
        objectRen = objectToBePainted.GetComponent<Renderer>();
        ren = AssignToBrush.GetComponent<Renderer>();
        ren.material.color = newColor;
        objectRen.material.color = objCurrentColor;
    }

    public void OnGrab()
    {
        toolGrabbed = true;
    }


    private void OnTriggerEnter(Collider other) //for contact between two game objects
    {
        Debug.Log("Painter trying to paint");
        if (objectToBePainted != null && objectToBePainted.GetComponent<IsWoodOrPlastic>().paintWithPaint == true && toolGrabbed == true)
        {

            if (other.GetComponent<Painter>() != null && colorPicked == true && toolGrabbed == true)
            {
                other.GetComponent<MeshRenderer>().material = material; //we are leaving the thing alone
            }

            else if (other.GetComponent<Painter>() != null && toolGrabbed == true) //checking we had a paint bucket and if we grabbed the tool
            {
                colorPicked = true;//yes we picked a color
                                   //I don't know how this works so we comment out and pray it doesn't break anything
                                   //material = objectToBePainted.GetComponent<MeshRenderer>().material; //we want the object to keep it's current material, we just want to change the color
                                   //mat = ren.materials;
                                   //mat[3] = material;
                                   //AssignToBrush.GetComponent<MeshRenderer>().materials = mat;

                //we are going to set the color as the color of the paint which is on the brush
                objectRen.material.color = newColor;
            }
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

    public void OnRelease()
    {
        toolGrabbed = false;
    }

  
}
