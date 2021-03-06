using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        SpawningAdds,
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
    public Vector3 curGunPt;
    public Vector3 curIdlePt;
    public int numIdleRot;
    public Vector3 nextMissPt;
    private bool dashing = false;
    public BossSpawnMgr spawnMgr;
    public AudioMgr audioMgr;
    void Start()
    {
        state = BossState.Entering;
        ent = GetComponent<Entity>();
        Random.seed = System.DateTime.Now.Millisecond;
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
            case BossState.SpawningAdds:
                SpawningAdds();
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

        if (Vector3.Distance(transform.position, ptMgr.GetResetPt()) < .3f)
        {
            state = BossState.Idling;
            curIdlePt = ptMgr.GetNextIdlePt();
        }
    }

    void MovingToMainGunPt()
    {
        MoveTo(curGunPt);

        if (Vector3.Distance(transform.position, curGunPt) < .3f)
        {
            ent.SetVelocity(Vector3.zero);
            state = BossState.FiringMainGun;
        }
    }

    void FiringMainGun()
    {
        projMgr.SpawnProjectile(mainProj, ptMgr.GetFirePt());
        state = BossState.MovingToMainGunPt;
        audioMgr.PlayStreamer();
        if (ptMgr.DoneFiring())
        {
            state = BossState.Resetting;
            ptMgr.ResetMainGun();
            return;
        }

        curGunPt = ptMgr.GetNextGunPt();
    }

    void MovingToMissilePt()
    {
        MoveTo(ptMgr.GetMissilePt());
        audioMgr.PlayPelter();
        if (Vector3.Distance(transform.position, ptMgr.GetMissilePt()) < .3f)
        {
            ent.SetVelocity(Vector3.zero);
            state = BossState.FiringMissiles;
        }
    }

    void FiringMissiles()
    {
        nextMissPt = missilePtMgr.GetNextPt();
        projMgr.SpawnProjectile(missileProj, nextMissPt);
        if (missilePtMgr.DoneFiring())
        {
            state = BossState.Resetting;
            missilePtMgr.Reset();
        }
    }

    void SpawningAdds()
    {
        spawnMgr.SpawnRandomBossAdds();
        state = BossState.Resetting;
    }

    void ChargingDash()
    {
        MoveTo(ptMgr.GetChargePt());

        if (Vector3.Distance(transform.position, ptMgr.GetChargePt()) < .3f)
        {
            state = BossState.Dashing;
            dashing = true;
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

        if (Vector3.Distance(transform.position, ptMgr.GetResetPt()) < .3f)
        {
            state = BossState.SelectingNextPattern;
        }
    }

    void Idling()
    {
        MoveTo(curIdlePt);

        if (Vector3.Distance(curIdlePt, transform.position) < .3f)
        {
            curIdlePt = ptMgr.GetNextIdlePt();
        }

        if (ptMgr.DoneIdling(numIdleRot))
        {
            ptMgr.ResetIdle();
            state = BossState.Resetting;
        }
    }

    void SelectingNextPattern()
    {
        float diceRoll = Random.Range(0, 100);

        if (diceRoll < 0)
        {
            state = BossState.Idling;
        }else if (diceRoll < 50)
        {
            state = BossState.MovingToMainGunPt;
            curGunPt = ptMgr.GetNextGunPt();        
        }else if (diceRoll < 75)
        {
            state = BossState.MovingToMissilePt;
        }
        else if (diceRoll < 85)
        {
            state = BossState.ChargingDash;
        }
        else
        {
            state = BossState.SpawningAdds;
        }

    }

    void TeleportingBack()
    {
        transform.position = ptMgr.GetTeleportPoint();
        state = BossState.Resetting;
        dashing = false;
        GetComponent<Collider>().enabled = true;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && dashing)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}

