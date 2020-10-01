using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;


public class Pointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PointerHandler pointerHandler = gameObject.AddComponent<PointerHandler>();
        pointerHandler.OnPointerClicked.AddListener((evt) => Debug.Log("Tap Detected " + Time.time));
        // Make this a global input handler, otherwise this object will only receive events when it has input focus
        CoreServices.InputSystem.RegisterHandler<IMixedRealityPointerHandler>(pointerHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
