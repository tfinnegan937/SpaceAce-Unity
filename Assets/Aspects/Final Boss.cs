using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public enum BossState
    {
        Entering,
        MovingToMainGunPt,
        FiringMainGun,
        MovingToMissilePt,
        FiringMissiles,
        SpawningStreamers,
        ChargingDash,
        Dashing,
        Resetting,
        Idling,
        SelectingNextPattern,
        TeleportingBack,
    }

    public BossState state;
    public BossMissilePtMgr missilePtMgr;
    public BossPtMgr ptMgr;
    public Projectile missileProj;
    public Projectile mainProj;
    void Start()
    {
        state = BossState.Entering;
    }

    // Update is called once per frame
    void Update()
    {
        //Handle State Machine
        switch (state)
        {
            case BossState.Entering:
                Entering();
                break;
            case BossState.MovingToMainGunPt:
                MovingToMainGunPt();
                break;
            case BossState.MovingToMissilePt:
                MovingToMissilePt();
                break;
            case BossState.FiringMissiles:
                FiringMissiles();
                break;
            case BossState.FiringMainGun:
                FiringMainGun();
                break;
            case BossState.SpawningStreamers:
                SpawningStreamers();
                break;
            case BossState.ChargingDash:
                ChargingDash();
                break;
            case BossState.Dashing:
                Dashing();
                break;
            case BossState.Resetting:
                Resetting();
                break;
            case BossState.Idling:
                Idling();
                break;
            case BossState.SelectingNextPattern:
                SelectingNextPattern();
                break;
            case BossState.TeleportingBack:
                TeleportingBack();
                break;
        }
    }

    void Entering()
    {
        
    }

    void MovingToMainGunPt()
    {
        
    }

    void FiringMainGun()
    {
        
    }

    void MovingToMissilePt()
    {
        
    }

    void FiringMissiles()
    {
        
    }

    void SpawningStreamers()
    {
        
    }

    void ChargingDash()
    {
        
    }

    void Dashing()
    {
        
    }

    void Resetting()
    {
        
    }

    void Idling()
    {
        
    }

    void SelectingNextPattern()
    {
        
    }

    void TeleportingBack()
    {
        
    }
}
