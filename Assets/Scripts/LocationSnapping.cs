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

    [Header("Gizmo Radius (Red Outline)")]
    [SerializeField] private bool enableSphereGizmo = true;

    [SerializeField] private float gizmoSize = 0.01f;

    [SerializeField] private Vector3 boxSizeVector = new Vector3(0.01f, 0.01f, 0.01f);

    private GameObject objToBeSnapped;

    private Collider snapCollider;      

    private SnapObject objSnapRef;

    // allowing to choose what gets snapped (in inspector)
    [Header("Choose what snaps to this Zone")]
    [SerializeField] bool snapScrews = true;        // default value
    [SerializeField] bool snapWoodPieces;

    [Header("Snapping Angle for Wood Piece")]
    [SerializeField] private bool zeroDegree = true;
    [SerializeField] private bool ninetyDegree = false;
    [SerializeField] private bool negNinetyDegree = false;
    [SerializeField] private bool test = false;

    // connect to snapping manager

    private void Start()
    {
        snapped = false;
        grabbed = true;
        snapCollider = GetComponent<Collider>();      
        gizmoSize = 0.01f;    // new
    }

    // when a object enters the collider (trigger)
    private void OnTriggerEnter(Collider other)     // other is the gameObject that entered the collider
    {
        Debug.Log(other.gameObject.name + "object has entered collider!");

        if (other.gameObject.CompareTag("screw") && snapScrews == true)   // if the gameobject that entered the collider is tagged with "screw"
        {
            objToBeSnapped = other.gameObject;        // recording the screw that entered the collider (snap zone)
            
            insideCollider = true;
            Debug.Log("BOLT inside collider"); // debugging
        }
        else if (other.gameObject.CompareTag("woodPiece") && snapWoodPieces == true)      // NEW  
        {
            objToBeSnapped = other.gameObject;        // recording the woodPiece that entered the collider (snap zone)

            insideCollider = true;
            Debug.Log("WOOD PIECE inside collider"); // debugging
        }

        // for other objects, just copy above and change the tag part
        
    }

    // check if object leaves sphere collider (snapping zone) 
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("something exited collider!");
        if (other.gameObject.CompareTag("screw") && snapScrews == true)
        {
            insideCollider = false;
            Debug.Log("screw LEFT collider/zone");

            grabbed = true;
        } 
        else if (other.gameObject.CompareTag("woodPiece") && snapWoodPieces == true)      // NEW
        {
            insideCollider = false;
            Debug.Log("wood piece LEFT collider/zone");

            grabbed = true;
        }
    }

    // function activates and keeps on running when any object is inside collider
    private void OnTriggerStay(Collider other)
    {
        // all objects that have the screw tag must have the SnapObject script attached
        if (other.gameObject.CompareTag("screw") && snapScrews == true)
        {
            
            SnapObject screw = other.GetComponent<SnapObject>();        // getting SnapObject script from object that is inside snap zone/collider
            
            objSnapRef = other.GetComponent<SnapObject>();        // keeping a reference to the screw that entered the collider

            grabbed = screw.grabbed;        // finding out whether object is currently being grabbed or not

            //Debug.Log("Inside Trigger, Grabbed is [ " + grabbed + " ]");
        }
        else if (other.gameObject.CompareTag("woodPiece") && snapWoodPieces == true)      // NEW
        {
            SnapObject woodPiece = other.GetComponent<SnapObject>();        // getting SnapObject script from object that is inside snap zone/collider

            objSnapRef = other.GetComponent<SnapObject>();        // keeping a reference to the object that entered the collider

            grabbed = woodPiece.grabbed;        // finding out whether object is currently being grabbed or not
        }
    }

    // snaps object to snapRefObject
    private void SnapObject()
    {
        if (insideCollider == true)
        {
            if (objToBeSnapped.CompareTag("screw") && snapScrews == true)
            {
                var snapRefTranform = snapRefObject.transform.position;
                snapRefTranform.z -= 0.01f;         // modified z to make the screw look better when snapped

                objToBeSnapped.transform.position = snapRefTranform;       // snapping transform

                var snapRefRot = snapRefObject.transform.rotation.eulerAngles;
                snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y + 180, snapRefRot.z);       // changing rotation to make screw face the right way

                objToBeSnapped.transform.rotation = Quaternion.Euler(snapRefRot);      // match rotation to snap object


                // disable object manipulation temporarily (cant grab object once its snapped temporarily)
                snapped = true;
                snapCollider.enabled = false;       // disabling the collider to prevent other objects from snapping to it and just removing the collider 
                                                    // since not needed once snapped

                objToBeSnapped.GetComponent<SnapObject>().disableGrabbingtemporarily();        // disable grabbing for a sec

                Debug.Log("OBJECT SNAPPED!");       // debugging
            }
            else if (objToBeSnapped.CompareTag("woodPiece") && snapWoodPieces == true)
            {
                var snapRefTranform = snapRefObject.transform.position;
                //snapRefTranform.z -= 0.01f;         // modified z to make the screw look better when snapped

                

                var snapRefRot = snapRefObject.transform.rotation.eulerAngles;
                //snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y, snapRefRot.z + 90);       // changing rotation to make screw face the right way
                // testing
                snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y, snapRefRot.z);

                if (ninetyDegree == true)
                {
                    Debug.Log("90 degree Snap!**********************");
                    //snapRefTranform.z -= 0.49f;
                    //objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                    //snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y, snapRefRot.z + 90);
                    objToBeSnapped.transform.rotation = snapCollider.transform.rotation;      // match rotation to snap object
                    // test below
                    snapRefTranform.z -= 0.49f;
                    objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                }
                else if (snapRefRot.z == -90f || negNinetyDegree == true)
                {
                    Debug.Log("-90 degree Snap!");
                    //snapRefTranform.z += 0.49f;
                    //objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                    //snapRefRot = new Vector3(snapRefRot.x, snapRefRot.y, snapRefRot.z + 90);
                    objToBeSnapped.transform.rotation = Quaternion.Euler(snapRefRot);      // match rotation to snap object
                    // test below
                    snapRefTranform.z += 0.49f;
                    objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                }
                else if (test == true)
                {
                    // TESTING LOCAL TRANSFORM
                    Debug.Log("TESTING LOCAL SNAPPING!");
                    objToBeSnapped.transform.position = snapRefTranform;
                    objToBeSnapped.transform.rotation = Quaternion.Euler(snapRefRot);      // match rotation to snap object

                }
                else
                {
                    Debug.Log("transform.rotation.x = " + snapCollider.transform.rotation.eulerAngles.x);
                    Debug.Log("NORMAL Snap!**********************");
                    //snapRefTranform.y += 0.490f;
                    //objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                    objToBeSnapped.transform.rotation = Quaternion.Euler(snapRefRot);      // match rotation to snap object
                    // test below
                    snapRefTranform.y += 0.490f;
                    objToBeSnapped.transform.position = snapRefTranform;       // snapping transform
                }

                


                // disable object manipulation temporarily (cant grab object once its snapped temporarily)
                snapped = true;
                snapCollider.enabled = false;       // disabling the collider to prevent other objects from snapping to it and just removing the collider 
                                                    // since not needed once snapped

                objToBeSnapped.GetComponent<SnapObject>().disableGrabbingtemporarily();     // disable grabbing for a sec

                Debug.Log("OBJECT SNAPPED!");       // debugging
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // new 
        // if object has snapped, check if object is being grabbed, if it is then enable the collider again to allow objects to snap to it
        if (snapped)
        {
            if (objSnapRef.grabbed == true)
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

            //Debug.Log("InsideCollider = true, snapped == false");             <-- This DEBUG LINE prints when an object is inside collider

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


        //Gizmos.DrawWireSphere(gizmoTransform, sphereCollider.radius);       // new, trying to match current collide radius with gizmo (not working)

        if (enableSphereGizmo == true)
        {
            Gizmos.DrawWireSphere(gizmoTransform, gizmoSize);       //  but worked
        }
        else
        {
            Gizmos.DrawWireCube(gizmoTransform, boxSizeVector);
        }
        



        //new Vector3(0.01f, 0.01f, 0.01f)
    }
}
