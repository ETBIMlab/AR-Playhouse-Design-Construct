using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldButtonScript : MonoBehaviour
{
    /// <summary>
    /// this is from a tutorial about pop up buttons used on the workbench
    /// Link to tutorial: https://youtu.be/tHEG95vrO_Q 
    /// </summary>
    [SerializeField]
    private Transform targetTransform;

    private RectTransform rectTransform;
    private Image image;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    // Update is called once per frame
    private void Update()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPoint;
        var viewportPoint = Camera.main.WorldToViewportPoint(targetTransform.position);
        var distanceFromCenter = Vector2.Distance(viewportPoint, Vector2.one * 0.5f);

        var show = distanceFromCenter < 0.3f;
        if (screenPoint.z < 0.0f) show = false;
        else image.enabled = show;


    }
}
