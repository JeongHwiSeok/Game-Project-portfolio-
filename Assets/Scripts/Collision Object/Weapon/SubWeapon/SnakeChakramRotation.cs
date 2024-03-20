using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChakramRotation : Weapon
{
    [SerializeField] float time;
    [SerializeField] float duration;

    private void Update()
    {
        if (GameManager.instance.state)
        {
            AttackDirection();

            time += Time.deltaTime;
            CheckDgree();
        }
    }

    private void CheckDgree()
    {
        if (gameObject.activeSelf && time > duration)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    private void AttackDirection()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -speed * Time.deltaTime);

        if (transform.position.x > 0 && transform.position.y == 0)
        {
            transform.position += new Vector3(Time.deltaTime, 0, 0);
        }
        else if (transform.position.x > 0 && transform.position.y > 0)
        {
            transform.position += new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
        else if (transform.position.x == 0 && transform.position.y > 0)
        {
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }
        else if (transform.position.x < 0 && transform.position.y > 0)
        {
            transform.position += new Vector3(-Time.deltaTime, Time.deltaTime, 0);
        }
        else if (transform.position.x < 0 && transform.position.y == 0)
        {
            transform.position += new Vector3(-Time.deltaTime, 0, 0);
        }
        else if (transform.position.x < 0 && transform.position.y < 0)
        {
            transform.position += new Vector3(-Time.deltaTime, -Time.deltaTime, 0);
        }
        else if (transform.position.x == 0 && transform.position.y < 0)
        {
            transform.position += new Vector3(0, -Time.deltaTime, 0);
        }
        else if (transform.position.x > 0 && transform.position.y < 0)
        {
            transform.position += new Vector3(Time.deltaTime, -Time.deltaTime, 0);
        }
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void Duration(float num)
    {
        duration = num;
    }
}
