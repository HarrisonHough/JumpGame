using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameOverManager : Singleton<GameOverManager> {

    // TODO remove singleton 
    //public static GameOverManager instance;

    private GameObject gameOverPanel;
    private Animator gameOverAnim;

    private Button playAgainBtn, backBtn;

    private GameObject scoreText;
    private Text finalScore;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOverShowPanel()
    {
        // TODO clean up
        scoreText.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScore.text = "Score\n" + "" + ScoreManager.Instance.GetScore();
        gameOverAnim.Play("FadeIn");
    }

    /// <summary>
    /// 
    /// </summary>
    void Initialize()
    {

        // TODO improve finding / setting of these variables
        gameOverPanel = GameObject.Find("Game Over Panel Holder");
        gameOverAnim = gameOverPanel.GetComponent<Animator>();
        playAgainBtn = GameObject.Find("Restart Button").GetComponent<Button>();
        backBtn = GameObject.Find("Back Button").GetComponent<Button>();

        playAgainBtn.onClick.AddListener(() => PlayAgain());
        backBtn.onClick.AddListener(() => BackToMenu());

        scoreText = GameObject.Find("Score Text");
        finalScore = GameObject.Find("Final Score Text").GetComponent<Text>();

        // hide panel
        gameOverPanel.SetActive(false);
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

}
