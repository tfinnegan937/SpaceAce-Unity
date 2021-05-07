using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject overlayFirst;
    public GameObject overlaySecond;

    public Vector3 overlayFirstStart;
    public Vector3 overlaySecondStart;

    void Start()
    {
        overlayFirstStart = overlayFirst.transform.localPosition;
        overlaySecondStart = overlaySecond.transform.localPosition;
    }

    public void Move(float speed)
    {

        GameObject temp;
        overlayFirst.transform.localPosition += (new Vector3(-1, 0, 0)) * speed * Time.deltaTime;
        overlaySecond.transform.localPosition += (new Vector3(-1, 0, 0)) * speed * Time.deltaTime;

        if (overlaySecond.transform.localPosition.x <= overlayFirstStart.x)
        {
            overlaySecond.transform.localPosition = overlaySecondStart;
            overlayFirst.transform.localPosition = overlayFirstStart;
        }
    }
}
