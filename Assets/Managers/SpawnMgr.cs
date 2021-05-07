using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> spawnLocations;
    public EntityMgr entityMgr;

    public static SpawnMgr inst;

    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        foreach(Transform t in transform)
        {
            spawnLocations.Add(t.position);
        }
    }

    public Entity SpawnEntity(Entity entity, int pos)
    {
        GameObject ent = Instantiate(entity.gameObject);
        ent.SetActive(true);
        ent.GetComponent<Entity>().health = entity.health;
        ent.transform.position = spawnLocations[pos];
        entityMgr.AddEntity(ent);
        return ent.GetComponent<Entity>();
    }
}
