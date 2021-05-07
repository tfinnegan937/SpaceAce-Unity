using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Overlay overlayNear;
    public Overlay overlay;
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        overlayNear.Move(speed);
        overlay.Move(speed / 2);
    }
}
