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
    public GameObject PanelGameMenu;
    public GameObject PanelButtons;
    private AudioSource MenuMusicGenerator;
    private Scene ActiveScene;
	
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);//Menu Manager lives forever
        MenuPanelController();//turn panels on/off correctly

    }

	// Update is called once per frame
	void Update () {

        //If the menu state changed, activate the appropriate menus
        if (VariableKeeper.menuState != VariableKeeper.menuStateBefore)
        {
            MenuPanelController();//turn panels on/off correctly
            VariableKeeper.menuStateBefore = VariableKeeper.menuState;
        }

        //main menu buttons to navigate scenes
        Debug.Log("MenuState is " + VariableKeeper.menuState);
        if (VariableKeeper.menuState != 3)
        {
            MenuInputTaker();
        }
        else if ((VariableKeeper.menuState == 3))
        {
            GameInputTaker();
        }
    }

    //takes input of the menus outside the game
    public void MenuInputTaker()
    {
        if (Input.GetButtonDown("Continue"))
        {
            ContinueGame();
        }
        else if (Input.GetButtonDown("Credits"))
        {
            MenuChanger();
            //Credits();
        }
        else if (Input.GetButtonDown("Start"))
        {
            StartGame();
        }
        else if (Input.GetButtonDown("Stop"))
        {
            QuitGame();
        }
    }

    //takes input of the menus outside the game
    public void GameInputTaker()
    {
        if (Input.GetButtonDown("Credits"))
        {
            ReturnToMenu();
        }
    }

    //which menu should be active at any time
    public void MenuPanelController()
    {
        if (VariableKeeper.menuState == 0)
        {
            //Debug.Log("Main Menu");
            PanelMainMenu.SetActive(true);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            MenuMusicGenerator = MainMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
        else if (VariableKeeper.menuState == 1)
        {
            //Debug.Log("Win Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(true);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            MenuMusicGenerator = WinMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
        else if (VariableKeeper.menuState == 2)
        {
            //Debug.Log("Lose Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(true);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            MenuMusicGenerator = LoseMenuMusic.GetComponent<AudioSource>();
            MenuMusicGenerator.Play();
        }
        else if (VariableKeeper.menuState == 3)
        {
            //Debug.Log("Game Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(true);
            PanelButtons.SetActive(false);
            //MenuMusicGenerator = LoseMenuMusic.GetComponent<AudioSource>();
            //MenuMusicGenerator.Play();
        }
    }


    //Debug to change menu
    public void MenuChanger()
    {
        Debug.Log("The menu was " + VariableKeeper.menuState);
        if (VariableKeeper.menuState == 2)
        {
            VariableKeeper.menuState = 0;
            MenuPanelController();
        }
        else
        {
            VariableKeeper.menuState = VariableKeeper.menuState + 1;
            MenuPanelController();
        }
        Debug.Log("The menu is "+VariableKeeper.menuState);
    }
    
    //load main menu
    public void ReturnToMenu()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 0;
    }

    //load first level
    public void StartGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        SceneManager.LoadScene(VariableKeeper.levelStart);
    }
    
    //load scene from last play
    public void ContinueGame()
	{
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        SceneManager.LoadScene(VariableKeeper.levelProgression);
	}

    //load next level
    public void NextGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        SceneManager.UnloadSceneAsync(ActiveScene.buildIndex);
        VariableKeeper.levelProgression = VariableKeeper.levelProgression + 1;
        SceneManager.LoadScene(VariableKeeper.levelProgression);
    }

    //win game
    public void WinGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.levelProgression = 1;
        VariableKeeper.menuState = 1;
    }

    //lose game
    public void LoseGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 2;
    }

    //quit game
    public void QuitGame()
    {
        MenuMusicGenerator.Stop();
        Application.Quit();
    }


}
