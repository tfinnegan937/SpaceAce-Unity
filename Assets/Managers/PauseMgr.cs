using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMgr : MonoBehaviour
{
    public GameObject Canvas;
    private bool paused = false;

    public UIMgr uiMgr;

    public void PauseGame()
    {
        Canvas.SetActive(true);
        uiMgr.BlankUI();
        Time.timeScale = 0;
        paused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        Canvas.SetActive(false);
        uiMgr.ShowUI();
        paused = false;
    }

    public void TogglePause()
    {
        if (paused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }
    
}
