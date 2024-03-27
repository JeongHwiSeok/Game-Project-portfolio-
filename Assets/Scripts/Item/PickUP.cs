using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] public float menoHologram;

    public static PickUP instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
        menoHologram = 1;
    }

    public void PickUPRangeUP()
    {
        circleCollider2D.radius = 1 * BuffDebuffManager.instance.shpPickUpRangePow * menoHologram * BuffDebuffManager.instance.shopExp;
    }

    public float PickUPRange()
    {
        return circleCollider2D.radius;
    }
}
