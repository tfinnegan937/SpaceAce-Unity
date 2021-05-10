using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : AIAspect
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
    public ProjectileMgr projMgr;
    private Entity ent;
    public float speed;
    private Vector3 curGunPt;
    private Vector3 curIdlePt;
    public int numIdleRot;
    void Start()
    {
        state = BossState.Entering;
        ent = GetComponent<Entity>();
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
        MoveTo(ptMgr.GetResetPt());

        if (Vector3.Distance(transform.position, ptMgr.GetResetPt()) < .1f)
        {
            state = BossState.Idling;
            curIdlePt = ptMgr.GetNextIdlePt();
        }
    }

    void MovingToMainGunPt()
    {
        
    }

    void FiringMainGun()
    {
        projMgr.SpawnProjectile(mainProj, ptMgr.GetFirePt());
        state = BossState.MovingToMainGunPt;
        if (ptMgr.DoneFiring())
        {
            state = BossState.Resetting;
            ptMgr.ResetMainGun();
        }
    }

    void MovingToMissilePt()
    {
        MoveTo(ptMgr.GetMissilePt());

        if (Vector3.Distance(transform.position, ptMgr.GetMissilePt()) < .1f)
        {
            state = BossState.FiringMissiles;
        }
    }

    void FiringMissiles()
    {
        projMgr.SpawnProjectile(missileProj, missilePtMgr.GetNextPt());
        if (missilePtMgr.DoneFiring())
        {
            state = BossState.Resetting;
            missilePtMgr.Reset();
        }
    }

    void SpawningStreamers()
    {
        
    }

    void ChargingDash()
    {
        MoveTo(ptMgr.GetChargePt());

        if (Vector3.Distance(transform.position, ptMgr.GetChargePt()) < .1f)
        {
            state = BossState.Dashing;
            ent.SetVelocity(ent.maxSpeed * new Vector3(-1, 0, 0));
        }
    }

    void Dashing()
    {
        if (transform.position.x < ptMgr.GetDashEndX())
        {
            state = BossState.TeleportingBack;
        }
    }

    void Resetting()
    {
        MoveTo(ptMgr.GetResetPt());

        if (Vector3.Distance(transform.position, ptMgr.GetResetPt()) < .1f)
        {
            state = BossState.SelectingNextPattern;
        }
    }

    void Idling()
    {
        MoveTo(curIdlePt);

        if (Vector3.Distance(curIdlePt, transform.position) < .1f)
        {
            curIdlePt = ptMgr.GetNextIdlePt();
        }

        if (ptMgr.DoneIdling(numIdleRot))
        {
            state = BossState.Resetting;
        }
    }

    void SelectingNextPattern()
    {
        
    }

    void TeleportingBack()
    {
        transform.position = ptMgr.GetTeleportPoint();
        state = BossState.Resetting;
    }

    void MoveTo(Vector3 pos, bool MaxSpeed = false)
    {
        Vector3 diff = pos - transform.position;
        float desiredHeading = Mathf.Atan2(diff.y, diff.x);
        float curSpeed = speed;
        if (MaxSpeed)
        {
            curSpeed = ent.maxSpeed;
        }
        Vector3 vel = new Vector3(
            curSpeed * Mathf.Cos(desiredHeading),
            curSpeed * Mathf.Sin(desiredHeading),
            -5
        );
 
        ent.SetVelocity(vel);
    }
}
