using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamerAI : AIAspect
{
    // Start is called before the first frame update
    public Vector3 currentPoint;
    public StreamerMoveMgr ptMgr;
    private Entity ent;
    public float speed;
    public float fireRate;
    public int fireCount;
    public int numFire;
    public float timeTilFire;
    public Projectile proj;
    public Transform projLoc;
    public ProjectileMgr projMgr;
    public int numSwitchLeave;
    private int numSwitch;
    public AudioMgr audioMgr;
    [System.Serializable]
    public enum State
    {
        Entering,
        Selecting,
        Moving,
        Firing,
        Leaving
    }

    public State state;
    void Start()
    {
        state = State.Entering;
        timeTilFire = 0;
        fireCount = 0;
        numSwitch = 0;
        ent = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Entering:
                Entering();
                break;
            case State.Selecting:
                Selecting();
                break;
            case State.Moving:
                Moving();
                break;
            case State.Firing:
                Firing();
                break;
            case State.Leaving:
                Leaving();
                break;
        }
    }

    void Entering()
    {
        Vector3 vel = new Vector3(-1, 0, 0) * speed;
        ent.SetVelocity(vel);
        if (IsOnCamera())
        {
            currentPoint = ptMgr.GetNearestPosition(transform.position);
            state = State.Moving; //Skip selecting for first selection
        }
    }

    void Selecting()
    {
        currentPoint = ptMgr.GetNextPosition(currentPoint);
        state = State.Moving;
    }

    void Moving()
    {
        Vector3 diff = currentPoint - transform.position;
        float desiredHeading = Mathf.Atan2(diff.y, diff.x);
        Vector3 vel = new Vector3(
            speed * Mathf.Cos(desiredHeading),
            speed * Mathf.Sin(desiredHeading),
            -5
        );
 
        ent.SetVelocity(vel);

        if (Vector3.Distance(currentPoint, transform.position) < 1.1f)
        {
            ent.SetVelocity(Vector3.zero);
            fireCount = 0;
            state = State.Firing;
            timeTilFire = Time.time;
            numSwitch++;
        }
    }

    void Firing()
    {
        if (Time.time >= timeTilFire)
        {
            audioMgr.PlayStreamer();
            projMgr.SpawnProjectile(proj, projLoc.position);
            timeTilFire = Time.time + (1 / fireRate);
            fireCount++;
        }

        if (fireCount >= numFire)
        {
            state = State.Selecting;
            if (numSwitch > numSwitchLeave)
            {
                state = State.Leaving;
            }
        }
    }

    void Leaving()
    {
        //Called after a timer
        Vector3 vel = new Vector3(-1, 0, 0) * speed;
        ent.SetVelocity(vel);
        if (!IsOnCamera())
        {
            ent.entityMgr.RemoveEntity(ent.gameObject.name);
        }
    }
}
