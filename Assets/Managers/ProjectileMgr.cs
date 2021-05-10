using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMgr : MonoBehaviour
{
    public static ProjectileMgr inst;
    public AudioMgr audioMgr;
    void Awake()
    {
        inst = this;
    }

    public Projectile playerProjectile;
    public GameObject playerFirePoint;
    public EntityMgr entityMgr;
    public Vector3 firePos;

    public void Update()
    {
        if (playerFirePoint)
        {
            firePos = playerFirePoint.transform.position;
        }
    }
    
    public void SpawnPlayerProjectile()
    {
        if (playerFirePoint)
        {
            audioMgr.PlayPlayerBlaster();
            GameObject projInst = Instantiate(playerProjectile.gameObject);
            projInst.SetActive(true);
            projInst.GetComponent<Projectile>().firePoint = playerFirePoint;
            entityMgr.AddEntity(projInst);
            projInst.transform.position = firePos;
        }
    }



    public void SpawnProjectile(Projectile proj, Vector3 pos)
    {
        GameObject projInst = Instantiate(proj.gameObject);
        projInst.SetActive(true);
        projInst.transform.position = pos;
        Debug.Log("Spawned");
        entityMgr.AddEntity(projInst);
    }
    
}
