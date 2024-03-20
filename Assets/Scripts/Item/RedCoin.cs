using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : Item
{
    private void Awake()
    {
        coin = 10 * JewalBox.instance.Pow;
        exp = 0;
    }
}
