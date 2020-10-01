using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;

public class TestScript : MonoBehaviour, IMixedRealityPointerHandler
{

    private int material = 1;
    public Material material1;
    public Material material2;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer Up");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer Dragged");
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Pointer Clicked");
        if(material == 1)
        {
            gameObject.GetComponent<Renderer>().material = material2;
            material = 2;
        } else
        {
            gameObject.GetComponent<Renderer>().material = material1;
            material = 1;
        }
    }

    public void ManipStarted()
    {
        Debug.Log("Manipulation Started");
    }

    public void ManipStop()
    {
        Debug.Log("Manipulation Ended");
    }
}
