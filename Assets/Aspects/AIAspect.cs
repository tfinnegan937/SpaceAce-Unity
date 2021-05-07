using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAspect : MonoBehaviour
{
    // Start is called before the first frame update
    public ScoreMgr scoreMgr;
    public int scoreValue;
    public bool destroyedByProjectile = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        if (destroyedByProjectile)
        {
            scoreMgr.AddScore(scoreValue);
        }
    }
}
