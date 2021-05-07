using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlankUI()
    {
        Canvas.SetActive(false);
    }

    public void ShowUI()
    {
        Canvas.SetActive(true);
    }
}
