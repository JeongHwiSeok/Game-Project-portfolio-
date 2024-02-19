using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffCollisionObj : CollisionObject
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public override void Activate(PlayerManager player)
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        spriteRenderer.sortingOrder = 6;
    }

    public override void UnActivate(PlayerManager player)
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        spriteRenderer.sortingOrder = 3;
    }
}
