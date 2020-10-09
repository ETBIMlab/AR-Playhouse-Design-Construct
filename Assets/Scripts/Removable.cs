using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManipulationHandler))]
[RequireComponent(typeof(Collider))]
public class Removable : MonoBehaviour
{
    public void RemoveObject()
    {
        Destroy(this.gameObject);
    }

    public void Reset()
    {
        //Add 
    }
}
