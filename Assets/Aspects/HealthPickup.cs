using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDisappear; //Amount of time required to disappear
    public float disappearTime; //The exact moment at which it will disappear.
    public HPMgr hpMgr;
    void OnEnable()
    {
        disappearTime = Time.time + timeToDisappear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > disappearTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("HEAL");
        if (other.gameObject.CompareTag("Player"))
        {
            hpMgr.GainLife();
            Destroy(gameObject);
        }
    }
}
