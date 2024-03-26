using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action keyAction;

    public void Update()
    {
        if (GameManager.instance.state)
        {
            if (Input.anyKey == false)
            {
                if (PlayerManager.instance != null)
                {
                    PlayerManager.instance.Idle();
                }
                else
                {
                    return;
                }
            }

            if (keyAction != null)
            {
                keyAction.Invoke();
            }
        }
    }
}
