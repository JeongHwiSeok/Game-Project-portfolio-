using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternityFlameBullet : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        transform.position = new Vector3(0, 0, 0);
        AttackDirection(target);
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

    public void Target(Vector3 _target)
    {
        target = _target;
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableZone zone = collision.GetComponent<DisableZone>();

        if (zone != null)
        {
            gameObject.SetActive(false);
        }
    }
}
