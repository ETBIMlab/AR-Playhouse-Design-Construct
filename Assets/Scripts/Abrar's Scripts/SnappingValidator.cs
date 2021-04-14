//using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;


/// <summary>
/// This class focuses on validating if objects can snap to this collider/snap point
/// </summary>
public class SnappingValidator : MonoBehaviour
{
    #region Serializable Variables
    [Header("Objects that are Allowed to Snap Here")]
    [SerializeField] private bool canSnapHalfFacePanel = false;
    [SerializeField] private bool canSnapFullFacePanel = false;
    [SerializeField] private bool canSnapSlide = false;
    [Tooltip("canSnapFullFacePanelAttachment")]
    [SerializeField] private bool canSnapFullFacePanelAttachment = false;
    [SerializeField] private bool canSnapLadder = false;
    [SerializeField] private bool canSnapAttachment = false;
    [SerializeField] private bool canSnapPlank = false;

    [Header("Object Orientation")]
    [Tooltip("If set to false, orientation will not matter")]
    [SerializeField] private bool isOrientationSpecific = false;
    #endregion

    // choose what orientation is allowed for snapping for this snap collider in inspector
    public ItemInfo.ItemOrientation allowedItemOrientation;   

    // verifies if object/item can snap at this snap point
    public bool verifySnapCapability(ItemInfo.ItemType itemType, ItemInfo.ItemOrientation itemOrientation)
    {
        //Debug.Log("verifying item: " + itemType.ToString());
        switch (itemType)
        {
            case ItemInfo.ItemType.Half_Panel:
                if (canSnapHalfFacePanel == true) // checking if the snap collider allows HalfFacePanels to snap here
                {
                    if (isOrientationSpecific)  // checking if the snap collider cares about orientation
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;

            case ItemInfo.ItemType.Full_Panel:
                if (canSnapFullFacePanel == true)
                {
                    if (isOrientationSpecific)  
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;

            case ItemInfo.ItemType.Slide:
                if (canSnapSlide == true)
                {
                    if (isOrientationSpecific)
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;

            case ItemInfo.ItemType.Full_Panel_Attachment:
                if (canSnapFullFacePanelAttachment == true)
                {
                    if (isOrientationSpecific)
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;
            case ItemInfo.ItemType.Ladder:
                if (canSnapLadder == true)
                {
                    if (isOrientationSpecific)
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;
            case ItemInfo.ItemType.Attachment:
                if (canSnapAttachment == true)
                {
                    if (isOrientationSpecific)
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;
            case ItemInfo.ItemType.Plank:
                if (canSnapPlank == true)
                {
                    if (isOrientationSpecific)
                    {
                        return verifyOrientation(itemOrientation);
                    }
                    else return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    /// <summary>
    /// Decides whether the object can snap to the location
    /// </summary>
    /// <param name="itemOrientation"></param>
    /// <returns>returns boolean if object can snap to location</returns>
    private bool verifyOrientation(ItemInfo.ItemOrientation itemOrientation)
    {
        if (allowedItemOrientation == itemOrientation)
        {
            return true;
        }
        else
        {
            Debug.Log("not allowed: allowed = " + allowedItemOrientation.ToString() + ", items = " + itemOrientation.ToString());
            return false;
        }
    }
}
