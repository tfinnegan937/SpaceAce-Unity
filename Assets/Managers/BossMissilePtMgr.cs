using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissilePtMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pt1;
    public Transform pt2;
    public Transform pt3;
    public Transform pt4;
    private List<Vector3> pts;
    private int fired = 0;

    public static BossMissilePtMgr inst;

    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        pts.Add(pt1.position);
        pts.Add(pt2.position);
        pts.Add(pt3.position);
        pts.Add(pt4.position);
        Reset();


    }
    

    public void Reset()
    {
        fired = 0;
    }

    public bool DoneFiring()
    {
        return (fired > pts.Count - 1);
    }

    public Vector3 GetNextPt()
    {
        Vector3 pt = pts[fired];
        fired++;
        return pt;
    }
}
