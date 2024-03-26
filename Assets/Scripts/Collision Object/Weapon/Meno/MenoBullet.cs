using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoBullet : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        point = MenoManager.instance.point;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg);
        transform.position = new Vector3(0, 0, 0);
        atk = 10 * MenoManager.instance.pickUpBuff * MenoManager.instance.atkBuff;
        speed = 8f * BuffDebuffManager.instance.pwsSpeedBuff * MenoManager.instance.spdBuff;
        knockBack = MenoManager.instance.knockBack;
        AttackDirection(point);
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            PositionStatus(direction);
        }
    }

    private void AttackDirection(Vector3 position)
    {
        direction = position - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    private void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
