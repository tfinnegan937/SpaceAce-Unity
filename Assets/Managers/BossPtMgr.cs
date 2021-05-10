using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPtMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform missilePt;
    public Transform StopEnterPt;
    public Transform DashChargePt;
    public Transform MainGunPt1;
    public Transform MainGunPt2;
    public Transform MainGunPt3;
    public Transform IdlePt1;
    public Transform idlePt2;
    public Transform teleportPt;
    public Transform DashStopPt;
    public Transform FirePt;
    public List<Vector3> gunPts;
    public List<Vector3> idlePts;

    private int idleIndex;
    private int timesIdled;

    private int fired;
    void Start()
    {
        gunPts.Add(MainGunPt1.position);
        gunPts.Add(MainGunPt2.position);
        gunPts.Add(MainGunPt3.position);
        
        idlePts.Add(IdlePt1.position);
        idlePts.Add(idlePt2.position);
        idleIndex = 0;
        timesIdled = 0;
        fired = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DoneIdling(int numRotations)
    {
        return (timesIdled >= numRotations);
    }

    public bool DoneFiring()
    {
        return (fired > gunPts.Count - 1);
    }

    public void ResetMainGun()
    {
        fired = 0;
    }

    public Vector3 GetNextGunPt()
    {
        Vector3 pt = gunPts[fired];
        fired++;
        return pt;
    }

    public Vector3 GetNextIdlePt()
    {
        Vector3 pt = idlePts[idleIndex];
        idleIndex++;
        if (idleIndex > idlePts.Count - 1)
        {
            timesIdled++;
            idleIndex = 0;
        }

        return pt;
    }

    public void ResetIdle()
    {
        idleIndex = 0;
        timesIdled = 0;
    }

    public Vector3 GetResetPt()
    {
        return StopEnterPt.position;
    }

    public Vector3 GetChargePt()
    {
        return DashChargePt.position;
    }

    public Vector3 GetTeleportPoint()
    {
        return teleportPt.position;
    }

    public Vector3 GetMissilePt()
    {
        return missilePt.position;
    }

    public float GetDashEndX()
    {
        return DashStopPt.position.x;
    }

    public Vector3 GetFirePt()
    {
        return FirePt.position;
    }
}
