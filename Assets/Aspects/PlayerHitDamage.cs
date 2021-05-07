using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public HPMgr hpMgr;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hpMgr.DamagePlayer();
        }
    }
}
