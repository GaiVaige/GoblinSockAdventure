using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private AudioSource openMenuSound;
    [SerializeField] private AudioSource exitMenuSound;

    private bool pauseState = false;
    private bool audioMenuState = false;

    void Update()
    {
        //Returns to pause menu if escape is clicked in audio options menu
        if (Input.GetKeyDown(KeyCode.Escape) && audioMenuState == true)
        {
            exitMenuSound.Play();
            CloseAudioMenu();
            return;
        }

        //Opens pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && (pauseState == false) && (audioMenuState == false))
        {
            OpenPauseMenu();
            openMenuSound.Play();
            return;
        }

        //Closes pasuse menu if audio menu is not open
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState == true)
        {
            exitMenuSound.Play();
            Continue();
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
        Time.timeScale = 1;
        StartCoroutine(RestartLevel2());
    }

    public IEnumerator RestartLevel2()
    {
        yield return new WaitForSeconds(0.6f);

        SceneManager.LoadScene("Level1");
    }

    //Options button
    public void AudioOptions()
    {
        pauseMenu.SetActive(false);
        audioMenu.SetActive(true);
        audioMenuState = true;
        pauseState = false;
    }

    //Closes audio menu and returns to pause menu
    public void CloseAudioMenu()
    {
        OpenPauseMenu();
        audioMenu.SetActive(false);
        audioMenuState = false;
        return;
    }

    //Opens pause menu
    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseState = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
