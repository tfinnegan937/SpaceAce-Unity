using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instructions;
    public GameObject mMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ViewInstructions()
    {
        instructions.SetActive(true);
        mMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        mMenu.SetActive(true);
        instructions.SetActive(false);
    }
}
