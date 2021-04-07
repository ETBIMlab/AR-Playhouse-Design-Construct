using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class Fixed_Tool : MonoBehaviour
{

    public GameObject tool;
    public GameObject camera;
    public GameObject toolParent;

    public float cameraDistOffsetY = 0;
    public float cameraDistOffsetZ = 0;

    public float initialToolRotationX = 1;
    public float initialToolRotationY = 1;
    public float initialToolRotationZ = 1;

    public float initialToolPositionX = 1;
    public float initialToolPositionY = 1;
    public float initialToolPositionZ = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camInfo = camera.transform.transform.position;
        Vector3 camRot = camera.transform.eulerAngles;
        Vector3 toolParentRot = toolParent.transform.eulerAngles;

        toolParent.transform.position = new Vector3(camInfo.x, camInfo.y - cameraDistOffsetY, camInfo.z - cameraDistOffsetZ);
        toolParent.transform.eulerAngles = new Vector3(toolParentRot.x, camRot.y, toolParentRot.z);

    }

    public void OnRelease()
    {
        Vector3 camInfo = camera.transform.transform.position;
        tool.transform.localPosition = new Vector3(0 + initialToolPositionX, 0 + initialToolPositionY, 0 + initialToolPositionZ);

        Vector3 toolRot = tool.transform.eulerAngles;
        tool.transform.localEulerAngles = new Vector3(toolRot.x - toolRot.x, toolRot.y - toolRot.y, toolRot.z - toolRot.z);
        tool.transform.localEulerAngles = new Vector3(toolRot.x + initialToolRotationX, toolRot.y + initialToolRotationY, toolRot.z + initialToolRotationZ);

    }
}
