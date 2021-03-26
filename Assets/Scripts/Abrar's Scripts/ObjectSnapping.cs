using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSnapping : MonoBehaviour
{
    // This will snap to a specific point and not the origin of the snapping collider
    #region Private Variables

    // contains the child Colliders of the gameobject which this script is attached to (e.g. face panel object)
    private List<Collider> SnappingColliderList = new List<Collider>(); 

    public float snappingRadius;
    private float minDistance = -1f;

    private ItemInfo itemInfo;
    private ItemInfo.ItemType itemType;
    private ItemInfo.ItemOrientation itemOrientation;

    private Collider snappedCollider = null;

    private bool snappedOnToObject = false;
    private bool collidersEnabled = true;
    #endregion

    [Header("Settings for Enabling other Snap Points After Being Snapped")]
    [SerializeField] List<GameObject> colliderObjectsToEnable;

    // Events
    public UnityEvent OnObjectStartedSnapping;
    public UnityEvent OnObjectFinishedSnapping;

    void Start()
    {
        SnappingColliderList = GetChildSnappingColliders(transform.gameObject);

        InitializeItemInfo();
    }

    /// <summary>
    /// Gets info from ItemInfo component and stores info in relative variables
    /// </summary>
    private void InitializeItemInfo()
    {
        itemInfo = GetComponent<ItemInfo>();
        this.itemType = itemInfo.itemType;
        this.itemOrientation = itemInfo.itemOrientation;
    }

    /// <summary>
    /// Searches and returns a list of colliders (child snapping colliders)
    /// </summary>
    /// <param name="obj">Gameobject that has snapping colliders as children</param>
    /// <returns>List of Colliders</returns>
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

    /// <summary>
    /// Checks, Verfies and then snaps to snap location if valid location is found
    /// </summary>
    public void SnapToLocation()
    {
        // Invoke UnityAction (event)
        OnObjectStartedSnapping.Invoke();

        minDistance = -1f;  // refresh minimum distance

        snappedOnToObject = false;
        bool foundSnapLocation = false;

        // disable colliders in colliderObjectsToEnable
        if (collidersEnabled == true) disableColliderObjects();

        Collider minDistanceChildCollider = null;
        Vector3 minDistanceHitColliderLocation = new Vector3(0, 0, 0);

        // updating item orientation (in case user rotates object)
        itemOrientation =  itemInfo.UpdateItemOrienation(transform.eulerAngles.y);

        #region Comments explaining code below
        /*
         * The first foreach loop, got though each potential snapping point
         * 
         * "Collider[] hitSnappingColliders = Physics.OverlapSphere(SnapPointCollider.transform.position, snappingRadius);"
         * grabs all the colliders in in the sphere projected (restricted by the sphere's radius) and stores them inside the 
         * array of colliders calledhitSnappingColliders
         * 
         * The next foreach loop looks at the colliders the colliders found by the overlap sphere and compares the distance 
         * from the object the user just let go to each snapping point/collider in hitSnappingColliders.
         * 
         * Then it checks if it can snap to that snapping point/collider by verifying with the SnappingValidator component
         * that should be attached to the snapping point gameobject
         * 
         * If a object is valid, then its distance from the object the user has let go of and the snapping point/collider
         * is recorded. The distances are compared and the collider with the smallest distance will be the snap point/collider
         * the object will snap to.
         * 
        */
        #endregion

        foreach (Collider SnapPointCollider in SnappingColliderList)
        {
            Collider[] hitSnappingColliders = Physics.OverlapSphere(SnapPointCollider.transform.position, snappingRadius);

            // if no snap colliders are found, then return
            if (hitSnappingColliders.Length == 0) return;

            foreach (Collider ConnectedSnappingPoint in hitSnappingColliders)
            {
                // if no SnappingValidator is found on the snap point collider, continue to next iteration of foreach loop
                if (ConnectedSnappingPoint.GetComponent<SnappingValidator>() == null) continue; 

                // NEW: checking if this object can even snap to that snapping point
                bool canSnap = ConnectedSnappingPoint.GetComponent<SnappingValidator>().verifySnapCapability(itemType, itemOrientation);

                if (canSnap == true)
                {
                    if (ConnectedSnappingPoint.gameObject.CompareTag("SnappingCollider") && 
                        !SnappingColliderList.Contains(ConnectedSnappingPoint.gameObject.GetComponent<Collider>()))
                    {
                        float distance = Vector3.Distance(SnapPointCollider.transform.position, ConnectedSnappingPoint.transform.position);

                        if (distance < minDistance || minDistance < 0)
                        {
                            Debug.Log("Found snapping point named:" + SnapPointCollider.name);

                            if (ConnectedSnappingPoint.transform.childCount > 0)
                            {
                                #region Comparing Custom Snapping Points

                                minDistance = distance;
                                minDistanceChildCollider = SnapPointCollider;

                                // NEW: get child object "custom snap point" from snapping collider game object to get custom snap point
                                Debug.Log("found CHILD snapping point: " + ConnectedSnappingPoint.name);

                                minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.GetChild(0).transform.position;

                                // keeping reference to collider we are going to snap to
                                snappedCollider = ConnectedSnappingPoint;

                                #endregion
                            }
                            else
                            {
                                #region Comparing Normal Snapping Points
                                // Made it so custom snapping points are optional
                                //Debug.Log("no child (custom snapping point) found");

                                minDistance = distance;
                                minDistanceChildCollider = SnapPointCollider;
                                minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.position;

                                // keeping reference to the snap collider that will be snapped to
                                snappedCollider = ConnectedSnappingPoint;
                                #endregion
                            }

                            foundSnapLocation = true;
                        }
                    }
                }
                // else Debug.Log("CANT SNAP, item type is: " + itemType.ToString());
            }
        }

        if (foundSnapLocation == true)
        {
            SnapObjectToLocation(minDistanceChildCollider, minDistanceHitColliderLocation);

            // disabling snap collider to prevent other objects from snapping to it
            //snappedCollider.enabled = false;
        }
    }


    /* 
    * Enables the collider that had an object snapped to it
    * Will be called when user grabs object ("On Manipulation Started" in 
    * the ManipulationHandler component)
    */
    /// <summary>
    /// Enables the collider that had an object snapped to it,
    /// Will be called when user grabs object ("On Manipulation Started" in 
    /// the ManipulationHandler component)
    /// </summary>
    public void EnableSnappedCollider()
    {
        if (snappedCollider != null)  snappedCollider.enabled = true; 
    }

    private void SnapObjectToLocation(Collider minDistChildCollider, Vector3 minDistHitColliderLocation)
    {
        Vector3 currentRotation = transform.eulerAngles;

        #region Code that Rounds Current Objects Rotation to nearest 90 degree increment
        currentRotation.x = Mathf.Round(currentRotation.x / 90) * 90;
        currentRotation.y = Mathf.Round(currentRotation.y / 90) * 90;
        currentRotation.z = Mathf.Round(currentRotation.z / 90) * 90;
        #endregion

        transform.eulerAngles = currentRotation;

        if (minDistance >= 0f)
        {
            transform.position = transform.position + (minDistHitColliderLocation - minDistChildCollider.transform.position);
        }

        snappedOnToObject = true;

        // since object is snapped onto another object, enable the other colliders
        enableColliderObjects();

        OnObjectFinishedSnapping.Invoke();
    }


    /// <summary>
    /// Enable additional snap collider on the current object
    /// </summary>
    private void enableColliderObjects()
    {
        foreach (GameObject colliderObject in colliderObjectsToEnable)
        {
            colliderObject.SetActive(true);
        }
        collidersEnabled = true;
    }

    /// <summary>
    /// Disable additional snap collider on the current object
    /// </summary>
    private void disableColliderObjects()
    {
        foreach (GameObject colliderObject in colliderObjectsToEnable)
        {
            colliderObject.SetActive(false);
        }
        collidersEnabled = false;
    }
}
