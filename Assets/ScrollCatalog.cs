using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
public class ScrollCatalog : MonoBehaviour
{
    public Material[] catalog; //this holds all 19 pages of the catalog as materials
    public float i = 0, oldRange = 1, oldMax = 1, oldMin = 0; //this is my counter
    private int newMin = 18, newRange = -18, newMax = 0, newValue; //old range = oldMax-oldMin, new range = (newMax-newMin)...the new range is our array, there are 19 pages but arrays start at 0 so 0-18
    public GameObject canvas; //this is what it's displayed on
    public TextMeshPro textMesh = null;
    Renderer thisRend;
    public void incPage()
    {
        //Debug.Log("I made it to scroll catalog"); //this works 

        i = float.Parse(textMesh.text);
        //i = i * 10;
        //Debug.Log(i); this works
        newValue = (int)(((i * newRange) / oldRange)) + newMin; //this formula I found on ye old google allows me to convert from the slider range to the array range
        //Debug.Log(newValue); this works
        //catalog[newValue] = canvas.gameObject.GetComponent<Renderer>().material;
        thisRend.material = catalog[newValue];

    }
    // Start is called before the first frame update
    void Start()
    {
        thisRend = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
