using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] private float waitBeforeMovements_CD = 1;
    [SerializeField] private bool useInCounter = true;
    private float waitBeforeMovements_TIMER;

    protected GameObject playerRef;
    protected Vector2 direction;

    protected override void Start()
    {
        base.Start();
        playerRef = GameManager.Instance.getPlayerRef();
    }

    protected override void Update()
    {
        if (waitBeforeMovements_TIMER > 0)
            waitBeforeMovements_TIMER -= Time.deltaTime;

        base.Update();
    }
    protected virtual void FixedUpdate()
    {
        Translate(direction);
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
    }

    protected override void Death()
    {
        if (CanUseCounter())
            GameManager.Instance.EnemiesInWave--;

        Destroy(this.gameObject);
    }

    public bool CanMove() { return waitBeforeMovements_TIMER <= 0; }
    public bool CanUseCounter() { return useInCounter; }
}
