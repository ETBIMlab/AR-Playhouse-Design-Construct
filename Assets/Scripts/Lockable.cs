using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class Lockable : MonoBehaviour
{
    private int numLocks = 0;
    private List<GameObject> lockedBy = new List<GameObject>();
    private bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool getIsLocked()
    {
        return isLocked;
    }
    public void addLock(GameObject locker)
    {
        Debug.Log("Adding a lock");
        numLocks++;
        lockedBy.Add(locker);
        isLocked = true;
        gameObject.GetComponent<ManipulationHandler>().enabled = false;

    }

    public void removeLock(GameObject locker)
    {
        Debug.Log("Removing Lock");
        numLocks--;
        lockedBy.Remove(locker);
        isLocked = false;
        gameObject.GetComponent<ManipulationHandler>().enabled = true;
        /*if (numLocks <= 0)
        {
            numLocks = 0;
            gameObject.GetComponent<ManipulationHandler>().enabled = true ;
        }*/
    }
}
