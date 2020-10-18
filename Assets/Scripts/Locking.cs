using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locking : MonoBehaviour
{
    public float snappingRadius;
    private GameObject lockedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SnapToLockLocation()
    {
        Debug.Log("SnapToLockLocation() called");



        GameObject toBeLockedObject = null;
        float minDistance = -1f;
        Vector3 minDistanceHitColliderLocation = new Vector3(0, 0, 0);

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, snappingRadius);
        foreach (Collider NearbyLockingPoint in hitColliders)
        {
            if (NearbyLockingPoint.gameObject.CompareTag("LockingCollider"))
            {
                float distance = Vector3.Distance(gameObject.transform.position, NearbyLockingPoint.transform.position);
                if (distance < minDistance || minDistance < 0)
                {
                    minDistance = distance;
                    minDistanceHitColliderLocation = NearbyLockingPoint.transform.position;
                    //toBeLockedObject = NearbyLockingPoint.transform.root.gameObject;

                    // go up only one parent level (changed since objects are now in a gameobject
                    // containing all the playhouse pieces)
                    /*
                     * TODO: make for loop/function that traverses up the gameobject hierarchy
                     * until it finds the object(contruction piece) or finds nothing (at root) and return
                     * maybe use a object tag?
                     */
                    if (NearbyLockingPoint.transform.parent.parent.gameObject != null)
                    {
                        toBeLockedObject = NearbyLockingPoint.transform.parent.parent.gameObject;
                    }
                    else { toBeLockedObject = NearbyLockingPoint.transform.root.gameObject; }   // .root references the gameobject that holds all the pieces (not wanted)
                    
                }
            }
        }
        if(minDistance >= 0)
        {
            transform.position = minDistanceHitColliderLocation;
        }
        lockedObject = toBeLockedObject;
        if(lockedObject != null && lockedObject.GetComponent<Lockable>() != null)
        {
            Debug.Log(toBeLockedObject.name + " Will be locked");
            lockedObject.GetComponent<Lockable>().addLock(gameObject);
        }

        // debugging REMOVE
        if (lockedObject == null) Debug.Log("lockedObject is null..");
        else Debug.Log("lockedObject is " + lockedObject.name);

        if (lockedObject.GetComponent<Lockable>() == null) Debug.Log(lockedObject.name + " does not have Lockable component..");

    }

    public void UnlockObject()
    {
        if(lockedObject != null)
        {
            Debug.Log("Unlocked object");
            lockedObject.GetComponent<Lockable>().removeLock(gameObject);
        }
    }
}
