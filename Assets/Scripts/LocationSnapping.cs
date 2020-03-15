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

    private Collider snapCollider;      // new

    private SnapObject screwRef;

    // connect to snapping manager

    private void Start()
    {
        snapped = false;
        grabbed = true;
        snapCollider = GetComponent<Collider>();        // new
    }

    // when a object enters the collider (trigger)
    private void OnTriggerEnter(Collider other)     // other is the gameObject that entered the collider
    {
        Debug.Log(other.gameObject.name + "object has entered collider!");

        if (other.gameObject.CompareTag("screw"))   // if the gameobject that entered the collider is tagged with "screw"
        {
            screwToBeSnappedObj = other.gameObject;        // recording the screw that entered the collider (snap zone)
            
            insideCollider = true;
            Debug.Log("SCREW inside collider"); // debugging
        }

        // for other objects, just copy above and change the tag part
        
    }

    // check if object leaves sphere collider (snapping zone) 
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("something exited collider!");
        if (other.gameObject.CompareTag("screw"))
        {
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
            
            screwRef = other.GetComponent<SnapObject>();        // keeping a reference to the screw that entered the collider

            grabbed = screw.grabbed;        // finding out whether object is currently being grabbed or not

            //Debug.Log("Inside Trigger, Grabbed is [ " + grabbed + " ]");
        }
    }

    // snaps object to snapRefObject
    private void SnapObject()
    {
        if (insideCollider == true)
        {

            var snapRefTranform = snapRefObject.transform.position;
            snapRefTranform.z -= 0.01f;         // modified z to make the screw look better when snapped

            screwToBeSnappedObj.transform.position = snapRefTranform;       // snapping transform

            var snapRefRot = snapRefObject.transform.rotation.eulerAngles;  
            snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y + 180, snapRefRot.z);       // changing rotation to make screw face the right way

            screwToBeSnappedObj.transform.rotation = Quaternion.Euler(snapRefRot);      // match rotation to snap object


            // disable object manipulation temporarily (cant grab object once its snapped temporarily)
            snapped = true;
            snapCollider.enabled = false;       // disabling the collider to prevent other objects from snapping to it and just removing the collider 
                                                // since not needed once snapped

            screwToBeSnappedObj.GetComponent<SnapObject>().disableGrabbingtemporarily();        // disable grabbing for a sec

            Debug.Log("OBJECT SNAPPED!");       // debugging
        }
    }

    // Update is called once per frame
    void Update()
    {
        // new 
        // if object has snapped, check if object is being grabbed, if it is then enable the collider again to allow objects to snap to it
        if (snapped)
        {
            if (screwRef.grabbed == true)
            {
                snapCollider.enabled = true;       // new
            }
        }

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
        }
    }

    // drawing a graphic to see where the snap zone is
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var gizmoTransform = transform.position;
        gizmoTransform.x -= 0.001f;

        Gizmos.DrawWireSphere(gizmoTransform, 0.01f);
        //new Vector3(0.01f, 0.01f, 0.01f)
    }
}
