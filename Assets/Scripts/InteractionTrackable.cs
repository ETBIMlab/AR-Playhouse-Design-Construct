using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditorInternal;
#endif
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
    private UnityAction StartRemovalAction;
    private UnityAction EndRemovalAction;

    // Start is called before the first frame update
    void Start()
    {
        // Required for all logging actions
        ActionLogger = GameObject.Find("ActivityLogger");
        if (ActionLogger == null)
        {
            throw new Exception("No Action Logger Available");
        }
        else
        {
            Debug.Log("ActionLogger Found");
        }

        // If trackMovement or trackRotation is true, check for ManipulationHandler
        if (gameObject.GetComponent<ManipulationHandler>() == null)
        {
            Debug.Log("Null Manipulation Handler");
#if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a ManipulationHandler component", "Add ManipulationHandler", "Stop Tracking Movement and/or Rotation"))
            {
                gameObject.AddComponent(typeof(ManipulationHandler));
            }
            else
            {
                trackMovement = false;
                trackRotation = false;
            }
#endif
        }
        if (trackMovement || trackRotation) {
            if (gameObject.GetComponent<ManipulationHandler>() == null)
            {
                Debug.Log("Null Manipulation Handler");
#if UNITY_EDITOR
                if (UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a \"ManipulationHandler component\"", "Add \"ManipulationHandler\"", "Stop Tracking Movement and/or Rotation"))
                {
                    gameObject.AddComponent(typeof(ManipulationHandler));
                }
                else
                {
                    trackMovement = false;
                    trackRotation = false;
                }
#endif
            }
            ManipulationHandler manipHandler = gameObject.GetComponent<ManipulationHandler>();
            StartMovementAction += StartMovement;
            EndMovementAction += EndMovement;
            manipHandler.OnManipulationStarted.AddListener(StartMovementAction);
            manipHandler.OnManipulationEnded.AddListener(EndMovementAction);
        }
        if (trackRemoval)
        {
            if(gameObject.GetComponent<Removable>() == null)
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a \"Removable\" component", "Add \"Removable\"", "Stop Tracking Removal"))
                {
                    gameObject.AddComponent(typeof(Removable));
                }
                else
                {
                    trackRemoval = false;
                }
#endif
            }
            Removable removableComponent = gameObject.GetComponent<Removable>();
            StartRemovalAction += StartRemoval;
            EndRemovalAction += EndRemoval;
            removableComponent.OnRemovalStart.AddListener(StartRemovalAction);
            removableComponent.OnRemovalEnd.AddListener(EndRemovalAction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        if((trackMovement || trackRotation) && gameObject.GetComponent<ManipulationHandler>() == null)
        {
            Debug.Log("Null Manipulation Handler");
#if UNITY_EDITOR
            if(UnityEditor.EditorUtility.DisplayDialog("Missing required component", "Trackable object requires a ManipulationHandler component", "Add ManipulationHandler", "Stop Tracking Movement and/or Rotation"))
            {
                gameObject.AddComponent(typeof(ManipulationHandler));
                trackMovement = true;
                trackRotation = true;
            } else
            {
                trackMovement = false;
                trackRotation = false;
            }
#endif
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
        if (!transform.position.Equals(storedPosition) && trackMovement)
        {
            message += "User Moved " + gameObject.name + " from " + storedPosition + " to " + transform.position + ".";
        }
        if (!transform.rotation.Equals(storedRotation) && trackRotation)
        {
            message += "User Rotated " + gameObject.name + " from " + storedRotation + " to " + transform.rotation + ".";
        }
        ActionLogger.SendMessage("LogItem", message);
    }
#endregion

#region RemovalTracking
    private void StartRemoval()
    {
        string message = gameObject.name + " was removed from scene";
        ActionLogger.SendMessage("LogItem", message);
    }

    private void EndRemoval()
    {
        
    }
#endregion RemovalTracking

#region SnappingTracking

#endregion
}
