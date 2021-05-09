using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamerAI : AIAspect
{
    // Start is called before the first frame update
    public Vector3 currentPoint;
    public StreamerMoveMgr ptMgr;

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
        
    }

    void Selecting()
    {
        
    }

    void Moving()
    {
        
    }

    void Firing()
    {
    }

    void Leaving()
    {
        
    }
}
