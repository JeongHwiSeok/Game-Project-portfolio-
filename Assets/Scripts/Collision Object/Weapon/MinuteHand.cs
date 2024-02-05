using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHand : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private float speed;

    public float Speed
    {
        get { return speed; }
    }

    private void OnEnable()
    {
        point = Input.mousePosition;
        // target = new Vector3(1, 0, 0);
        // transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg - 90);
        transform.position = new Vector3(0, 1.5f, 0);
        speed = 240f;
    }

    private void Update()
    {
        AttackDirection();
        // PositionStatus(direction);
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, - speed * Time.deltaTime);
    }

    //private void AttackDirection(Vector3 position)
    //{
    //    direction = position - transform.position;

    //    direction.Normalize();

    //    PositionStatus(direction);
    //}

    //private void PositionStatus(Vector3 position)
    //{
    //    gameObject.transform.Translate(position * speed * Time.deltaTime, Space.World);
    //}
}
