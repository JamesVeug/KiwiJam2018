using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    public void OnPickup()
    {
        if (VariableKeeper.levelProgression == 2)
        {
            var menuManager = GameObject.Find("MenuManager");
            if (menuManager != null)
            {
                menuManager.GetComponent<SceneChanging>().WinGame();
                VariableKeeper.isIceCreamLicked = true;
            }
        }
        else if (!VariableKeeper.isIceCreamLicked)
        {
            var menuManager = GameObject.Find("MenuManager");
            if (menuManager != null)
            {
                Debug.Log("Nooooo!");
                menuManager.GetComponent<SceneChanging>().NextGame();
            }
        }
    }
}
