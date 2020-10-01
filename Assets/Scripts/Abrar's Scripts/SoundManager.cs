using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [Range(0f, 2f)]
    [SerializeField] private float soundVolume;

    [SerializeField] private AudioClip woodSnapSound;

    // saving reference so we dont have to get the refernce everytime we play a sound
    const ItemInfo.ItemType full_panel = ItemInfo.ItemType.Full_Panel;
    const ItemInfo.ItemType half_panel = ItemInfo.ItemType.Half_Panel;
    const ItemInfo.ItemType slide = ItemInfo.ItemType.Slide;

    // determines what sound to play at gievn location
    public void PlaySoundAtLocation(ItemInfo.ItemType itemType, Vector3 position)
    {
        switch (itemType)
        {
            case full_panel:
                AudioSource.PlayClipAtPoint(woodSnapSound, position, soundVolume);
                break;
            case half_panel:
                AudioSource.PlayClipAtPoint(woodSnapSound, position, soundVolume);
                break;
            case slide:
                // play slide sound here
                break;

            default:
                break;
        }
    }
}
