using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAspect : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool IsOnCamera()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.WorldToViewportPoint(transform.position);
        if (pos.x >= 0 && pos.x <= 1)
        {
            return true;
        }

        return false;
    }

}
