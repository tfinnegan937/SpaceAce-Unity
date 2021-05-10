using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public static SceneMgr inst;
    public TimeMgr timeMgr;
    public EntityMgr entityMgr;
    public SpawnMgr spawnMgr;
    public float time;
    public AudioMgr audioMgr;
    [System.Serializable]
    public struct SpawnRecord
    {
        public Entity ent;
        public int spawner;
    };
    
    [System.Serializable]
    public struct SpawnList
    {
        public List<SpawnRecord> spawnRecords;
        public List<Entity> activeEntities;
        public bool isTimed;
        public float time;
    }

    public enum WaveType
    {
        NotStarted,
        Timed, //Next wave spawns after timer
        Destroyed //Next wave spawns when all entities are destroyed.
    }
    
    

    public List<SpawnList> spawnLists;
    public SpawnList curSpawnList;
    public WaveType state = WaveType.NotStarted;
    private bool started = false;
    public float waveEndTime;
    public bool BossSpawned = false;
    public GameObject FinalBoss;
    void Awake()
    {
        inst = this;
    }


    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if (started)
        {
            switch (curSpawnList.isTimed)
            {
                case true:
                    waitTimed();
                    break;
                case false:
                    waitDestroyed();
                    break;
            }
        }
        else if(spawnLists.Count > 0)
        {
            ProcessNextSpawnList();
        }
        else
        {
            if (!BossSpawned)
            {
                BossSpawned = true;
                FinalBoss.SetActive(true);
                audioMgr.BeginBossMusic();
            }
        }
        
    }

    void ProcessNextSpawnList()
    {
        curSpawnList = spawnLists[0];
        SetState(curSpawnList);
        foreach (SpawnRecord r in curSpawnList.spawnRecords)
        {
            Entity ent = r.ent;
            int spawner = r.spawner;
            
            curSpawnList.activeEntities.Add(spawnMgr.SpawnEntity(ent, spawner));
        }
        spawnLists.RemoveAt(0);

        if (state == WaveType.Timed )
        {
            waveEndTime = Time.time + curSpawnList.time;
        }
        started = true;
    }
    
    void SetState(SpawnList spawnList)
    {
        switch (spawnList.isTimed)
        {
            case true:
                state = WaveType.Timed;
                break;
            case false:
                state = WaveType.Destroyed;
                break;
        }
    }

    void waitTimed()
    {
        if (Time.time >= waveEndTime)
        {
            started = false;
        }
    }

    void waitDestroyed()
    {
        bool allDead = true;
        foreach (Entity ent in curSpawnList.activeEntities)
        {
            if (ent)
            {
                allDead = false;
            }
        }

        if (allDead)
        {
            started = false;
        }
    }

    public void AddSpawnList(SpawnList list)
    {
        spawnLists.Add(list);
    }
  
}
