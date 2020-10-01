using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
public class CubeBehavior : MonoBehaviour, IMixedRealityGestureHandler, IMixedRealityFocusHandler
{
    public bool isRotating;
    public Vector3 Vect;
    public float speed;

    public void OnFocusEnter(FocusEventData eventData)
    {
        this.isRotating = true;
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        this.isRotating = false;
    }

    public void OnGestureCanceled(InputEventData eventData)
    {
        Debug.Log("Gesture canceled: " + eventData.ToString());
    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        Debug.Log("Gesture completed: " + eventData.ToString());
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        Debug.Log("Gesture Started: " + eventData.ToString());
    }

    public void OnGestureUpdated(InputEventData eventData)
    {
        Debug.Log("Gesture updated: " + eventData.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Vect = new Vector3();
        Debug.Log("TESTTTTTTTT");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating) transform.Rotate(this.Vect * Time.deltaTime * speed);
    }

       
}
