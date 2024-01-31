using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public SpriteRenderer renderer;
    [SerializeField] public Animator animator;

    private void OnEnable()
    {
        // InputManager.instance.keyAction += Move;
    }

    private void Start()
    {
        InputManager.instance.keyAction += Move;
        //renderer = GetComponent<SpriteRenderer>();
        //renderer.sortingOrder = -1; 레이어 렌더링 순서 조정하는법
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            animator.SetBool("Move", true);
        }
        //else if ()
        //{
        //    animator.SetBool("Move", false);
        //}
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= Move;
    }
}
