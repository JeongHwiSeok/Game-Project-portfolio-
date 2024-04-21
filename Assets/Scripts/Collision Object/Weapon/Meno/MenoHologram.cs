using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoHologram : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = PlayerManager.instance.gameObject.GetComponent<SpriteRenderer>().flipX;
    }
}
