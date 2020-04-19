using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture : MonoBehaviour, IMixedRealityGestureHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGestureComplete(InputEventData data)
    {
        Debug.Log("Gesture Complete" + data.ToString());
    }

    public void OnGestureStarted(InputEventData data)
    {
        Debug.Log("Gesture started" + data.ToString());
    }

    public void OnGestureUpdated(InputEventData data)
    {
         Debug.Log("Gesture Updated" + data.ToString());
    }

    public void OnGestureCanceled(InputEventData data)
    {
        Debug.Log("Gesture Canceled" + data.ToString());
    }

    public void OnGestureCompleted(InputEventData data)
    {
        Debug.Log("Gesture Completed" + data.ToString());
    }
}
