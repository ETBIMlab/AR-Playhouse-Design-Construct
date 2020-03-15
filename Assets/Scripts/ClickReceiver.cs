using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class ClickReceiver : MonoBehaviour, IMixedRealityPointerHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(MixedRealityPointerEventData eData)
    {
        Debug.Log("Pointer Down");
        transform.localScale = new Vector3(1, 1, 2);
    }

    public void OnPointerUp(MixedRealityPointerEventData eData)
    {
        Debug.Log("Pointer Up");
        transform.localScale = new Vector3(1, 2, 1);
    }

    public void OnPointerClicked(MixedRealityPointerEventData eData)
    {
        Debug.Log("Pointer Clicked");
        transform.localScale = new Vector3(2, 1, 1);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eData)
    {
        Debug.Log("Pointer Dragged");
        transform.localScale = new Vector3(3, 3, 3);
    }
}
