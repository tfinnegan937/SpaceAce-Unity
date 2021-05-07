using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeSinceStart = 0;
    public float distanceSinceStart = 0;
    public float flySpeed = 100;
    public static TimeMgr inst;

    void Awake()
    {
        inst = this;
    }
 

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        distanceSinceStart = timeSinceStart * 100;
    }

    public float GetScrollDistance()
    {
        return distanceSinceStart;
    }

    public float GetScrollTime()
    {
        return timeSinceStart;
    }
}
