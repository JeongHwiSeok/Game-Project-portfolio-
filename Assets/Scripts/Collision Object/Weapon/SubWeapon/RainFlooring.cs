using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFlooring : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    [SerializeField] public float time;

    [SerializeField] public int itemLV;

    private void OnEnable()
    {
        StartCoroutine(DisableOff());
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
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
