using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    Idle,
    Move
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance
    {
        get;
        private set;
    }

    [SerializeField] public SpriteRenderer renderer;
    [SerializeField] public Animator animator;
    [SerializeField] Movement movement;

    [SerializeField] public float horizontal;
    [SerializeField] public float vertical;

    private void OnEnable()
    {
        // InputManager.instance.keyAction += Move;
    }

    private void Start()
    {
        instance = this;
        InputManager.instance.keyAction += Move;
        movement = Movement.Idle;
    }

    private void Update()
    {
        Status();
    }
    private void Move()
    {
        if(GameManager.instance.state == false)
        {
            return;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
            movement = Movement.Move;
            Status();
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            movement = Movement.Move;
            Status();
        }
        else if(Input.GetAxis("Vertical") != 0)
        {
            movement = Movement.Move;
            Status();
        }
    }

    public void Idle()
    {
        movement = Movement.Idle;
    }

    private void Status()
    {
        switch(movement)
        {
            case Movement.Idle:
                animator.Play("Idle");
                break;
            case Movement.Move:
                animator.Play("run");
                break;
        }
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= Move;
    }
}
