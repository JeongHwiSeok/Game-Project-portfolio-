using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHand : Weapon
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
        // transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg - 90);
        transform.position = savePoint;
        speed = 10f;
    }

    private void Update()
    {
        AttackDirection(target);
        // PositionStatus(direction);
    }

    private void AttackDirection(Vector3 target)
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        savePoint = transform.position;
    }
}
