using System.Collections;
using UnityEngine;

public class CheckPos : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) 
        {
            player.StartCoroutine(player.DeathSequence());
        }
    }
}
