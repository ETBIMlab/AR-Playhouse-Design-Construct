using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

/*
 * This class will validate whether the object can snap to this collider or snap point
 * 
*/ 
public class SnappingValidator : MonoBehaviour
{
    [Header("Objects that are Allowed to Snap Here")]
    [SerializeField] private bool canSnapHalfFacePanel = false;
    [SerializeField] private bool canSnapFullFacePanel = false;
    [SerializeField] private bool canSnapSlide = false;

    // verifies if object/item can snap at this snap point
    public bool verifySnapCapability(ItemInfo.ItemType itemType)
    {
        Debug.Log("verifying item: " + itemType.ToString());
        switch (itemType)
        {
            case ItemInfo.ItemType.Half_Panel:
                if (canSnapHalfFacePanel == true) return true;
                break;
            case ItemInfo.ItemType.Full_Panel:
                if (canSnapFullFacePanel == true) return true;
                break;
            case ItemInfo.ItemType.Slide:
                if (canSnapSlide == true) return true;
                break;
            default:
                break;
        }

        return false;
    }
}
