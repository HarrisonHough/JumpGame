using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class MainMenuController : MonoBehaviour {

    //TODO add in editor editing of which scene to load
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
