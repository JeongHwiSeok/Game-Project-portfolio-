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
        point = MenoWeapon.instance.point;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg);
        transform.position = new Vector3(0, 0, 0);
        atk = 10 * MenoWeapon.instance.pickUpBuff;
        speed = 8f * GameManager.instance.pwsBuff;
        AttackDirection(point);
    }

    private void Update()
    {
        PositionStatus(direction);
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
