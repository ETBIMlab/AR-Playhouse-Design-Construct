using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedObjects : MonoBehaviour
{
    /// <summary>
    /// so these three objects you can't grab the different components like you can with the bell prefab (based on how they were uploaded)
    /// We need to be able to order them in different colors so I made prefabs in the standard colors: Red, Yellow, Blue, and Green
    /// This script is going to allow users to order them in different colors but it's different than OrderInColor by inidividually assigning each command to an object, not an object in a color.
    /// </summary>
    //Tube Bridge
    GameObject AssignToYellowTubeBridge;
    GameObject AssignToRedTubeBridge;
    GameObject AssignToGreenTubeBridge;
    GameObject AssignToBlueTubeBridge;
    //Small Chimes: Chimes Panel 5' by 3'6in 
    GameObject AssignToYellowSmallChimes;
    GameObject AssignToRedSmallChimes;
    GameObject AssignToGreenSmallChimes;
    GameObject AssignToBlueSmallChimes;
    //Large Chimes: Chimes Panel 5' by 5'4in
    GameObject AssignToYellowBigChimes;
    GameObject AssignToRedBigChimes;
    GameObject AssignToGreenBigChimes;
    GameObject AssignToBlueBigChimes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
