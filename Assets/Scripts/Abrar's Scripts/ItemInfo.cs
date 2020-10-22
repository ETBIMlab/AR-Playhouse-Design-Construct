using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

// class contains itemtype and maybe other things later..
public class ItemInfo : MonoBehaviour
{
    public enum ItemType
    {
        Half_Panel,
        Full_Panel,
        Slide,
        Full_Panel_Attachment
        // add more item types here
    }

    public ItemType itemType;

    public bool hasDifferentOriginRotation = false;
    

    // Problem: Orientation/rototion of each object is different, which makes it very confusing to set up a orientation
    // Model needs to be fixed
    
    // For storing info on the objects orientaion (direction it is facing)
    public enum ItemOrientation
    {
        NorthSouth,
        EastWest
    }

    public ItemOrientation itemOrientation;
    
    public ItemOrientation UpdateItemOrienation(float yRotation)
    {
        //Debug.Log("orginal y angle is: " + yRotation);

        // round to nearest 90 degree increment
        yRotation = Mathf.Round(yRotation / 90) * 90;
        //Debug.Log("rounded y angle is: " + yRotation);

        
        while (yRotation >= 360 || yRotation < 0)
        {
            yRotation = SimplifyRotation(yRotation);
            //Debug.Log("Angle simplified: " + yRotation);
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
                break;

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
                break;

            default:
                Debug.Log("ItemOrienation() failed");
                return itemOrientation;
                break;
        }
    }

    // simplifies rotation (e.g. if angle was 450 degrees, it would become 90 degrees)
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
