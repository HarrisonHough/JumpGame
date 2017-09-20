using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class JumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    // Store reference to playerJump;
    [SerializeField]
    private PlayerJump playerJump;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        // Check for null reference
        if (playerJump = null)
        {
            playerJump = FindObjectOfType<PlayerJump>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerDown(PointerEventData data)
    {
        playerJump.SetPower(true);
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void OnPointerUp(PointerEventData data)
    {
        playerJump.SetPower(true);
    }

}
