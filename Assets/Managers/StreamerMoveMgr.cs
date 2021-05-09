using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamerMoveMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointOne;
    public Transform pointTwo;

    public List<Vector3> positions;
    void Start()
    {
        positions.Add(pointOne.position);
        positions.Add(pointTwo.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetNearestPosition(Vector3 pos)
    {
        float distanceToOne = Vector3.Distance(pos, positions[0]);
        float distanceToTwo = Vector3.Distance(pos, positions[1]);

        if (distanceToTwo > distanceToOne)
        {
            return positions[0];
        }

        return positions[1];
    }

    public Vector3 GetNextPosition(Vector3 pos)
    {
        if (pos == positions[0])
        {
            return positions[1];
        }

        return positions[0];
    }
}
