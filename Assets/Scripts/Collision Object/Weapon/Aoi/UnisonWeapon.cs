using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnisonWeapon : MonoBehaviour
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private float speed;
    private float time;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 2.5f, 0);
        speed = 360f;
    }

    private void Update()
    {
        AttackDirection();
        time += Time.deltaTime;
        CheckDgree();
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    private void CheckDgree()
    {
        if(gameObject.activeSelf && time > 1f)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.position = new Vector3(0, 2.5f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
