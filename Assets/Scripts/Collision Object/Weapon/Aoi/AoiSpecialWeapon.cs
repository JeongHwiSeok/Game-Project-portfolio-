using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiSpecialWeapon : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] CapsuleCollider2D capsuleCollider;

    [SerializeField] Animator animator;

    [SerializeField] bool drop;

    public Vector3 Target
    {
        set { target = value; }
        get { return target; }
    }

    private void OnEnable()
    {
        animator.SetBool("Drop", true);
        capsuleCollider.enabled = false;
        drop = false;
    }

    private void Update()
    {
        if (drop)
        {
            return;
        }
        else
        {
            if (GameManager.instance.state)
            {
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10);

                if ((transform.position - target).sqrMagnitude < 0.1f)
                {
                    transform.position = target;
                    animator.SetBool("Landing", true);
                    animator.SetBool("Drop", false);
                    capsuleCollider.enabled = true;
                    drop = true;
                    StartCoroutine(DeleteTime());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();

        if (player != null)
        {
            if (AoiManager.instance.buffCheck)
            {
                AoiManager.instance.AoiSpecialWeaponBuffStart();
            }
            else
            {
                AoiManager.instance.aoiSpecialWeaponTime = GameManager.instance.time;
            }
            AoiManager.instance.buffCheck = false;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(30f);
        gameObject.SetActive(false);
    }
}
