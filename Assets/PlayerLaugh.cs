using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaugh : MonoBehaviour
{

    public Vector2 LaughDelay = new Vector2();
    private float laughDelta;

    private void Awake()
    {
        laughDelta = Random.Range(LaughDelay.x, LaughDelay.y);
    }

    // Update is called once per frame
    void Update () {
        if (VariableKeeper.menuState == 3)
        {
            laughDelta -= Time.deltaTime;
            if (laughDelta < 0)
            {
                SceneChanging.Instance.PlaySFXGirlLaugh();
                laughDelta = Random.Range(LaughDelay.x, LaughDelay.y);
            }
        }
    }
}
