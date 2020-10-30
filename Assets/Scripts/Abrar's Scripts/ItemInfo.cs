using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class contains itemtype and maybe other things later..
public class ItemInfo : MonoBehaviour
{
    public double itemPrice = 0;
    public enum ItemType
    {
        Half_Panel,
        Full_Panel,
        Slide
        // add more item types here
    }

    public ItemType itemType;
    

    // Problem: Orientation/rototion of each object is different, which makes it very confusing to set up a orientation
    // Model needs to be fixed
    
    // For storing info on the objects orientaion (direction it is facing)
    public enum ItemOrientation
    {
        NorthSouth,
        EastWest
    }

    public ItemOrientation itemOrientation;
    

}
