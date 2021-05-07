using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButt : MonoBehaviour
{
    // Start is called before the first frame update
    public PauseMgr pauseMgr;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnpauseGame()
    {
        pauseMgr.UnpauseGame();
    }
}
