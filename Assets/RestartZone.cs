using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartZone : MonoBehaviour
{
    public static bool Active = true;

	public void RestartGame()
    {
        var menuManager = GameObject.Find("MenuManager");
        if (menuManager != null)
        {
            menuManager.GetComponent<SceneChanging>().LoseGame();
            menuManager.GetComponent<SceneChanging>().PlaySFXGirlDeath();
        }
    }
}
