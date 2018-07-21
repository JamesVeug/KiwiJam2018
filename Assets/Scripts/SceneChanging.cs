using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour {

    public GameObject MainMenuMusic;
    public GameObject WinMenuMusic;
    public GameObject LoseMenuMusic;
    public GameObject PanelMainMenu;
    public GameObject PanelWinMenu;
    public GameObject PanelLoseMenu;
    private AudioSource MenuMusicGenerator;
	
	// Use this for initialization
	void Start () {

        if(VariableKeeper.menuState == 0)
        {
            Debug.Log("Main Menu");
            PanelMainMenu.SetActive(true);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            MenuMusicGenerator = MainMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
        else if (VariableKeeper.menuState == 1)
        {
            Debug.Log("Win Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(true);
            PanelLoseMenu.SetActive(false);
            MenuMusicGenerator = WinMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
        else if (VariableKeeper.menuState == 2)
        {
            Debug.Log("Lose Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(true);
            MenuMusicGenerator = LoseMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Debug to change menu
    public void MenuChanger()
    {
        Debug.Log("The menu was " + VariableKeeper.menuState);
        if (VariableKeeper.menuState == 2)
        {
            VariableKeeper.menuState = 0;
        }
        else
        {
            VariableKeeper.menuState = VariableKeeper.menuState + 1;
        }
        Debug.Log("The menu is "+VariableKeeper.menuState);
        SceneManager.LoadScene(VariableKeeper.levelMenu);
    }

    //load first scene
    public void StartGame()
    {
        MenuMusicGenerator.Stop();
        SceneManager.LoadScene(VariableKeeper.levelStart);
    }
    
    //load scene from last play
    public void ContinueGame()
	{
        MenuMusicGenerator.Stop();
		SceneManager.LoadScene(VariableKeeper.levelProgression);
	}

    public void ReturnToMenu()
    {
        MenuMusicGenerator.Stop();
        SceneManager.LoadScene(VariableKeeper.levelMenu);
    }

    //quit game
    public void QuitGame()
    {
        MenuMusicGenerator.Stop();
        Application.Quit();
    }


}
