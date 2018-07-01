using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Main Menu Controller Class 
*/


/// <summary>
/// 
/// </summary>
public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
