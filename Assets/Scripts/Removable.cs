using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ManipulationHandler))]
[RequireComponent(typeof(Collider))]
public class Removable : MonoBehaviour
{

    public UnityEvent OnRemovalStart;
    public UnityEvent OnRemovalEnd;

    public void RemoveObject()
    {
        // Start action tracker func
        OnRemovalStart.Invoke();
        
        // Interaction
        Destroy(this.gameObject);

        // End action tracker func
        OnRemovalEnd.Invoke();
    }

    public void Reset()
    {
        //Add 
    }
}
