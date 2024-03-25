using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiSpecialWeapon : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] CapsuleCollider2D capsuleCollider;

    [SerializeField] Animator animator;

    public Vector3 Target
    {
        set { target = value; }
        get { return target; }
    }

    private void OnEnable()
    {
        animator.SetBool("Drop", true);
        capsuleCollider.enabled = false;
        rigidbody.gravityScale = 1;
    }

    private void Update()
    {
        if (transform.localPosition.y - target.y <= 0.1f)
        {
            rigidbody.gravityScale = 0;
            transform.localPosition = target;
            animator.SetBool("Landing", true);
            animator.SetBool("Drop", false);
            capsuleCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();

        if (player != null)
        {
            if (AoiWeapon.instance.buffCheck)
            {
                AoiWeapon.instance.AoiSpecialWeaponBuffStart();
            }
            else
            {
                AoiWeapon.instance.aoiSpecialWeaponTime = UIManager.instance.time;
            }
            AoiWeapon.instance.buffCheck = false;
            gameObject.SetActive(false);
        }
    }
}
