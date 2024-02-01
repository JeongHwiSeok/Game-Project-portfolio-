using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitionaryManager : Singleton<DitionaryManager>
{
    Dictionary<KeyCode, Action> keyDictionary;

    private void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.RightArrow, RightArrow },
            {KeyCode.LeftArrow, LeftArrow },
            {KeyCode.UpArrow, UpArrow},
            {KeyCode.DownArrow, DownArrow }
        };
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            foreach(var dic in keyDictionary)
            {
                if(Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    private void RightArrow()
    {

    }

    private void LeftArrow()
    {

    }

    private void UpArrow()
    {

    }

    private void DownArrow()
    {

    }
}
