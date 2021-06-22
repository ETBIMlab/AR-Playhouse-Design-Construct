using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMechanics : MonoBehaviour
{
    float rotSpeed = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 10;
        }

        transform.Rotate(0, 0, rotSpeed);
        this.rotSpeed *= 0.96f;
    }
}
