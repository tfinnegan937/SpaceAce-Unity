using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public HPMgr hpMgr;
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public bool isEnemy;
    public int negate = 1;
    public GameObject firePoint;
    public virtual void Start()
    {
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();

            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();

                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        if (isEnemy)
        {
            negate = -1;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        CullProjectilesOffscreen();
    }

    void OnCollisionEnter(Collision co)
    {
        speed = 0;

        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        string tag = co.gameObject.tag;
        Entity ent = co.gameObject.GetComponent<Entity>();

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
                Destroy(hitVFX, psHit.main.duration);

            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }

        if (tag == "Player")
        {
            //Modify Health System
            hpMgr.DamagePlayer();
        }
        else if (tag == "Enemy")
        {
            ent.Damage();
        }

        Debug.Log("COLLIDED");
        ent.entityMgr.RemoveEntity(gameObject.name);

    }

    void CullProjectilesOffscreen()
    {
        Camera main = Camera.main;
        Vector3 viewportPoint = main.WorldToViewportPoint(transform.position);
        Entity ent = GetComponent<Entity>();
        if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
        {
            ent.entityMgr.RemoveEntity(gameObject.name);

        }
    }

}
