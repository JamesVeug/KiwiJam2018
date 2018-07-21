using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour {

    public GameObject MenuMusic;
    private AudioSource MenuMusicGenerator;
    public int levelMainMenu = 0;
    public int levelStart = 0;
    public int levelProgression = 1;
	
	// Use this for initialization
	void Start () {
        MenuMusicGenerator = MenuMusic.GetComponent<AudioSource>();
        MenuMusicGenerator.Play();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("level starting at "+levelStart);
		//Debug.Log("level continuing at "+levelProgression);
	}

    //load first scene
    public void StartGame()
    {
        MenuMusicGenerator.Stop();
        SceneManager.LoadScene(levelStart);
    }
    
    //load scene from last play
    public void ContinueGame()
	{
        MenuMusicGenerator.Stop();
		SceneManager.LoadScene(levelProgression);
	}

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(levelMainMenu);
    }

    //quit game
    public void QuitGame()
    {
        MenuMusicGenerator.Stop();
        Application.Quit();
    }


}
