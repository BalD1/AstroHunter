using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    [SerializeField] private float waitBeforeMovements_CD = 1;
    private float waitBeforeMovements_TIMER;

    private GameObject playerRef;
    private Vector2 direction;

    private void Start()
    {
        CallStart();
        playerRef = GameManager.Instance.getPlayerRef();
    }

    private void Update()
    {
        if (waitBeforeMovements_TIMER > 0)
            waitBeforeMovements_TIMER -= Time.deltaTime;

        CallUpdate();
        direction = playerRef.transform.position - this.transform.position;
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        if (CanMove())
            Translate(direction);
        else
            this.body.velocity *= 0.97f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && waitBeforeMovements_TIMER <= 0)
        {
            waitBeforeMovements_TIMER = waitBeforeMovements_CD;
            Player p = collision.GetComponentInParent<Player>();
            p.TakeDamages(characterStats.damages);
        }
    }

    public override void TakeDamages(int amount)
    {
        base.TakeDamages(amount);
        GameManager.Instance.EnemiesInWave--;
    }

    protected override void Death()
    {
        Destroy(this.gameObject);
    }

    public bool CanMove() { return waitBeforeMovements_TIMER <= 0; }
}
