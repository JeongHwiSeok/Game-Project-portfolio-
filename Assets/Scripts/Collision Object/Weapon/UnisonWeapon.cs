using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnisonWeapon : MonoBehaviour
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 savePoint;

    private float speed;

    private void OnEnable()
    {
        point = Input.mousePosition;
        target = new Vector3(1, 0, 0);
        transform.position = new Vector3(0, 2.5f, 0);
        speed = 360f;
    }

    private void Update()
    {
        AttackDirection();
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        AoiWeapon.instance.Attack();
    }
}
