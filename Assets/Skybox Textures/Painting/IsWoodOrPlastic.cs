using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
public class IsWoodOrPlastic : MonoBehaviour
{
    /// <summary>
    /// RETIRED
    /// </summary>
    /// / // public Renderer ren;
    public bool hasWood = false;
    public bool hasPlastic = false;
    public bool hasMetal = false;
    public bool isPaintBucket = false;
    //public GameObject objectToColor;
    //public bool orderableInColor = false;
   // public bool paintWithPaint = false;
    //many of the objects are seperable  into wood, metal, and plastic. Not so with the following objects so bools: 
  //  public bool isTubeBridge = false;
    //public bool isChimesSmall = false;
    //public bool isChimesLarge = false;
    //public bool isSingleSlide = false;
    //public bool isDoubleSlide = false;
    //public bool isTubeSlide = false;
    //public bool isFirepole = false;
    //public bool isCoilClimber = false;
    //public bool isStairs = false;
    public Material paintColorMaterial;
    //Color Wheel bc ROYGBIV isn't completely built into unity
    /*private Color orderRed = Color.red;
    private Color orderOrange = new Vector4(255, 165, 0, 1);
    private Color orderYellow = Color.yellow;
    private Color orderGreen = Color.green;
    private Color orderBlue = Color.blue;
    private Color orderIngdigo = new Vector4(75, 0, 130, 1);
    private Color orderViolet = new Vector4(238, 130, 238, 1);
    private Color orderGrey = Color.grey;
    private Color orderWhite = Color.white;*/
    // Start is called before the first frame update
    void Start()
    {
        // objectToColor.GetComponent<Renderer>();
       /* if (hasWood == true)
        {
            if (gameObject.GetComponent<Paintable> != null)
            {
                gameObject.AddComponent<Paintable>();
            }
        }
        else if(isPaintBucket == true)
        {
            if (gameObject.GetComponent<isPaintBucket> != null)
            {
                gameObject.AddComponent<isPaintBucket>();

            }
        }
        /* else if (hasWood == false && hasPlastic == false && hasMetal == true && isPaintBucket == false)
       {
           //paintWithPaint = false;
          // orderableInColor = false;
       }
       else if (isTubeBridge == true || isChimesLarge == true || isChimesSmall == true || isSingleSlide == true
           || isDoubleSlide == true || isTubeSlide == true || isCoilClimber == true || isFirepole == true || isStairs == true)
       {
           orderableInColor = true; //if you add onto the order in color script, hard code there and then add to the bools in this if / else
       }

     else if (hasWood == false && hasPlastic == false && hasMetal == false && isPaintBucket == true)
       {
           //we need to grab the color from our paint bucket
           paintColorMaterial = gameObject.GetComponent<MeshRenderer>().materials[2];

       }
        */
    }

}
