using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoLaser : MonoBehaviour
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    public void ObjectOff()
    {
        GameObject parent = transform.parent.gameObject;
        parent.transform.gameObject.SetActive(false);
    }
}
