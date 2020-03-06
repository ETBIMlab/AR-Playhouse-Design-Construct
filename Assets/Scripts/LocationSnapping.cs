using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSnapping : MonoBehaviour
{

    //private bool grabbed;

    private bool insideCollider;

    private bool snapped;

    //private bool screwInsideZone;

    //[SerializeField] GameObject screw;

    // object was supposed to be used for referencing/snapping to it BUT NOT WORKING?
    [SerializeField] GameObject snapRefObject;

    private GameObject screwToBeSnappedObj;

    // connect to snapping manager

    private void Start()
    {
        snapped = false;
        //screwInsideZone = false;

        // test
        //screwToBeSnappedObj = screw;
        //grabbed = true;
    }

    // when a object enters the collider (trigger)
    private void OnTriggerEnter(Collider other)     // other is the gameObject that entered the collider
    {
        Debug.Log(other.gameObject.name + " entered collider!");

        
        if (other.gameObject.CompareTag("screw"))   // if the gameobject that entered the collider is tagged with screw
        {
            screwToBeSnappedObj = other.gameObject;        // recording the screw that entered the collider (snap zone)
            //screwInsideZone = true;
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
        }
    }
    
    // snaps object to snapRefObject
    private void SnapObject()
    {
        if (insideCollider == true)
        {
            //screw.gameObject.transform.position = transform.position;

            //Testing
            //Vector3 snapPos = new Vector3(snapRefObject.transform.position.x, snapRefObject.transform.position.y, snapRefObject.transform.position.z - 0.05f);


            // PROBLEM: Cant get screw to snap at the right location, I want it to snap to the snapRefObject
            Vector3 snapPos = new Vector3(0.4f, -0.2022f, 0.801f);

            screwToBeSnappedObj.transform.position = snapRefObject.transform.position;     // works for the cubes (which are tagged as "screw") BUT NOT the actual screw :C

            screwToBeSnappedObj.transform.rotation = snapRefObject.transform.rotation;    // making the rotation the same as the reference object

            //screwToBeSnappedObj.GetComponent<ManipulationHandler>().enabled = false;    // makes it so you cant pick up the screw again
            snapped = true;
            Debug.Log("OBJECT SNAPPED!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // only snap when screw is inside the Collider/snap zone
        if (insideCollider == true)
        {
            SnapObject();


            /*
            //Debug.Log("GRABBED = " + screwToBeSnappedObj.GetComponent<SnapObject>().grabbed);
            if (snapped == false && screwToBeSnappedObj.GetComponent<SnapObject>().grabbed == false)
            {
                SnapObject();
            }
            
            */
            
        }
    }
}
