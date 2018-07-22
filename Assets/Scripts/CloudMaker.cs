using UnityEngine;

public class CloudMaker : MonoBehaviour
{
    public Cloud[] cloudTemplates;
    
    public float SpawnFrequency = 2;

    private float lastSpawn = 0;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (lastSpawn >= SpawnFrequency)
        {
            int cloudIndex = Random.Range(0, cloudTemplates.Length);
            var cloud = Instantiate(cloudTemplates[cloudIndex], transform, false);
            cloud.Initialize();
            Camera cam = SkyCanvas.camera;
            float height = Screen.height;
            float width = Screen.width;

            float y = Random.Range(-(height/2), height/2);
            float x = cloud.moveRight ? -(width/2-2) : (width/2+1);
            cloud.transform.localPosition = new Vector3(x, y, cloud.transform.position.z);

            lastSpawn = 0;
        }
        else
        {
            lastSpawn += Time.fixedDeltaTime;
        }

    }
}
