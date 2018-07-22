using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour {

    public GameObject PanelMainMenu;
    public GameObject PanelWinMenu;
    public GameObject PanelLoseMenu;
    public GameObject PanelGameMenu;
    public GameObject PanelButtons;

    private AudioSource MenuMusicGenerator;
    public GameObject MainMenuMusic;
    public GameObject WinMenuMusic;
    public GameObject LoseMenuMusic;
    public GameObject buttonPressSFX;
    private AudioSource[] buttonPress;
    private int buttonPressTotal;
    public GameObject manDeathSFX;
    private AudioSource[] manDeath;
    private int manDeathTotal;
    public GameObject girlComeSFX;
    private AudioSource[] girlCome;
    private int girlComeTotal;
    public GameObject girlDeathSFX;
    private AudioSource[] girlDeath;
    private int girlDeathTotal;
    public GameObject electrocutionSFX;
    private AudioSource[] electrocution;
    private int electrocutionTotal;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);//Menu Manager lives forever
        MenuPanelController();//turn panels on/off correctly
        InitializeAudio();
    }

	// Update is called once per frame
	void Update () {

        //Debug.Log("MenuStateBefore " + VariableKeeper.menuStateBefore);
        //Debug.Log("MenuState " + VariableKeeper.menuState);
        //Debug.Log("LevelProgression " + VariableKeeper.levelProgression);
        //Debug.Log(VariableKeeper.isIceCreamLicked);


        //If the menu state changed, activate the appropriate menus
        if (VariableKeeper.menuState != VariableKeeper.menuStateBefore)
        {
            MenuPanelController();//turn panels on/off correctly
            VariableKeeper.menuStateBefore = VariableKeeper.menuState;
        }

        //main menu buttons to navigate scenes
        //Debug.Log("MenuState is " + VariableKeeper.menuState);
        if (VariableKeeper.menuState != 3)
        {
            MenuInputTaker();
        }
        else if ((VariableKeeper.menuState == 3))
        {
            GameInputTaker();
        }
    }

    //gets the audio started
    public void InitializeAudio()
    {

        //                menuManager.GetComponent<SceneChanging>().PlaySFXManDeath();
        buttonPress = buttonPressSFX.GetComponentsInChildren<AudioSource>();
        buttonPressTotal = buttonPress.Length;
        manDeath = manDeathSFX.GetComponentsInChildren<AudioSource>();
        manDeathTotal = manDeath.Length;
        girlCome = girlComeSFX.GetComponentsInChildren<AudioSource>();
        girlComeTotal = girlCome.Length;
        girlDeath = girlDeathSFX.GetComponentsInChildren<AudioSource>();
        girlDeathTotal = girlDeath.Length;
        electrocution = electrocutionSFX.GetComponentsInChildren<AudioSource>();
        electrocutionTotal = electrocution.Length;

    }

    public void PlaySFXelectrocution()
    {
        int random = Random.Range(0, electrocutionTotal);
        electrocution[random].Play();
    }

    public void PlaySFXManDeath()
    {
        int random = Random.Range(0, manDeathTotal);
        manDeath[random].Play();
    }

    public void PlaySFXGirlDeath()
    {
        int random = Random.Range(0, girlDeathTotal);
        girlDeath[random].Play();
    }

    public void PlaySFXGirlCome()
    {
        int random = Random.Range(0, girlComeTotal);
        girlCome[random].Play();
    }
    
    public void PlaySFXButtonPress()
    {
        int random = Random.Range(0, buttonPressTotal);
        buttonPress[random].Play();
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
            PlayerMovement.MovementEnabled = false;
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
            PlayerMovement.MovementEnabled = false;
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
            PlayerMovement.MovementEnabled = false;
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
            PlayerMovement.MovementEnabled = true;
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
        PlaySFXButtonPress();
        VariableKeeper.menuState = 0;
    }

    //load first level
    public void StartGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        VariableKeeper.levelProgression = 1;
        PlaySFXButtonPress();
        SceneManager.LoadScene(VariableKeeper.levelStart);
        VariableKeeper.isIceCreamLicked = false;
    }
    
    //load scene from last play
    public void ContinueGame()
	{
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        PlaySFXButtonPress();
        SceneManager.LoadScene(VariableKeeper.levelProgression);
        VariableKeeper.isIceCreamLicked = false;
    }

    //load next level
    public void NextGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 3;
        VariableKeeper.levelProgression = VariableKeeper.levelProgression + 1;
        VariableKeeper.isIceCreamLicked = false;
        SceneManager.LoadScene(VariableKeeper.levelProgression);
    }

    //win game
    public void WinGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 1;
        MenuPanelController();
        VariableKeeper.levelProgression = 1;
        VariableKeeper.isIceCreamLicked = false;

    }

    //lose game
    public void LoseGame()
    {
        MenuMusicGenerator.Stop();
        VariableKeeper.menuState = 2;
        VariableKeeper.isIceCreamLicked = false;
        Debug.Log("did I lose?");
    }

    //quit game
    public void QuitGame()
    {
        MenuMusicGenerator.Stop();
        Application.Quit();
    }


}
