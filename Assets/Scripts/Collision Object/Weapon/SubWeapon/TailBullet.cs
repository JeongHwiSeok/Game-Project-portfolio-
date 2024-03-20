using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBullet : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        transform.position = new Vector3(0, 0, 0);
        AttackDirection(target);
        StartCoroutine(DisableOn());
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
        transform.position = Vector3.Lerp(transform.position, direction , speed * Time.deltaTime);
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void Target(Vector3 _target)
    {
        target = _target;
    }

    private IEnumerator DisableOn()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}
