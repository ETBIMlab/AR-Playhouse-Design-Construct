using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrackable : MonoBehaviour


{
    public GameObject ActionLogger;

    #region TrackingChecks
    public Boolean trackMovement = true;
    public Boolean trackRotation = true;
    public Boolean trackSnapping = true;
    public Boolean trackCollisions = true;
    public Boolean trackVoiceCommands = true;
    public Boolean trackRemoval = true;
    public Boolean trackOrdering = true;
    #endregion

    private UnityAction<ManipulationEventData> StartMovementAction;
    private UnityAction<ManipulationEventData> EndMovementAction;

    // Start is called before the first frame update
    void Start()
    {
        ActionLogger = GameObject.Find("ActivityLogger");
        if (ActionLogger == null)
        {
            throw new Exception("No Action Logger Available");
        }
        else
        {
            Debug.Log("ActionLogger Found");
        }
        if (gameObject.GetComponent<ManipulationHandler>() == null)
        {
            Debug.Log("Null Manipulation Handler");
            if (UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a ManipulationHandler component", "Add ManipulationHandler", "Stop Tracking Movement and/or Rotation"))
            {
                gameObject.AddComponent(typeof(ManipulationHandler));
            }
            else
            {
                trackMovement = false;
                trackRotation = false;
            }
        }
        if (trackMovement) {
            ManipulationHandler manipHandler = gameObject.GetComponent<ManipulationHandler>();
            StartMovementAction += StartMovement;
            EndMovementAction += EndMovement;
            manipHandler.OnManipulationStarted.AddListener(StartMovementAction);
            manipHandler.OnManipulationEnded.AddListener(EndMovementAction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        if(gameObject.GetComponent<ManipulationHandler>() == null)
        {
            Debug.Log("Null Manipulation Handler");
            if(UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a ManipulationHandler component", "Add ManipulationHandler", "Stop Tracking Movement and/or Rotation"))
            {
                gameObject.AddComponent(typeof(ManipulationHandler));
            } else
            {
                trackMovement = false;
                trackRotation = false;
            }
        }
    }


    #region MovementTracking
    private Vector3 storedPosition;
    private Quaternion storedRotation;

    private void StartMovement(ManipulationEventData eData)
    {
        ActionLogger = GameObject.Find("ActivityLogger");
        if (ActionLogger == null)
        {
            Debug.Log("ActionLogger Not Found");
        }
        else
        {
            Debug.Log("ActionLogger Found");
        }
        storedPosition = transform.position;
        storedRotation = transform.rotation;
    }

    private void EndMovement(ManipulationEventData eData)
    {
        string message = "";
        if (!transform.position.Equals(storedPosition))
        {
            message += "User Moved " + gameObject.name + " from " + storedPosition + " to " + transform.position + ".";
        }
        if (!transform.rotation.Equals(storedRotation))
        {
            message += "User Rotated " + gameObject.name + " from " + storedRotation + " to " + transform.rotation + ".";
        }
        ActionLogger.SendMessage("LogItem", message);
    }
    #endregion

    #region SnappingTracking

    #endregion
}
