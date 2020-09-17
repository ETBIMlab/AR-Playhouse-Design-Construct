using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class contains itemtype and maybe other things later..
public class ItemInfo : MonoBehaviour
{
    public enum ItemType
    {
        Half_Panel,
        Full_Panel,
        Slide
        // add more item types here
    }

    // readonly outside of this class
    public ItemType itemType;

}
