using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Characters
{
    [SerializeField] private float waitBeforeMovements_CD = 1;
    [SerializeField] private bool useInCounter = true;
    private float waitBeforeMovements_TIMER;

    protected GameObject playerRef;
    protected Vector2 direction;

    protected override void Start()
    {
        base.Start();
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

            base.Update();
        }
    }
    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            Translate(direction);
        else if (GameManager.Instance.GameState == GameManager.GameStates.GameOver)
            this.body.velocity = Vector2.zero;
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
