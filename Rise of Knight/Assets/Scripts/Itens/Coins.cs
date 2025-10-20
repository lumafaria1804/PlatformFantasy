using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.GetComponent<Player>().CurrentGold++;
            AudioController.instance.PlayAndDestroy(coinSound, transform.position, null);
            Destroy(gameObject);
        }
    }
}
