using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Game Manager Class 
*/


/// <summary>
/// 
/// </summary>
public class GameManager : Singleton<GameManager> {

    public int Score { get { return scoreManager.Score; } }

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;

    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private PlatformManager platformManager;
    

    [SerializeField]
    private GameObject platformPrefab;

    private GameObject[] platformArray;
    private float platformIndex;

    [SerializeField]
    private GameUIController gameUIControl;

    private float minX = -2.5f;
    private float maxX = 2.5f;
    private float minY = -4.7f;
    private float maxY = -3.7f;

    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //CreateAllPlatforms();
        //CreateInitialPlatforms();
    }

    private void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            
        }

        if (gameUIControl == null)
        {
            gameUIControl = FindObjectOfType<GameUIController>();
        }

        platformManager.SpawnPlatformWithPlayer(player);
    }

    /// <summary>
    /// Update called every frame after physics calculations and 
    /// object transformation updates. 
    /// </summary>
    private void LateUpdate()
    {
        if (lerpCamera)
        {
            LerpTheCamera();
        }
    }


    void CreateAllPlatforms()
    {
        platformManager.CreatePlatformArray();
        platformManager.SpawnPlatformWithPlayer(player);
        platformManager.SpawnPlatform();

    }

    /// <summary>
    /// 
    /// </summary>
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

    void CreateInitialPlatformsOLD()
    {
        Vector3 tempPosition = new Vector3(Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);

        Instantiate(platformPrefab, tempPosition, Quaternion.identity);

        // adjust position for player
        tempPosition.y += 2f;
        // create player
        Instantiate(playerPrefab, tempPosition, Quaternion.identity);

        tempPosition = new Vector3(Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);

        Instantiate(platformPrefab, tempPosition, Quaternion.identity);
    }

    /// <summary>
    /// 
    /// </summary>
    void LerpTheCamera()
    {
        float x = Camera.main.transform.position.x;

        x = Mathf.Lerp(x, lerpX, lerpTime * Time.deltaTime);

        Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if (Camera.main.transform.position.x >= (lerpX - 0.07f))
        {
            lerpCamera = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lerpPosition"></param>
    public void CreateNewPlatformAndLerp(float lerpPosition)
    {
        //CreateNewPlatform();
        platformManager.SpawnPlatform();
        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void CreateNewPlatform()
    {
        float cameraX = Camera.main.transform.position.x;
        float newMaxX = (maxX * 2) + cameraX;

        Instantiate(platformPrefab, new Vector3(Random.Range(newMaxX, newMaxX - 1.2f), Random.Range(maxY, maxY - 1.2f), 0), Quaternion.identity);

    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver()
    {
        // Show game over UI
        gameUIControl.ShowGameOverPanel();
        // Update score
        gameUIControl.UpdateFinalScore("Score\n" + "" + Score);

        // Hide Player
        //player.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncrementScore()
    {
        scoreManager.IncrementScore();
    }
}
