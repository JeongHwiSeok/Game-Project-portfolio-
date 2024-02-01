using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> mapList;
    
    public float offsetX = 38.4f;
    public float offsetY = 21.6f;

    public Vector3 direction;

    public float speed;

    private void OnEnable()
    {
        // InputManager.instance.keyAction += MapMove;
    }

    private void Start()
    {
        InputManager.instance.keyAction += MapMove;
        speed = GameManager.instance.CharacterSpeed;
        mapList.Capacity = 20;
    }

    public void MapMove()
    {
        direction.x = Input.GetAxis("Horizontal") * -1;

        direction.y = Input.GetAxis("Vertical") * -1;

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= MapMove;
    }
}
