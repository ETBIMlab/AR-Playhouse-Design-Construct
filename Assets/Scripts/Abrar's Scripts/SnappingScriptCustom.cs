using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScriptCustom : MonoBehaviour
{
    // Modified version of TJ's script "SnappingScript.cs"
    // This will snap to a specific point and not the origin of the snapping collider

    private List<GameObject> ColliderListObjects = new List<GameObject>();
    public float snappingRadius;

    private ItemInfo.ItemType itemType;
    private int snappingCollidersLayer;

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

        this.itemType = GetComponent<ItemInfo>().itemType;
        snappingCollidersLayer = LayerMask.NameToLayer(LayerMask.LayerToName(0));
        Debug.Log("layer name:" + LayerMask.LayerToName(0));
    }

    private List<GameObject> GetChildSnappingColliders(GameObject obj)
    {
        List<GameObject> childSnappingColliders = new List<GameObject>();

        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.tag.Equals("SnappingCollider"))
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

    public void SnapToLocation()
    {
        snappedOnToObject = false;
        // disable colliders in colliderObjectsToEnable
        if (collidersEnabled == true) disableColliderObjects();

        Debug.Log("--------------------------");
        Debug.Log("SnapToLocation() called from: " + transform.gameObject.name);
        float minDistance = -1f;
        GameObject minDistanceChildCollider = null;
        Vector3 minDistanceHitColliderLocation = new Vector3(0, 0, 0);

        foreach (GameObject SnapPointObject in ColliderListObjects)
        {
            //Debug.Log("Looking at collider: " + SnapPoint.name);
            Collider[] hitColliders = Physics.OverlapSphere(SnapPointObject.transform.position, snappingRadius);

            if (hitColliders.Length == 0)
            {
                Debug.Log("No colliders found in OverlapShere()");
                return;
            }

            foreach (Collider ConnectedSnappingPoint in hitColliders)
            {
                // error checking --------------------------------------------------------------- Remove
                if (ConnectedSnappingPoint.GetComponent<SnappingValidator>() == null)
                {
                    Debug.LogWarning("Could not find SnappingValidator script on snap point collider: Name -> " + ConnectedSnappingPoint.name);
                    continue;
                }
                else
                {
                    Debug.Log("Found SnappingValiditor on " + ConnectedSnappingPoint.name);
                    bool canSnapTemp = ConnectedSnappingPoint.GetComponent<SnappingValidator>().verifySnapCapability(itemType);
                    if (canSnapTemp)
                    {
                        Debug.Log("Snap-able!!!   item is: " + itemType.ToString());
                    }
                    else
                    {
                        Debug.Log("CANT SNAP :(   item is: " + itemType.ToString());
                        return;
                    }
                }

                // NEW: checking if this object can even snap to that snapping point
                bool canSnap = ConnectedSnappingPoint.GetComponent<SnappingValidator>().verifySnapCapability(itemType);
                if (canSnap == true)
                {
                    //Debug.Log(SnapPoint.name + " is close enough to " + ConnectedSnappingPoint.gameObject.name);

                    if (ConnectedSnappingPoint.gameObject.CompareTag("SnappingCollider") && !ColliderListObjects.Contains(ConnectedSnappingPoint.gameObject))
                    {
                        float distance = Vector3.Distance(SnapPointObject.transform.position, ConnectedSnappingPoint.transform.position);
                        if (distance < minDistance || minDistance < 0)
                        {
                            Debug.Log("Found snapping point named:" + SnapPointObject.name);
                            // moved inside the if statement
                            //minDistance = distance;

                            //minDistanceChildCollider = SnapPointObject;   

                            if (ConnectedSnappingPoint.transform.childCount > 0)
                            {
                                minDistance = distance;

                                minDistanceChildCollider = SnapPointObject;

                                // NEW: get child object "custom snap point" from snapping collider game object to get custom snap point
                                Debug.Log("found CHILD snapping point: " + ConnectedSnappingPoint.name);

                                minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.GetChild(0).transform.position;
                            }
                            else
                            {
                                Debug.Log("no child found, count was " + ConnectedSnappingPoint.transform.childCount);
                            }

                            //minDistanceHitColliderLocation = ConnectedSnappingPoint.transform.position;
                        }
                    }
                }
            }
        }

        float AngleSnappingNum = 30f;

        Vector3 currentRotation = transform.eulerAngles;
        //Debug.Log("X Rotation:" + currentRotation.x);
        //Debug.Log("Y Rotation:" + currentRotation.y);
        //Debug.Log("Z Rotation:" + currentRotation.z);
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
        transform.eulerAngles = currentRotation;

        if (minDistance >= 0f)
        {
            transform.position = transform.position + (minDistanceHitColliderLocation - minDistanceChildCollider.transform.position);
        }

        snappedOnToObject = true;

        // since object is snapped onto another object, enable the other colliders
        enableColliderObjects();
    }

    /* enabling other colliders (like the slide snapping point) after it is snapped
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
