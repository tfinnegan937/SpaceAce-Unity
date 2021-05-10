using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public GameObject playerBlaster;

    public GameObject explosion;

    public GameObject pelter;

    public GameObject streamer;

    public AudioSource bossMusic;

    public AudioSource defaultMusic;

    public bool bossSpawned = false;
    // Start is called before the first frame update
    public enum State
    {
        Playing,
        LoweringDefault,
        RaisingBoss,
        PlayingBoss
    }

    public State state;
    void Start()
    {
        state = State.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Playing:
                break;
            case State.LoweringDefault:
                LoweringDefault();
                break;
            case State.RaisingBoss:
                RaisingBoss();
                break;
            case State.PlayingBoss:
                break;
        }
    }

    void LoweringDefault()
    {
        defaultMusic.volume -= 0.1f * Time.deltaTime;
        if (defaultMusic.volume <= 0)
        {
            state = State.RaisingBoss;
            bossMusic.enabled = true;
            defaultMusic.enabled = false;
        }
    }

    void RaisingBoss()
    {
        bossMusic.volume += 0.1f * Time.deltaTime;
        if (bossMusic.volume >= .5f)
        {
            state = State.PlayingBoss;
        }
    }
    public void PlayPlayerBlaster()
    {
        GameObject audioObject = Instantiate(playerBlaster);
        audioObject.SetActive(true);
        AudioSource src = audioObject.GetComponent<AudioSource>();
        Destroy(audioObject, src.clip.length);
    }

    public void PlayExplosion()
    {
        GameObject audioObject = Instantiate(explosion);
        audioObject.SetActive(true);
        AudioSource src = audioObject.GetComponent<AudioSource>();
        Destroy(audioObject, src.clip.length);
    }
    
    public void PlayStreamer()
    {
        GameObject audioObject = Instantiate(streamer);
        AudioSource src = audioObject.GetComponent<AudioSource>();
        audioObject.SetActive(true);
        Destroy(audioObject, src.clip.length);
    }

    public void PlayPelter()
    {
        GameObject audioObject = Instantiate(pelter);
        AudioSource src = audioObject.GetComponent<AudioSource>();
        audioObject.SetActive(true);
        Destroy(audioObject, src.clip.length);
    }

    public void BeginBossMusic()
    {
        if (!bossSpawned)
        {
            state = State.LoweringDefault;
            bossSpawned = true;
        }
    }
}
