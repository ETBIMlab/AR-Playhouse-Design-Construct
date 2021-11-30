using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCatalog : MonoBehaviour
{
    public Material[] catalog; //this holds all 19 pages of the catalog as materials
    public int i = 0; //this is my counter
    public GameObject canvas; //this is what it's displayed on
    public void incI()
    {
        if(i < catalog.Length)
        {
            i++;
            Debug.Log(i);
            catalog[i] = canvas.gameObject.GetComponent<Renderer>().material;
        }
        else if(i >= catalog.Length)
        {
            i = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
