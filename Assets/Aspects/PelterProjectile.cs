using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelterProjectile : Projectile
{
    // Start is called before the first frame update
    public Entity ent;

    public override void Start()
    {
        base.Start();
        ent = GetComponent<Entity>();
        Debug.Log("here");
        ent.SetVelocity(new Vector3(negate * 1, 0, 0) * speed);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ent.SetVelocity(new Vector3(negate * 1, 0, 0) * speed);
    }
    

}
