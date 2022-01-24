using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    [SerializeField] private SpriteRenderer sprite;
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

    protected override void Translate(Vector2 direction)
    {
        if (CanMove())
            base.Translate(direction);
        else
            this.body.velocity *= 0.97f;

        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        sprite.flipY = !(angle > -90f && angle < 90);

        body.rotation = angle;
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
