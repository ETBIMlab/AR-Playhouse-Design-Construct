using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SnapObject : MonoBehaviour
{

    // amount of time the grabbing gets disabled for when snapped
    [SerializeField] float disableTime = 1f;

    public bool grabbed;        // whether or not object is grabbed

    public bool handlerEnabled = true;      // just for debugging only

    private ManipulationHandler manipHandler;


    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        manipHandler = GetComponent<ManipulationHandler>();     // getting the ManipulationHandler componenet attached to this object
    }

    // Update is called once per frame
    void Update()
    {
        handlerEnabled = manipHandler.enabled;      // this just for debugging only (so i can see in the inspector if the ManipHandler was disabled)
    }

    // called by ManipulationHandler on object this script is attached to (look at inspector under events to see how implemented)
    // when grabbed, grabbed is set to true
    public void setGrabbedToFalse()
    {
        grabbed = false;
        Debug.Log("grabbedtofalse = " + grabbed);
    }

    // called by ManipulationHandler on object this script is attached to (look at inspector under events to see how implemented)
    // when grabbed, grabbed is set to false
    public void setGrabbedToTrue()
    {
        grabbed = true;
        Debug.Log("grabbedtotrue = " + grabbed);
    }

    // disable grabbing for a bit
    public void disableGrabbingtemporarily()
    {
        Debug.Log("DISABLED Grabbing");
        // diable manipulationHandler temporarily
        manipHandler.enabled = false;

        // call courotine to enable it again after some time
        StartCoroutine(disableTemp());
    }


    IEnumerator disableTemp()
    {

        yield return new WaitForSeconds(disableTime);       // waiting 

        // activate handler to enable grabbing again
        manipHandler.enabled = true;
       
        Debug.Log("**ENABLED Grabbing**");
    }

}
