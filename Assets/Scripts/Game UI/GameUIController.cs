using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Game UI Controller Class 
*/

/// <summary>
/// 
/// </summary>
public class GameUIController : MonoBehaviour {

    [SerializeField]
    private CanvasGroup gameOverPanelHolder;

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text finalScoreText;

    [SerializeField]
    private Animator gameOverAnim;

    [SerializeField]
    private PlayerJump player;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        Initialize();
	}

    /// <summary>
    /// 
    /// </summary>
    void Initialize()
    {
        NullReferenceChecks();

        // Add button functionality
        backButton.onClick.AddListener(() => BackToMenu());
        restartButton.onClick.AddListener(() => PlayAgain());

        // Hide game over panel on start
        gameOverPanelHolder.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void NullReferenceChecks()
    {
        if (gameOverPanelHolder == null)
        {
            gameOverPanelHolder = GameObject.Find("Game Over Panel Holder").GetComponent<CanvasGroup>();
        }

        if (backButton == null)
        {
            backButton = GameObject.Find("Back Button").GetComponent<Button>();
        }

        if (restartButton == null)
        {
            restartButton = GameObject.Find("Restart Button").GetComponent<Button>();
        }

        if (scoreText == null)
        {
            scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        }

        if (finalScoreText == null)
        {
            finalScoreText = GameObject.Find("Final Score Text").GetComponent<Text>();
        }

        if (gameOverAnim == null)
        {
            gameOverAnim = gameOverPanelHolder.GetComponent<Animator>();
        }

        player = FindObjectOfType<PlayerJump>();
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateFinalScore(string score)
    {
        finalScoreText.text = score;
    }
    /// <summary>
    /// 
    /// </summary>
    public void PlayAgain()
    {
        GameManager.Instance.PlayAgain();
    }

    /// <summary>
    /// 
    /// </summary>
    public void BackToMenu()
    {
        GameManager.Instance.BackToMenu();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowGameOverPanel()
    {
        scoreText.gameObject.SetActive(false);
        gameOverPanelHolder.gameObject.SetActive(true);
        finalScoreText.text = "Score\n" + "" + GameManager.Instance.Score;
        gameOverAnim.Play("FadeIn");
    }

    /// <summary>
    /// 
    /// </summary>
    public void JumpButtonDown()
    {
        player.SetPower(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void JumpButtonUp()
    {
        player.SetPower(false);
    }

}
