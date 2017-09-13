using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public void OnPointerDown(PointerEventData data)
    {
        if (PlayerJump.instance != null)
        {
            PlayerJump.instance.SetPower(true);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (PlayerJump.instance != null)
        {
            PlayerJump.instance.SetPower(false);
        }
    }

}
