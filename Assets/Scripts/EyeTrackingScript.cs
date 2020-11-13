using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingScript : MonoBehaviour
{
    public float capturePositionWaitTime = 1;
    float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTime)
        {
            ActivityLogger logScript = GetComponent<ActivityLogger>();
            gameObject.GetComponent<ActivityLogger>().LogPosition(CoreServices.InputSystem.EyeGazeProvider.HitPosition.ToString());

            Debug.Log("Position: " + CoreServices.InputSystem.EyeGazeProvider.HitPosition);
            nextTime += capturePositionWaitTime;
            //Debug.Log(CoreServices.InputSystem.EyeGazeProvider.HitInfo.)
        }
    }
}
