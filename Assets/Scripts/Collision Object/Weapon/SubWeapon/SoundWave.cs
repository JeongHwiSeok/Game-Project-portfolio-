using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    [SerializeField] float slow;

    public float Slow
    {
        set { slow = value; }
        get { return slow; }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableOn());
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    private IEnumerator DisableOn()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
