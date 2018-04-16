using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    [SerializeField]
    private GameObject platformPrefab;

    private GameObject[] platformArray;
    private int platformIndex = 0;

    private float minX = -2.5f;
    private float maxX = 2.5f;
    private float minY = -2.7f;
    private float maxY = -1.7f;


    private void Awake()
    {
        CreatePlatformArray();
    }

    // Use this for initialization
    void Start () {
		
	}


    public void CreatePlatformArray()
    {
        platformArray = new GameObject[10];
        Vector3 offScreenPosition = new Vector3(0,-100,0);
        for (int i = 0; i < 10; i++)
        {
            platformArray[i] = Instantiate(platformPrefab, offScreenPosition, Quaternion.identity);
            platformArray[i].transform.parent = transform;
            
        }
    }

    public void SpawnPlatform()
    {
        float cameraX = Camera.main.transform.position.x;
        float newMaxX = (maxX * 2) + cameraX;

        Vector3 tempPosition = new Vector3(Random.Range(newMaxX, newMaxX - 1.2f), Random.Range(maxY, maxY - 1.2f), 0);

        platformArray[platformIndex].transform.position = tempPosition;
        platformArray[platformIndex].tag = "Platform";
        if (platformIndex >= 9)
            platformIndex = 0;
        else
            platformIndex++;
    }

    public void SpawnPlatformWithPlayer(GameObject player)
    {

        Vector3 tempPosition = new Vector3(Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);

        
        platformArray[platformIndex].transform.position = tempPosition;
        platformArray[platformIndex].tag = "Platform";

        tempPosition.y += 2f;
        player.transform.position = tempPosition;

        if (platformIndex > 9)
            platformIndex = 0;
        else
            platformIndex++;

        tempPosition = new Vector3(Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);

        platformArray[platformIndex].transform.position = tempPosition;
        platformArray[platformIndex].tag = "Platform";

        if (platformIndex > 9)
            platformIndex = 0;
        else
            platformIndex++;


    }
}
