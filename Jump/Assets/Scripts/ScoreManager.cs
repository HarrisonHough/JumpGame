using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ScoreManager : Singleton<ScoreManager> {

    //public static ScoreManager instance;

    private Text scoreText;

    // TODO make this private and have a public one for easy getter
    private int score;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        //MakeInstance();
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }

    /// <summary>
    /// 
    /// </summary>
    void MakeInstance()
    {
        //if (instance == null)
        //{
        //    instance = this;
       // }
       // else
       // {
//
      //  }
    }

    /// <summary>
    /// 
    /// </summary>
    public void IncrementScore()
    {
        score++;
        scoreText.text = "" + score;
    }

    // TODO clean this up
    public int GetScore()
    {
        return this.score;
    }

}
