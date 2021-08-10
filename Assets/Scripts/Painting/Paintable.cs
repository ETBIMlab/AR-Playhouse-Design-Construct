using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Paintable : MonoBehaviour
{
    public void ChangeColor(Material material)
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
        Debug.Log("Change Color did a thing");

    }

}
