using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheck : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(transform.position);
            transform.position = Vector3.zero;
        }
    }
}
