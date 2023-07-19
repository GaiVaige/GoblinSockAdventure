using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioMenu;

    private bool pauseState = false;
    private bool audioMenuState = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && audioMenuState == true)
        {
            OpenPauseMenu();
            audioMenu.SetActive(false);
            audioMenuState = false;
            return;
        }

        //Opens pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState == false)
        {
            OpenPauseMenu();
            return;
        }

        //Closes pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState == true)
        {
            Continue();
            //Debug.Log("no");
        }
    }

    //Continue button
    public void Continue()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseState = false;
    }

    //Restart button
    public void RestartLevel()
    {
        SceneManager.LoadScene("TestingTrack");
        Time.timeScale = 1;
    }

    public void AudioOptions()
    {
        pauseMenu.SetActive(false);
        audioMenu.SetActive(true);
        audioMenuState = true;
    }
    
    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseState = true;
        //Debug.Log("yes");
    }

}
