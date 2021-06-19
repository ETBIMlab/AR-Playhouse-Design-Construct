using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool hasWood = false;
    public bool hasPlastic = false;
    public bool hasMetal = false;
    public bool isPaintBucket = false;
    private GameObject objectToColor;
    // Start is called before the first frame update
    void Start()
    {
        if(hasWood == true && hasPlastic == false && hasMetal == false && isPaintBucket == false)
        {
            
        }else if (hasWood == false && hasPlastic == true && hasMetal == false && isPaintBucket == false)
        {

        }
        else if (hasWood == false && hasPlastic == false && hasMetal == true && isPaintBucket == false)
        {

        }else if (hasWood == true && hasPlastic == true && hasMetal == false && isPaintBucket == false)
        {

        }else if (hasWood == true && hasPlastic == false && hasMetal == true && isPaintBucket == false)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
