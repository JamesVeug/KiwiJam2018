using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public Image image;

    public Vector2 ScaleRange = new Vector2(0.25f, 1);
    public Vector2 SpeedRange = new Vector2(0.025f, 0.05f);
    public bool moveRight = true;
    public float MaxLife = 3;
    public float SpawninTime;

    private float life;
    private float moveSpeed;
    
    public void Initialize()
    {
        moveRight = Random.Range(0, 100) < 50;
        moveSpeed = Random.Range(SpeedRange.x, SpeedRange.y);

        float scale = Random.Range(ScaleRange.x, ScaleRange.y);
        transform.localScale = new Vector2(scale, scale);
    }

    IEnumerator Start()
    {
        yield return FadeImage(true);
        yield return new WaitForSeconds(MaxLife);
        yield return FadeImage(false);
        Destroy(gameObject);
    }

    public IEnumerator FadeImage(bool fadein)
    {
        float time = 0;
        while (time < SpawninTime)
        {
            float alpha = time / SpawninTime;
            if (!fadein)
            {
                alpha = 1 - alpha;
            }

            image.color = new Color(image.color.r, image.color.b, image.color.g, alpha);
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }
        image.color = new Color(image.color.r, image.color.b, image.color.g, 1);
    }

	void FixedUpdate ()
    {
        Vector3 move = new Vector3(moveSpeed, 0, 0);
        if (!moveRight) move *= -1;

        transform.position += move;
	}
}
