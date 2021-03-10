using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityScript.Steps;
using Microsoft.MixedReality.Toolkit.UI;

[RequireComponent(typeof(ManipulationHandler))]
public class SnappingScript : MonoBehaviour
{
    private List<GameObject> ColliderList = new List<GameObject>();
    public float snappingRadius;

    private void Reset()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        ColliderList = GetChildSnappingColliders(transform.gameObject);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, 10));

    }

    private List<GameObject> GetChildSnappingColliders(GameObject obj)
    {
        List<GameObject> childSnappingColliders = new List<GameObject>();

        foreach(Transform child in obj.transform)
        {
            if(child.gameObject.tag.Equals("SnappingCollider"))
            {
                //Debug.Log("Adding " + child.gameObject.name + " to colliders of " + transform.gameObject.name);
                childSnappingColliders.Add(child.gameObject);
            }
            else
            {
                childSnappingColliders.AddRange(GetChildSnappingColliders(child.gameObject));
            }
        }
        return childSnappingColliders;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SnapToLocation()
    {
        float minDistance = -1f;
        GameObject minDistanceChildCollider = null;
        Vector3 minDistanceHitColliderLocation = new Vector3(0,0,0);
        foreach(GameObject SnapPoint in ColliderList)
        {
            //Debug.Log("Looking at collider: " + SnapPoint.name);
            Collider[] hitColliders = Physics.OverlapSphere(SnapPoint.transform.position, snappingRadius);
            foreach(Collider ConnectedSnappingPoint in hitColliders)
            {
                //Debug.Log(SnapPoint.name + " is close enough to " + ConnectedSnappingPoint.gameObject.name);
                
                if (ConnectedSnappingPoint.gameObject.CompareTag("SnappingCollider") && !ColliderList.Contains(ConnectedSnappingPoint.gameObject))
                {
                    float distance = Vector3.Distance(SnapPoint.transform.position, ConnectedSnappingPoint.transform.position);
                    if (distance < minDistance || minDistance < 0)
                    {
                        minDistance = distance;
                        minDistanceChildCollider = SnapPoint;
                        minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.position;
                    }
                }
            }
        }

        float AngleSnappingNum = 30f;

        Vector3 currentRotation = transform.eulerAngles;
        Debug.Log("X Rotation:" + currentRotation.x);
        Debug.Log("Y Rotation:" + currentRotation.y);
        Debug.Log("Z Rotation:" + currentRotation.z);
        if (currentRotation.x% AngleSnappingNum < (AngleSnappingNum/2))
        {
            currentRotation.x -= currentRotation.x % AngleSnappingNum;

        }
        else
        {
            currentRotation.x += AngleSnappingNum - (currentRotation.x % AngleSnappingNum);
        }
        if (currentRotation.y % AngleSnappingNum < (AngleSnappingNum/2))
        {
            currentRotation.y -= currentRotation.y % AngleSnappingNum;

        }
        else
        {
            currentRotation.y += AngleSnappingNum - (currentRotation.y % AngleSnappingNum);
        }
        if (currentRotation.z % AngleSnappingNum < (AngleSnappingNum/2))
        {
            currentRotation.z -= currentRotation.z % AngleSnappingNum;

        }
        else
        {
            currentRotation.z += AngleSnappingNum - (currentRotation.z % AngleSnappingNum);
        }
        transform.eulerAngles = currentRotation;

        if (minDistance >= 0f)
        {
            transform.position = transform.position + (minDistanceHitColliderLocation - minDistanceChildCollider.transform.position);
        }
        
    }
}
