using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnMgr : MonoBehaviour
{
    public List<SceneMgr.SpawnList> spawnLists;

    public SceneMgr scnMgr;
    // Start is called before the first frame update
    void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    SceneMgr.SpawnList GetRandomList()
    {
        if (spawnLists.Count == null)
        {
            return new SceneMgr.SpawnList();
        }
        int index = Random.Range(0, spawnLists.Count);

        return spawnLists[index];
    }

    public void SpawnRandomBossAdds()
    {
        scnMgr.AddSpawnList(GetRandomList());
    }
}
