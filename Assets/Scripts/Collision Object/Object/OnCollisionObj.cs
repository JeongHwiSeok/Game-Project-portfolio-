using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionObj : CollisionObject
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] MapManager mapManager;
    [SerializeField] Vector2 position;

    private void Awake()
    {
        mapManager = GameObject.Find("Map").GetComponent<MapManager>();
        spriteRenderer = this.GetComponentInParent<SpriteRenderer>();
    }

    public override void CollisionActivate(PlayerManager player)
    {
        position = player.pos;

        if (player.pos.x > 0)
        {
            mapManager.moveXNeg = false;
        }
        else if (player.pos.x < 0)
        {
            mapManager.moveXPos = false;
        }
        if (player.pos.y > 0)
        {
            mapManager.moveYNeg = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            spriteRenderer.sortingOrder = 6;
        }
        else if (player.pos.y < 0)
        {
            mapManager.moveYPos = false;
        }
    }

    public override void CollisionUnActivate(PlayerManager player)
    {
        if (position.x > 0)
        {
            mapManager.moveXNeg = true;
        }
        else if (position.x < 0)
        {
            mapManager.moveXPos = true;
        }
        else if (position.y > 0)
        {
            mapManager.moveYNeg = true;
        }
        else if (position.y < 0)
        {
            mapManager.moveYPos = true;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        spriteRenderer.sortingOrder = 3;
        player.transform.position = Vector3.zero;
    }

    public override void Activate(PlayerManager player)
    {
        
    }
    public override void UnActivate(PlayerManager player)
    {
        
    }
}
