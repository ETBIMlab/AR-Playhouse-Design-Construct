using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
public class IsWoodOrPlastic : MonoBehaviour
{
    /// <summary>
    /// tells us what the prefab is made of. The wood will be paintable using the paintbuckets,
    /// the plastic will be orderable in different colors, the metal won't be paintable, 
    /// and the paintBuckets will allow us to possibly re-dip the paintbrush. A lot of these have both wood and plastic or metal
    /// so they will need to be painted differently than just wood or just plastic. In Unity, you can make public variables
    /// in script and then toggle them as true or false in the editor so I tried to find as many prefabs as I could and label them.
    /// 
    /// </summary>
   // public Renderer ren;
    public bool hasWood = false;
    public bool hasPlastic = false;
    public bool hasMetal = false;
    public bool isPaintBucket = false;
    public GameObject objectToColor;
    public bool orderableInColor = false;
    public bool paintWithPaint = false;
    public bool paintSomeNotOthers = false;
    public Color paintColor;

    // Start is called before the first frame update
    void Start()
    {
        objectToColor.GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (hasWood == true && hasPlastic == false && hasMetal == false && isPaintBucket == false)
        {
            paintWithPaint = true;
        }
        else if (hasWood == false && hasPlastic == true && hasMetal == false && isPaintBucket == false)
        {
            orderableInColor = true;

        }
        else if (hasWood == false && hasPlastic == false && hasMetal == true && isPaintBucket == false)
        {
            paintWithPaint = false;
            orderableInColor = false;
        }
        else if (hasWood == true && hasPlastic == true && hasMetal == false && isPaintBucket == false)
        {
            paintSomeNotOthers = true; //these will use different methods
        }
        else if (hasWood == true && hasPlastic == false && hasMetal == true && isPaintBucket == false)
        {
            paintSomeNotOthers = true; // this will use different methods because we need to get unity to figure out where the wood is vs metal/plastic
        } 
        else if (hasWood == true && hasPlastic == false && hasMetal == true && isPaintBucket == false)
        {
            paintSomeNotOthers = true; // this will use different methods because we need to get unity to figure out where the wood is vs metal/plastic
        }
        else if (hasWood == false && hasPlastic == false && hasMetal == false && isPaintBucket == true)
        {
            //we need to grab the color from our paint bucket
            paintColor = gameObject.GetComponent<Renderer>().material.color;

        }
    }
}
