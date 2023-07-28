using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;

    public AudioSource open;
    public AudioSource close;

    private bool audioMenuState = false;
    private bool levelMenuState = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && audioMenuState == true)
        {
            CloseAudioMenu();
            close.Play();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && levelMenuState == true)
        {
            CloseLevelMenu();
            close.Play();
            return;
        }
    }

    public void OpenLevelMenu()
    {
        levelMenu.SetActive(true);
        levelMenuState = true;
        mainMenu.SetActive(false);
        open.Play();
    }

    public void CloseLevelMenu()
    {
        levelMenu.SetActive(false);
        levelMenuState = false;
        mainMenu.SetActive(true);
        close.Play();
    }

    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
        audioMenuState = true;
        mainMenu.SetActive(false);
        open.Play();
    }

    public void CloseAudioMenu()
    {
        audioMenu.SetActive(false);
        audioMenuState = false;
        mainMenu.SetActive(true);
        close.Play();
        return;
    }

    public void LoadLevel1()
    {
        open.Play();
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        open.Play();
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        open.Play();
        SceneManager.LoadScene("Level3");
    }

}
