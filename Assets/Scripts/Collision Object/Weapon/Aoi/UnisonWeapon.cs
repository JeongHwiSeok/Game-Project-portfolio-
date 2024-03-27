using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnisonWeapon : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    [SerializeField] GameObject secondHand;

    private float time;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 2f, 0);
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            AttackDirection();
            time += Time.deltaTime;
            CheckDgree();
        }  
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -normalspeed * Time.deltaTime);
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

    public GameObject SecondHand()
    {
        return secondHand;
    }
}
