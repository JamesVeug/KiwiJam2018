using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    public void OnPickup()
    {
        var menuManager = GameObject.Find("MenuManager");
        if (menuManager != null)
        {
            menuManager.GetComponent<SceneChanging>().NextGame();
        }
    }
}
