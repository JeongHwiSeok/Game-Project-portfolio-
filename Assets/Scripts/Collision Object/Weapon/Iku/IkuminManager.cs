using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminManager : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] Movement movement;

    private void Start()
    {
        movement = Movement.Idle;
    }

    private void Update()
    {
        movement = PlayerManager.instance.movement;
        Status();
    }

    private void Status()
    {
        switch (movement)
        {
            case Movement.Idle:
                animator.Play("Idle");
                break;
            case Movement.Move:
                animator.Play("run");
                break;
        }
    }
}
