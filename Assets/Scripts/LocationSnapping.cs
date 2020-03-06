using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSnapping : MonoBehaviour
{

    private bool grabbed;

    private bool insideCollider;

    private bool snapped;


    // object was supposed to be used for referencing/snapping
    public GameObject snapRefObject;

    public GameObject objectManger;

    private GameObject screwToBeSnappedObj;

    // connect to snapping manager

    private void Start()
    {
        snapped = false;
        grabbed = true;
    }

    // when a object enters the collider (trigger)
    private void OnTriggerEnter(Collider other)     // other is the gameObject that entered the collider
    {
        Debug.Log(other.gameObject.name + "object has entered collider!");

        if (other.gameObject.CompareTag("screw"))   // if the gameobject that entered the collider is tagged with screw
        {
            screwToBeSnappedObj = other.gameObject;        // recording the screw that entered the collider (snap zone)
            
            insideCollider = true;
            Debug.Log("SCREW inside collider");
        }
        
    }

    // check if object leaves sphere collider (snapping zone) 
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("something exited collider!");
        if (other.gameObject.CompareTag("screw"))
        {
            //screwToBeSnappedObj = null;        // removing the screw that left collider (snap zone)
            //screwInsideZone = false;
            insideCollider = false;
            Debug.Log("screw LEFT collider/zone");

            grabbed = true;
        }
    }

    // function activates and keeps on running when any object is inside collider
    private void OnTriggerStay(Collider other)
    {
        // all objects that have the screw tag must have the SnapObject script attached
        if (other.gameObject.CompareTag("screw"))
        {
            
            SnapObject screw = other.GetComponent<SnapObject>();        // getting SnapObject script from object that is inside snap zone/collider
            grabbed = screw.grabbed;        // finding out whether object is currently being grabbed or not
            //Debug.Log("Inside Trigger, Grabbed is [ " + grabbed + " ]");
        }
    }

    // snaps object to snapRefObject
    private void SnapObject()
    {
        if (insideCollider == true)
        {
            //screw.gameObject.transform.position = transform.position;

            // PROBLEM: Cant get screw to snap at the right location, I want it to snap to the snapRefObject
            Vector3 snapPos = new Vector3(0.4f, -0.2022f, 0.801f);

            screwToBeSnappedObj.transform.position = snapRefObject.transform.position;     // works for the cubes (which are tagged as "screw") BUT NOT the actual screw :C

            screwToBeSnappedObj.transform.rotation = snapRefObject.transform.rotation;    // making the rotation the same as the reference object

            // disable object manipulation temporarily (cant grab object once its snapped temporarily)
            snapped = true;
            screwToBeSnappedObj.GetComponent<SnapObject>().disableGrabbingtemporarily();

            //screwToBeSnappedObj.GetComponent<ManipulationHandler>().enabled = false;    // makes it so you cant pick up the screw again
            
            Debug.Log("OBJECT SNAPPED!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Now allowing grabbing it back up after snapping it
        if (grabbed)
        {
            //Debug.Log("Object being grabbed inside zone");
            snapped = false;
        } 

        if (insideCollider == true && snapped == false)
        {

            Debug.Log("InsideCollider = true, snapped == false");

            // if not currently grabbed, then snap
            if (grabbed == false)
            {
                SnapObject();
            }
           //-----------------------------


        }
        
    }
}
