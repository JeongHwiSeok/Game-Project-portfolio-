using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        if(direction.x < 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0,180,0);
        }
        else if(direction.x > 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        direction.y = Input.GetAxis("Vertical");

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
        // 위치에 버퍼링걸림 - 추후 리지드바디를 이용하여 움직임을 조절
    }
}
