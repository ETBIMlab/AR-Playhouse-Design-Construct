using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour
{
    private List<GameObject> ColliderList = new List<GameObject>();
    public float snappingRadius;

    // Start is called before the first frame update
    void Start()
    {
        ColliderList = GetChildSnappingColliders(transform.gameObject);

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
        Vector3 minDistanceChildColliderLocation = new Vector3(0,0,0);
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
                        minDistanceChildColliderLocation = SnapPoint.transform.position;
                        minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.position;
                    }
                }
            }
        }
        Debug.Log("Min Distance is: " + minDistance);
        transform.position = transform.position + (minDistanceHitColliderLocation - minDistanceChildColliderLocation);
    }
}
