using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject platformPrefab;

    private float minX = -2.5f;
    private float maxX = 2.5f;
    private float minY = -4.7f;
    private float maxY = -3.7f;

    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;

    private void Awake()
    {
        MakeInstance();
        CreateInitialPlatforms();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void CreateInitialPlatforms()
    {
        Vector3 tempPosition = new Vector3(Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);

        Instantiate(platformPrefab, tempPosition, Quaternion.identity );

        // adjust position for player
        tempPosition.y += 2f;
        // create player
        Instantiate(playerPrefab, tempPosition, Quaternion.identity);

        tempPosition = new Vector3(Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);

        Instantiate(platformPrefab, tempPosition, Quaternion.identity);
    }

    public void CreateNewPlatformAndLerp(float lerpPosition)
    {
        CreateNewPlatform();
        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    void CreateNewPlatform()
    {
        float cameraX = Camera.main.transform.position.x;
        float newMaxX = (maxX * 2) + cameraX;

        Instantiate(platformPrefab, new Vector3(Random.Range(newMaxX, newMaxX - 1.2f), Random.Range(maxY, maxY - 1.2f), 0), Quaternion.identity);

    }
}
