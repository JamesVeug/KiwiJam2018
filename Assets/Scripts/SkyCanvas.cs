using UnityEngine;
using System.Collections;

public class SkyCanvas : MonoBehaviour
{
    public static Canvas canvas;
    public static Camera camera;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        camera = canvas.worldCamera;
    }
}