using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PelterAI : AIAspect
{
    // Start is called before the first frame update
    [System.Serializable]
    public enum State
    {
        Entering,
        Selecting,
        Moving,
        Slowing,
        Firing
    }

    public State state;
    private Entity ent;
    public Vector3 desiredSpeed;
    public Vector3 firePoint;
    public PelterMovePtMgr ptMgr;
    public ProjectileMgr projectileMgr;
    public Projectile projectile;
    public GameObject projPoint;
    public Vector3 projSpawnLoc;
    void Start()
    {
        ent = GetComponent<Entity>();
        state = State.Entering;
        Random.seed = System.DateTime.Now.Millisecond;

        foreach (Transform t in transform)
        {
            Debug.Log(t.gameObject.name);
            if (t.gameObject.name == "ProjPoint")
            {
                projPoint = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(180 * Time.deltaTime, 0, 0, Space.Self);
        projSpawnLoc = projPoint.transform.position;
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
            case State.Slowing:
                break;
            case State.Firing:
                Firing();
                break;
        }
    }

    void Entering()
    {
        ent.SetVelocity(new Vector3(-1 * ent.maxSpeed, 0, 0));

        if (IsOnCamera())
        {
            state = State.Selecting;
        }
    }

    void Selecting()
    {

        Vector3 firePointLast = firePoint;
        firePoint = ptMgr.GetRandomPosition();

        if (firePointLast != firePoint)
        {
            ent.speed = 3;
            state = State.Moving;
        }

    }

    void Moving()
    {
        Vector3 diff = firePoint - transform.position;
        float desiredHeading = Mathf.Atan2(diff.y, diff.x);
        Vector3 vel = new Vector3(
            ent.speed * Mathf.Cos(desiredHeading),
            ent.speed * Mathf.Sin(desiredHeading),
            -5
            );
 
        ent.SetVelocity(vel);
        if (Vector3.Distance(firePoint, transform.position) < .1)
        {
            state = State.Firing;
        }
    }

    void Firing()
    {
        projectileMgr.SpawnProjectile(projectile, projSpawnLoc);
        state = State.Selecting;
    }


}
