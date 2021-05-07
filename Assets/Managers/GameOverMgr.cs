using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainUI;
    public GameObject GameOverUI;
    public GameObject SceneMgr;
    public GameObject EntityMgr;
    public GameObject InputMgr;
    public GameObject ProjectileMgr;
    public ScoreMgr scoreMgr;
    public Text GameOverScore;
    public static GameOverMgr inst;
    

    void Awake()
    {
        inst = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        GameOverScore.text = scoreMgr.scoreText.text;
        MainUI.SetActive(false);
        GameOverUI.SetActive(true);
        SceneMgr.GetComponent<SceneMgr>().enabled = false;
        EntityMgr.SetActive(false);
        InputMgr.SetActive(false);
        ProjectileMgr.SetActive(false);
    }
}
