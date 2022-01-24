using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    private GameObject playerRef;
    private Vector2 direction;

    private void Start()
    {
        CallStart();
        playerRef = GameManager.Instance.getPlayerRef();
    }

    private void Update()
    {
        CallUpdate();
        direction = playerRef.transform.position - this.transform.position;
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        Translate(direction);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponentInParent<Player>();
            p.TakeDamages(characterStats.damages);
        }
    }

    public override void TakeDamages(int amount)
    {
        base.TakeDamages(amount);
        if (characterStats.currentHP <= 0)
            Destroy(this.gameObject);
    }
}
