using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Paintable : MonoBehaviour
{
    public void ChangeColor(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
   
}
