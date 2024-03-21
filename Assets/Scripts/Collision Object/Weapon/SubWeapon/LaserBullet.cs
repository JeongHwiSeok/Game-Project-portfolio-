using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90);
        transform.position = new Vector3(0, 0, 0);
        AttackDirection(target);
        StartCoroutine(DisableOff());
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

    private IEnumerator DisableOff()
    {
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}
