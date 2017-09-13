using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    // TODO remove singleton 
    public static GameOverManager instance;

    private GameObject gameOverPanel;
    private Animator gameOverAnim;

    private Button playAgainBtn, backBtn;

    private GameObject scoreText;
    private Text finalScore;

    private void Awake()
    {
        MakeInstance();
        Initialize();
    }

    // TODO CLean this up
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

    public void GameOverShowPanel()
    {
        // TODO clean up
        scoreText.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScore.text = "Score\n" + "" + ScoreManager.instance.GetScore();
        gameOverAnim.Play("FadeIn");
    }

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

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
