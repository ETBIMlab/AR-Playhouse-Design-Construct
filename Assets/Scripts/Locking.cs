﻿using System.Collections;
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
                    toBeLockedObject = NearbyLockingPoint.transform.root.gameObject;
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
            Debug.Log(toBeLockedObject.name);
            lockedObject.GetComponent<Lockable>().addLock(gameObject);
        }
    }

    public void UnlockObject()
    {
        if(lockedObject != null)
        {
            lockedObject.GetComponent<Lockable>().removeLock(gameObject);
        }
    }
}