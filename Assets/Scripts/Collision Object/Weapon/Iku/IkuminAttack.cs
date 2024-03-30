using UnityEngine;

public class IkuminAttack : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 direction;
    [SerializeField] Movement movement;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] public Animator animator;

    private int count;
    private float time;

    [SerializeField] private float angle;

    [SerializeField] bool returnIkumin;
    [SerializeField] public bool damageFlag;

    private void OnEnable()
    {
        knockBack = 0.5f;
        returnIkumin = false;
        damageFlag = false;
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = new Vector3(0, 0, 0);
        speed = 5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = Movement.Attack;
        time = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
        if (gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (returnIkumin)
        {
            AttackDirection(PlayerManager.instance.transform.position);
            PositionStatus();
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            if (damageFlag)
            {
                atk = 0;
                knockBack = 0;
            }
        }
        else
        {
            time += Time.deltaTime;
            float x0 = 0;
            float x1 = point.x;
            float distance;
            if (x1 != x0)
            {
                distance = x1 - x0;
            }
            else
            {
                return;
            }
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(0, point.y, (nextX - x0) / distance);
            float arc = (nextX - x0) * (nextX - x1) / (-0.2f * distance * distance);
            Vector3 nextPosition = new Vector3(nextX, baseY + arc, 0);
            transform.position = nextPosition;
            if ((point - transform.position).sqrMagnitude <= 0.01f)
            {
                if (IkuManager.instance.ikuminBoom)
                {
                    gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().StatInput(10, normalspeed, knockBack, transform.localScale.x/0.7f);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Animator>().enabled = false;
                    gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                    gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().gameObject.SetActive(true);
                }
                else
                {
                    returnIkumin = true;
                    speed = 10f;
                    movement = Movement.Move;
                }
            }
        }
        Status();
    }

    private void PositionStatus()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void AttackDirection(Vector3 vector3)
    {
        direction = vector3 - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<Monster>() != null || collision.GetComponent<BigMonster>() != null) && IkuManager.instance.ikuminBoom == false)
        {
            AttackDirection(PlayerManager.instance.transform.position);
            speed = 10f;
            returnIkumin = true;
            movement = Movement.Move;
        }
        else if((collision.GetComponent<Monster>() != null || collision.GetComponent<BigMonster>() != null) && IkuManager.instance.ikuminBoom)
        {
            point = collision.gameObject.transform.position;
            gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().StatInput(10, normalspeed, knockBack, transform.localScale.x/0.7f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.GetChild(0).GetComponent<IkuminBoom>().gameObject.SetActive(true);
        }
        if (collision.GetComponent<PlayerManager>() != null && returnIkumin)
        {
            gameObject.SetActive(false);
            GameManager.instance.ikuminCount++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null && returnIkumin)
        {
            gameObject.SetActive(false);
        }
    }

    private void Status()
    {
        switch (movement)
        {
            case Movement.Attack:
                animator.Play("Shoot");
                break;
            case Movement.Move:
                animator.Play("run");
                break;
        }
    }
}
