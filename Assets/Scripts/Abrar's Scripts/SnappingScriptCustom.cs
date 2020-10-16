using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnappingScriptCustom : MonoBehaviour
{
    // Modified version of TJ's script "SnappingScript.cs"
    // This will snap to a specific point and not the origin of the snapping collider

    //private List<GameObject> ColliderListObjects = new List<GameObject>();

    private List<Collider> ColliderListObjects = new List<Collider>(); // new

    public float snappingRadius;
    private float minDistance = -1f;

    private ItemInfo itemInfo;
    private ItemInfo.ItemType itemType;
    private ItemInfo.ItemOrientation itemOrientation;

    private Collider snappedCollider = null;

    [Header("Settings for Enabling other Snap Points After Being Snapped")]
    private bool snappedOnToObject = false;
    private bool collidersEnabled = true;
    // these will be enabled after
    [SerializeField] List<GameObject> colliderObjectsToEnable;

    // Start is called before the first frame update
    void Start()
    {
        // contains the gameobject with snapping colliders of the gameobject which this script is attached to (e.g. face panel object)
        ColliderListObjects = GetChildSnappingColliders(transform.gameObject);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, 10));

        itemInfo = GetComponent<ItemInfo>();

        //this.itemType = GetComponent<ItemInfo>().itemType;
        this.itemType = itemInfo.itemType;
        //this.itemOrientation = GetComponent<ItemInfo>().itemOrientation;
        this.itemOrientation = itemInfo.itemOrientation;
    }

    // changed to colliders instead of gameobject
    private List<Collider> GetChildSnappingColliders(GameObject obj)
    {
        List<Collider> childSnappingColliders = new List<Collider>();

        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.tag.Equals("SnappingCollider"))
            {
                //Debug.Log("Adding " + child.gameObject.name + " to colliders of " + transform.gameObject.name);
                childSnappingColliders.Add(child.gameObject.GetComponent<Collider>());
            }
            else
            {
                childSnappingColliders.AddRange(GetChildSnappingColliders(child.gameObject));
            }
        }
        return childSnappingColliders;
    }

    public void SnapToLocation()
    {
        Debug.Log("\n-------------------------------");

        minDistance = -1f;  // refresh min distance

        snappedOnToObject = false;
        bool foundSnapLocation = false;

        // disable colliders in colliderObjectsToEnable
        if (collidersEnabled == true) disableColliderObjects();

        Collider minDistanceChildCollider = null;
        Vector3 minDistanceHitColliderLocation = new Vector3(0, 0, 0);

        // NEW
        // updating item orientation (in case user rotates object) ONLY WORKS FOR ONE TYPE OF PANEL SO FAR
        itemOrientation =  itemInfo.UpdateItemOrienation(transform.eulerAngles.y);

        //foreach (GameObject SnapPointObject in ColliderListObjects)
        foreach (Collider SnapPointCollider in ColliderListObjects)
        {
            //Debug.Log("Looking at collider: " + SnapPoint.name);
            Collider[] hitColliders = Physics.OverlapSphere(SnapPointCollider.transform.position, snappingRadius);

            if (hitColliders.Length == 0)
            {
                return;
            }

            foreach (Collider ConnectedSnappingPoint in hitColliders)
            {
                // error checking --------------------------------------------------------------- Remove
                if (ConnectedSnappingPoint.GetComponent<SnappingValidator>() == null)
                {
                    continue; // Could not find SnappingValidator script on snap point collider so continue to next iteration of foreach loop
                }
                // NEW: checking if this object can even snap to that snapping point
                bool canSnap = ConnectedSnappingPoint.GetComponent<SnappingValidator>().verifySnapCapability(itemType, itemOrientation);
                if (canSnap == true)
                {
                    Debug.Log("Snap-able!!!   item is: " + itemType.ToString());
                    //Debug.Log(SnapPoint.name + " is close enough to " + ConnectedSnappingPoint.gameObject.name);
                    if (ConnectedSnappingPoint.gameObject.CompareTag("SnappingCollider") && 
                        !ColliderListObjects.Contains(ConnectedSnappingPoint.gameObject.GetComponent<Collider>()))
                    {
                        float distance = Vector3.Distance(SnapPointCollider.transform.position, ConnectedSnappingPoint.transform.position);
                        if (distance < minDistance || minDistance < 0)
                        {
                            Debug.Log("Found snapping point named:" + SnapPointCollider.name);

                            if (ConnectedSnappingPoint.transform.childCount > 0)
                            {
                                minDistance = distance;
                                minDistanceChildCollider = SnapPointCollider;

                                // NEW: get child object "custom snap point" from snapping collider game object to get custom snap point
                                Debug.Log("found CHILD snapping point: " + ConnectedSnappingPoint.name);

                                minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.GetChild(0).transform.position;

                                // keeping reference to collider we are going to snap to
                                snappedCollider = ConnectedSnappingPoint;
                            }
                            else
                            {
                                // Made it so custom snapping points are optional
                                Debug.Log("no child (custom snapping point) found");
                                
                                minDistance = distance;
                                minDistanceChildCollider = SnapPointCollider;
                                minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.position;

                                // keeping reference to collider we are going to snap to
                                snappedCollider = ConnectedSnappingPoint;   
                            }
                            foundSnapLocation = true;
                        }
                    }
                }
                else Debug.Log("CANT SNAP :(   item type is: " + itemType.ToString());
            }
        }

        if (foundSnapLocation == true)
        {
            SnapObjectToLocation(minDistanceChildCollider, minDistanceHitColliderLocation);

            // disabling snap collider to prevent other objects from snapping to it
            snappedCollider.enabled = false;
            Debug.Log("Disabled: " + minDistanceChildCollider.name);
        }
    }


    /* 
    * Enables the collider that had an object snapped to it
    * Will be called when user grabs object ("On Manipulation Started" in 
    * the ManipulationHandler component)
    */
    public void EnableSnappedCollider()
    {
        if (snappedCollider != null)  snappedCollider.enabled = true; 
    }

    private void SnapObjectToLocation(Collider minDistChildCollider, Vector3 minDistHitColliderLocation)
    {
        float AngleSnappingNum = 30f;

        Vector3 currentRotation = transform.eulerAngles;
        //Debug.Log("X Rotation:" + currentRotation.x);
        //Debug.Log("Y Rotation:" + currentRotation.y);
        //Debug.Log("Z Rotation:" + currentRotation.z);

        currentRotation.x = Mathf.Round(currentRotation.x / 90) * 90;
        currentRotation.y = Mathf.Round(currentRotation.y / 90) * 90;
        currentRotation.z = Mathf.Round(currentRotation.z / 90) * 90;
        //transform.eulerAngles = currentRotation;

        /*
        if (currentRotation.x % AngleSnappingNum < (AngleSnappingNum / 2))
        {
            currentRotation.x -= currentRotation.x % AngleSnappingNum;
        }
        else
        {
            currentRotation.x += AngleSnappingNum - (currentRotation.x % AngleSnappingNum);
        }
        if (currentRotation.y % AngleSnappingNum < (AngleSnappingNum / 2))
        {
            currentRotation.y -= currentRotation.y % AngleSnappingNum;
        }
        else
        {
            currentRotation.y += AngleSnappingNum - (currentRotation.y % AngleSnappingNum);
        }
        if (currentRotation.z % AngleSnappingNum < (AngleSnappingNum / 2))
        {
            currentRotation.z -= currentRotation.z % AngleSnappingNum;
        }
        else
        {
            currentRotation.z += AngleSnappingNum - (currentRotation.z % AngleSnappingNum);
        }
        */
        transform.eulerAngles = currentRotation;

        if (minDistance >= 0f)
        {
            transform.position = transform.position + (minDistHitColliderLocation - minDistChildCollider.transform.position);
        }

        snappedOnToObject = true;

        // play corresponding snap sound
        
        if (FindObjectOfType<SoundManager>() != null)
        {
            FindObjectOfType<SoundManager>().PlaySoundAtLocation(itemType, transform.position);
        }
        else
        {
            Debug.Log("SoundManager object not found");
        }
        
        

        // since object is snapped onto another object, enable the other colliders
        enableColliderObjects();
    }


    /* 
     * enabling other colliders (like the slide snapping point) after it is snapped
     * because when it was active, it would cause a bug where it refers to the slide's 
     * snapping verfier script
    */
    private void enableColliderObjects()
    {
        foreach (GameObject colliderObject in colliderObjectsToEnable)
        {
            colliderObject.SetActive(true);
        }
        collidersEnabled = true;
    }

    private void disableColliderObjects()
    {
        foreach (GameObject colliderObject in colliderObjectsToEnable)
        {
            colliderObject.SetActive(false);
        }
        collidersEnabled = false;
    }
}
