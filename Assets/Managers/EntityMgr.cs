using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : MonoBehaviour
{
    public static EntityMgr inst;
    public List<Entity> Entities;
    public GameObject EntityObject;
    public GameObject healthPickup;
    public VictoryMgr vicMgr;
    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Entities = new List<Entity>(); //Initialize list

        foreach (Transform obj in EntityObject.transform) //Add any entities created at level startup
        {
            AddEntity(obj.gameObject);
        }
    }

    public void AddEntity(GameObject EntityObj)
    {
        Entity CurEnt;
        CurEnt = EntityObj.GetComponent<Entity>();
        if (CurEnt != null)
        {
            Debug.Log("ENTITY EXISTS");
            EntityObj.transform.parent = EntityObject.transform; //Child new entity to EntityObj
            Entities.Add(CurEnt);
        }
    }

    public void RemoveEntity(string name)
    {
        foreach(Entity ent in Entities)
        {
            if (ent.gameObject.transform.name == name)
            {
                int randChance = Random.Range(0, 100);
                if (randChance < 20 && !ent.gameObject.CompareTag("Projectile"))
                {
                    GameObject healthObj = Instantiate(healthPickup);
                    healthPickup.SetActive(true);
                    healthPickup.transform.position = ent.transform.position;
                }

                if (ent.GetComponent<FinalBoss>())
                {
                    vicMgr.Victory();
                }
                Entities.Remove(ent);
                Destroy(ent.gameObject);
            }
        }


    }

    public Entity GetEntity(string name)
    {
        foreach(Entity ent in Entities)
        {
            if (ent.gameObject.transform.name == name)
            {
                return ent;
            }
        }

        return null;
    }
}
