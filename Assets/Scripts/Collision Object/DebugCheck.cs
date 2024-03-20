using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheck : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Random.Range(0, 21));
    }
}
