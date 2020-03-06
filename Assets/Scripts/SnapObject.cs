using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour
{

    public bool grabbed;
    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGrabbedToFalse()
    {
        grabbed = false;
        Debug.Log("grabbedtofalse = " + grabbed);
    }

    public void setGrabbedToTrue()
    {
        grabbed = true;
        Debug.Log("grabbedtotrue = " + grabbed);
    }

}
