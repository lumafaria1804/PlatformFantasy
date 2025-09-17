using System.Threading;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;

    private Animator anim;
    private bool isHitting;
    private float timeCount;
    private float recoveryTime = 1f;

    private void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        OnMove();
        OnRun();

        if (isHitting)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0;
            }
        }
    }

    void OnMove()
    {
        if (player.IsClimbing)
        {
            anim.SetInteger("transition", 3);
            Debug.Log("Climbing");
        }
        else if (player.IsJumping)
        {
            anim.SetInteger("transition", 2);
            Debug.Log("Jumping");
        }
        else if (player.IsAttacking)
        {
            anim.SetInteger("isAttacking", 4);
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.transform.position, radius, enemyLayer);

            if (hit != null)
            {
                hit.GetComponentInChildren<AnimControl>().OnHit();
            }
        }

        else if (player.IsMovement)
        {
            anim.SetInteger("transition", 1);
            Debug.Log("Moving");
        }
        else
        {
            anim.SetInteger("transition", 0);
            Debug.Log("Idle");

        }
    }
    void OnRun()
    {
        if (player.IsRunning)
        {
            anim.SetInteger("transition", 4);
        }
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void Attack()
    {
        if (player.IsAttacking)
        {
            anim.SetTrigger("isAttacking");

            Collider2D hit = Physics2D.OverlapCircle(attackPoint.transform.position, radius, enemyLayer);

            if (hit != null)
            {
                hit.GetComponentInChildren<AnimControl>().OnHit();
            }
        }
    }
    public void OnHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}
