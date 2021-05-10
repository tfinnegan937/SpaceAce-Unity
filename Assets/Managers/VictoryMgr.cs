using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainUI;
    public GameObject VictoryUI;
    public GameObject SceneMgr;
    public GameObject EntityMgr;
    public GameObject InputMgr;
    public GameObject ProjectileMgr;
    public ScoreMgr scoreMgr;
    public Text VictoryScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Victory()
    {
        VictoryScore.text = scoreMgr.scoreText.text;
        MainUI.SetActive(false);
        VictoryUI.SetActive(true);
        SceneMgr.GetComponent<SceneMgr>().enabled = false;
        EntityMgr.SetActive(false);
        InputMgr.SetActive(false);
        ProjectileMgr.SetActive(false);
    }
}
