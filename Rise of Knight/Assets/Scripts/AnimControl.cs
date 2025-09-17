using UnityEngine;

public class AnimControl : MonoBehaviour
{
    private PlayerAnim playerAnim;
    private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

        if (hit != null)
        {
            playerAnim.OnHit();
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("hurt");
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
