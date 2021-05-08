using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : AIAspect
{
    // Start is called before the first frame update
    public GameObject playerLine;
    public float followSpeed;
    private Entity ent;

    public Vector3 followPoint;
    public float chargeTime;
    public float timeTilCharge;

    private Camera mainCamera;
    public Vector3 cameraRectTopLeft;
    public Vector3 cameraRectBottomRight;
    private float randomY;
    [System.Serializable]
    public enum State
    {
        Entering,
        Following,
        WindUp,
        Charging
    }

    public State state;
    void OnEnable()
    {
        mainCamera = Camera.main;
        ent = GetComponent<Entity>();
        Random.seed = System.DateTime.Now.Millisecond;

        Rect cameraRect = mainCamera.rect;

        Vector3 viewZVector = mainCamera.WorldToViewportPoint(transform.position);

        cameraRectTopLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(cameraRect.xMin, cameraRect.yMin, viewZVector.z));
        cameraRectBottomRight = mainCamera.ViewportToWorldPoint(
            new Vector3(cameraRect.xMax, cameraRect.yMax, viewZVector.z));
        
 
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Entering:
                Entering();
                break;
            case State.Following:
                Following();
                break;
            case State.WindUp:
                WindUp();
                break;
            case State.Charging:
                Charging();
                break;
        }
    }

    void Entering()
    {
        ent.SetVelocity(new Vector3(-1 * followSpeed, 0, 0));

        if (IsOnCamera())
        {
            state = State.Following;
            timeTilCharge = Time.time + chargeTime;
        }
    }



    void Following()
    {
        followPoint = playerLine.transform.position;


        Vector3 diff = followPoint - transform.position;
        float desiredHeading = Mathf.Atan2(diff.y, diff.x);
        Vector3 vel = new Vector3(
            followSpeed * Mathf.Cos(desiredHeading),
            followSpeed * Mathf.Sin(desiredHeading),
            -5
        );

        if (Vector3.Distance(followPoint, transform.position) > 1)
        {
            ent.SetVelocity(vel);
        }
        else
        {
            ent.SetVelocity(Vector3.zero);
        }

        if (Time.time > timeTilCharge)
        {
            state = State.WindUp;
        }
    }

    void WindUp()
    {
        //TODO
        state = State.Charging;
    }
    
    void Charging()
    {
        ent.SetVelocity(new Vector3(-1 * ent.maxSpeed, 0, 0));

        if (transform.position.z < cameraRectTopLeft.x -4)
        {
            ent.entityMgr.RemoveEntity(gameObject.name);
        }

    }
}
