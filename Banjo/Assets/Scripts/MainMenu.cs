using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;

    private bool audioMenuState = false;
    private bool levelMenuState = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && audioMenuState == true)
        {
            CloseAudioMenu();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && levelMenuState == true)
        {
            CloseLevelMenu();
            return;
        }
    }

    public void OpenLevelMenu()
    {
        levelMenu.SetActive(true);
        levelMenuState = true;
        mainMenu.SetActive(false);
    }

    public void CloseLevelMenu()
    {
        levelMenu.SetActive(false);
        levelMenuState = false;
        mainMenu.SetActive(true);
    }

    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
        audioMenuState = true;
        mainMenu.SetActive(false);
    }

    public void CloseAudioMenu()
    {
        audioMenu.SetActive(false);
        audioMenuState = false;
        mainMenu.SetActive(true);
        return;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

}
