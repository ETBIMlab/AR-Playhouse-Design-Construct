using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
/// <summary>
/// RETIRED
/// </summary>
// class contains itemtype and maybe other things later..
public class ItemInfo : MonoBehaviour
{
    /// <summary>
    /// Stores info on what type of object it is
    /// </summary>
 
    public double itemPrice = 0; 
    public int itemInstallTime = 0;
    public string itemName = "";
    public int itemFun = 0;
    public enum ItemType
    {
        Half_Panel,
        Full_Panel,
        Slide,
        Full_Panel_Attachment,
        Ladder,
        Attachment,
        Plank
        // add more item types here
    }

    public ItemType itemType;

    // for objects with a different origin rotation (remove once all individual models have the origins/rotations fixed)
    public bool hasDifferentOriginRotation = false;

    // FIX:
    // Problem: Orientation/rototion of each object is different, which makes it very confusing to set up a orientation
    // Individual Models needs to be fixed

    /// <summary>
    /// Stores info on the objects orientaion (direction it is facing)
    /// </summary>
    public enum ItemOrientation
    {
        NorthSouth,
        EastWest
    }

    public ItemOrientation itemOrientation;
    
    /// <summary>
    /// Updates item's orientation
    /// </summary>
    /// <param name="yRotation">Object's y rotation component</param>
    /// <returns>Returns updated object's ItemOrientaion value</returns>
    public ItemOrientation UpdateItemOrienation(float yRotation)
    {
        // round to nearest 90 degree increment
        yRotation = Mathf.Round(yRotation / 90) * 90;

        while (yRotation >= 360 || yRotation < 0)
        {
            yRotation = SimplifyRotation(yRotation);
        }

        switch (itemType)
        {
            // TODO: ADD MORE TYPES and wait until all object pieces have the same rotation (prefabs)
            case ItemType.Full_Panel:
                //Debug.Log("Simplified/rounded angle is: " + yRotation);

                // for handling full panels with different origin rotation
                if (hasDifferentOriginRotation) { yRotation += 90; }

                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                }
                return itemOrientation;

            case ItemType.Half_Panel:
                if (hasDifferentOriginRotation) { yRotation += 90;  }

                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                }
                return itemOrientation;

            case ItemType.Full_Panel_Attachment:
                if (hasDifferentOriginRotation) { yRotation += 90; }

                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                }
                return itemOrientation;

            case ItemType.Slide:
                //Debug.Log("Simplified/rounded angle is: " + yRotation);

                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                }
                return itemOrientation;
            case ItemType.Ladder:
                //Debug.Log("Simplified/rounded angle is: " + yRotation);

                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                }
                return itemOrientation;

            case ItemType.Attachment:
                //Debug.Log("Simplified/rounded angle is: " + yRotation);
                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                }
                return itemOrientation;

            case ItemType.Plank:
                //Debug.Log("Simplified/rounded angle is: " + yRotation);
                switch (yRotation)
                {
                    case 90:
                    case 270:
                        itemOrientation = ItemOrientation.EastWest;
                        break;
                    case 0:
                    case 180:
                        itemOrientation = ItemOrientation.NorthSouth;
                        break;
                }
                return itemOrientation;

            default:
                Debug.Log("ItemOrienation() failed");
                return itemOrientation;
        }
    }

    /// <summary>
    /// Simplifies rotation to a value within: 0 - 360
    /// </summary>
    /// <example>
    /// if angle was 450 degrees, it would become 90 degrees
    /// </example>
    /// <param name="rotation">rotation value to simplify</param>
    /// <returns>simplified rotation value</returns>
    private float SimplifyRotation(float rotation)
    {
        if (rotation >= 360f)
        {
            rotation -= 360;
        }
        else if (rotation < 0f)
        {
            rotation += 360;
        }

        return rotation;
    }

}
