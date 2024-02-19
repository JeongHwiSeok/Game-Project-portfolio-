using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> mapList;

    [SerializeField] public bool moveXPos;
    [SerializeField] public bool moveXNeg;
    [SerializeField] public bool moveYPos;
    [SerializeField] public bool moveYNeg;

    public float offsetX = 38.4f;
    public float offsetY = 21.6f;

    public Vector3 direction;

    private void OnEnable()
    {
        // InputManager.instance.keyAction += MapMove;
    }

    private void Start()
    {
        InputManager.instance.keyAction += MapMove;
        moveXPos = true;
        moveXNeg = true;
        moveYPos = true;
        moveYNeg = true;
        mapList.Capacity = 20;
    }

    public void MapMove()
    {
        direction.x = Input.GetAxis("Horizontal") * -1;

        direction.y = Input.GetAxis("Vertical") * -1;

        if (moveXNeg == false)
        {
            if (direction.x > 0)
            {
                direction.x = 0;
            }
        }
        if (moveXPos == false)
        {
            if (direction.x < 0)
            {
                direction.x = 0;
            }
        }
        if (moveYNeg == false)
        {
            if (direction.y > 0)
            {
                direction.y = 0;
            }
        }
        if (moveYPos == false)
        {
            if (direction.y < 0)
            {
                direction.y = 0;
            }
        }

        direction.Normalize();

        transform.position += direction * GameManager.instance.CharacterSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= MapMove;
    }
}
