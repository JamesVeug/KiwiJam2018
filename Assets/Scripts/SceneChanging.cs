﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour {

    public static SceneChanging Instance
    {
        get {
            var menuManager = GameObject.Find("MenuManager");
            if (menuManager != null)
            {
                return menuManager.GetComponent<SceneChanging>();
            }

            Debug.LogError("No SceneManager");
            return null;
        }
    }

    public GameObject menuCamera;
    public Canvas canvas;

    private AudioSource[] allAudioSources;

    public GameObject PanelMainMenu;
    public GameObject PanelWinMenu;
    public GameObject PanelLoseMenu;
    public GameObject PanelGameMenu;
    public GameObject PanelButtons;
    public GameObject PanelCredits;

    private AudioSource MainMenuMusicPlayer;
    public GameObject MainMenuMusic;
    private AudioSource Level1MusicPlayer;
    public GameObject Level1Music;
    private AudioSource Level2MusicPlayer;
    public GameObject Level2Music;
    private AudioSource WinMenuMusicPlayer;
    public GameObject WinMenuMusic;
    private AudioSource LoseMenuMusicPlayer;
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
    public GameObject electricitySFX;
    private AudioSource[] electricity;
    private int electricityTotal;
    public GameObject girlJumpSFX;
    private AudioSource[] girlJump;
    private int girlJumpTotal;
    public GameObject girlLaughSFX;
    private AudioSource[] girlLaugh;
    private int girlLaughTotal;
    public GameObject girlWantSFX;
    private AudioSource[] girlWant;
    private int girlWantTotal;
    public GameObject girlWinSFX;
    private AudioSource[] girlWin;
    private int girlWinTotal;

    public GameObject menuCharacters;
    public Animator manMenuAnimator;
    public Animator girlMenuAnimator;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);//Menu Manager lives forever
        InitializeAudio();
        MenuPanelController();//turn panels on/off correctly

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
        //menuManager.GetComponent<SceneChanging>().PlaySFXManDeath();
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        MainMenuMusicPlayer = MainMenuMusic.GetComponent<AudioSource>();
        WinMenuMusicPlayer = WinMenuMusic.GetComponent<AudioSource>();
        LoseMenuMusicPlayer = LoseMenuMusic.GetComponent<AudioSource>();
        Level1MusicPlayer = Level1Music.GetComponent<AudioSource>();
        Level2MusicPlayer = Level2Music.GetComponent<AudioSource>();
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
        electricity = electricitySFX.GetComponentsInChildren<AudioSource>();
        electricityTotal = electricity.Length;
        girlJump = girlJumpSFX.GetComponentsInChildren<AudioSource>();
        girlJumpTotal = girlJump.Length;
        girlLaugh = girlLaughSFX.GetComponentsInChildren<AudioSource>();
        girlLaughTotal = girlLaugh.Length;
        girlWant = girlWantSFX.GetComponentsInChildren<AudioSource>();
        girlWantTotal = girlWant.Length;
        girlWin = girlWinSFX.GetComponentsInChildren<AudioSource>();
        girlWinTotal = girlWin.Length;
    }

    public void PlaySFXGirlWant()
    {
        int random = Random.Range(0, girlWantTotal);
        girlWant[random].Play();
    }

    public void PlaySFXGirlWin()
    {
        int random = Random.Range(0, girlWinTotal);
        girlWin[random].Play();
    }

    public void PlaySFXGirlJump()
    {
        int random = Random.Range(0, girlJumpTotal);
        girlJump[random].Play();
    }

    public void PlaySFXGirlLaugh()
    {
        int random = Random.Range(0, girlLaughTotal);
        girlLaugh[random].Play();
    }
    
    public void PlaySFXelectricity()
    {
        int random = Random.Range(0, electricityTotal);
        electricity[random].Play();
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
            //MenuChanger();
            Credits();
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
        if (VariableKeeper.menuState == 0) // Main menu
        {
            //Debug.Log("Main Menu");
            PanelMainMenu.SetActive(true);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            PanelCredits.SetActive(false);
            MainMenuMusicPlayer.Play();
            PlayerMovement.MovementEnabled = false;

            //menuCamera.gameObject.SetActive(true);
            menuCharacters.gameObject.SetActive(true);
            manMenuAnimator.SetTrigger("IntroAndWin");
            girlMenuAnimator.SetTrigger("IntroAndWin");
        }
        else if (VariableKeeper.menuState == 1) // Win
        {
            //Debug.Log("Win Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(true);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            PanelCredits.SetActive(false);
            WinMenuMusicPlayer.Play();
            PlayerMovement.MovementEnabled = false;

            //menuCamera.gameObject.SetActive(true);
            menuCharacters.gameObject.SetActive(true);
            manMenuAnimator.SetTrigger("Lose");
            girlMenuAnimator.SetTrigger("IntroAndWin");
        }
        else if (VariableKeeper.menuState == 2) // Lose
        {
            //Debug.Log("Lose Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(true);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            PanelCredits.SetActive(false);
            LoseMenuMusicPlayer.Play();
            PlayerMovement.MovementEnabled = false;

            //menuCamera.gameObject.SetActive(true);
            menuCharacters.gameObject.SetActive(true);
            manMenuAnimator.SetTrigger("IntroAndWin");
            girlMenuAnimator.SetTrigger("Lose");
        }
        else if (VariableKeeper.menuState == 3)  // In game
        {
            //menuCamera.gameObject.SetActive(false);

            //Debug.Log("Game Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(true);
            PanelButtons.SetActive(false);
            PanelCredits.SetActive(false);
            if (VariableKeeper.levelProgression == 1)
            {
                Level1MusicPlayer.Play();
            } else
            {
                Level2MusicPlayer.Play();
            }
            PlayerMovement.MovementEnabled = true;


            menuCharacters.gameObject.SetActive(false);
        }

        else if (VariableKeeper.menuState == 4)  // Credits
        {
            //Debug.Log("Lose Menu");
            PanelMainMenu.SetActive(false);
            PanelWinMenu.SetActive(false);
            PanelLoseMenu.SetActive(false);
            PanelGameMenu.SetActive(false);
            PanelButtons.SetActive(true);
            PanelCredits.SetActive(true);
            LoseMenuMusicPlayer.Play();
            PlayerMovement.MovementEnabled = false;

            //menuCamera.gameObject.SetActive(true);
            menuCharacters.gameObject.SetActive(false);
            manMenuAnimator.SetTrigger("IntroAndWin");
            girlMenuAnimator.SetTrigger("Lose");
        }
    }


    //Debug to change menu
    //public void MenuChanger()
    //{
    //    Debug.Log("The menu was " + VariableKeeper.menuState);
    //    if (VariableKeeper.menuState == 2)
    //    {
    //        VariableKeeper.menuState = 0;
    //        MenuPanelController();
    //    }
    //    else
    //    {
    //        VariableKeeper.menuState = VariableKeeper.menuState + 1;
    //        MenuPanelController();
    //    }
    //    Debug.Log("The menu is "+VariableKeeper.menuState);
    //}
    
    //load main menu
    public void ReturnToMenu()
    {
        StopAllAudio();
        PlaySFXButtonPress();
        VariableKeeper.menuState = 0;
    }

    //load first level
    public void StartGame()
    {
        StopAllAudio();
        VariableKeeper.menuState = 3;
        VariableKeeper.levelProgression = 1;
        PlaySFXButtonPress();
        PlaySFXGirlWant();
        SceneManager.LoadScene(VariableKeeper.levelStart);
        VariableKeeper.isIceCreamLicked = false;
    }

    public void Credits()
    {
        StopAllAudio();
        PlaySFXButtonPress();
        VariableKeeper.menuState = 4;
    }

    //load scene from last play
    public void ContinueGame()
	{
        StopAllAudio();
        VariableKeeper.menuState = 3;
        PlaySFXButtonPress();
        PlaySFXGirlWant();
        SceneManager.LoadScene(VariableKeeper.levelProgression);
        VariableKeeper.isIceCreamLicked = false;
    }

    //load next level
    public void NextGame()
    {
        StopAllAudio();
        VariableKeeper.menuState = 3;
        PlaySFXGirlWin();
        Level2MusicPlayer.Play();
        VariableKeeper.levelProgression = VariableKeeper.levelProgression + 1;
        VariableKeeper.isIceCreamLicked = false;
        SceneManager.LoadScene(VariableKeeper.levelProgression);
    }

    //win game
    public void WinGame()
    {
        StopAllAudio();
        VariableKeeper.menuState = 1;
        MenuPanelController();
        VariableKeeper.levelProgression = 1;
        PlaySFXGirlWin();
        VariableKeeper.isIceCreamLicked = false;

    }

    //lose game
    public void LoseGame()
    {
        StopAllAudio();
        VariableKeeper.menuState = 2;
        VariableKeeper.isIceCreamLicked = false;
        Debug.Log("did I lose?");
    }

    //quit game
    public void QuitGame()
    {
        StopAllAudio();
        Application.Quit();
    }

    //Stop all sounds
    void StopAllAudio()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

}
