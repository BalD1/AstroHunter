using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] private float waitBeforeMovements_CD = 1;
    [SerializeField] private bool useInCounter = true;
    [SerializeField] private Material hitMaterial;
    private Material baseMaterial;
    private float waitBeforeMovements_TIMER;

    [SerializeField] private float blink_CD = 0.3f;
    private float blink_TIMER;
    private bool blink = false;

    protected GameObject playerRef;
    protected Vector2 direction;

    protected override void Start()
    {
        base.Start();
        baseMaterial = this.sprite.material;
        playerRef = GameManager.Instance.getPlayerRef();
        if (CanUseCounter())
            GameManager.Instance.EnemiesInWave++;

        GameManager.Instance.ev_ReloadEvent.AddListener(EnemyReload);
    }

    protected override void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {

            if (waitBeforeMovements_TIMER > 0)
                waitBeforeMovements_TIMER -= Time.deltaTime;

            if (blink_TIMER > 0)
                blink_TIMER -= Time.deltaTime;
            else if (blink && blink_TIMER <= 0)
            {
                blink = false;
                this.sprite.material = baseMaterial;
            }

            base.Update();
        }
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
            if (p != null)
                p.TakeDamages(characterStats.damages);
        }
    }

    public override void TakeDamages(int amount)
    {
        source.PlayOneShot(AudioManager.Instance.GetAudioClip(AudioManager.ClipsTags.enemyHurt));
        base.TakeDamages(amount);
        this.sprite.material = hitMaterial;
        blink_TIMER = blink_CD;
        blink = true;
    }

    protected override void Death()
    {
        if (CanUseCounter())
            GameManager.Instance.EnemiesInWave--;

        source.transform.parent = null;
        source.GetComponent<DelayedDestroy>().enabled = true;
        source.PlayOneShot(AudioManager.Instance.GetAudioClip(AudioManager.ClipsTags.enemyDeath));
        Destroy(this.gameObject);
    }

    public bool CanMove() { return waitBeforeMovements_TIMER <= 0; }
    public bool CanUseCounter() { return useInCounter; }

    public void EnemyReload()
    {
        Death();
    }
}
