using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected SCR_characters characterInfosScriptable;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected GameObject hpBar;
    [SerializeField] protected AudioSource source;

    [SerializeField] protected Material hitMaterial;
    [SerializeField] protected float blink_CD = 0.3f;
    protected Material baseMaterial;
    protected float blink_TIMER;
    protected bool blink = false;


    protected SCR_characters.stats characterStats;
    protected float invincibility_TIMER;

    protected virtual void Start()
    {
        baseMaterial = this.sprite.material;
        characterStats = characterInfosScriptable.CharacterStats;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            if (invincibility_TIMER > 0)
                invincibility_TIMER -= Time.deltaTime;

            if (blink_TIMER > 0)
                blink_TIMER -= Time.deltaTime;
            else if (blink && blink_TIMER <= 0)
            {
                blink = false;
                this.sprite.material = baseMaterial;
            }
        }
    }

    public virtual void TakeDamages(int amount)
    {
        if (invincibility_TIMER > 0)
            return;

        characterStats.currentHP -= amount;
        if (characterStats.currentHP <= 0)
            Death();

        invincibility_TIMER = characterStats.invincibleTime;

        this.sprite.material = hitMaterial;
        blink_TIMER = blink_CD;
        blink = true;
    }

    public virtual void Heal(int amount)
    {
        characterStats.currentHP = Mathf.Clamp(characterStats.currentHP + amount, 0, characterStats.maxHP);
    }

    protected virtual void Death()
    {

    }

    protected virtual void Translate(Vector2 direction)
    {
        this.body.velocity = new Vector2(direction.x * characterStats.speed, 
                                         direction.y * characterStats.speed);
    }
    protected void TranslateTo(Transform target)
    {
        Vector2 direction = target.transform.position - this.transform.position;
        direction.Normalize();
        body.MovePosition((Vector2)this.transform.position + (direction * characterStats.speed * Time.deltaTime));
    }
    protected void TranslateTo(Vector2 target)
    {
        Vector2 direction = target - (Vector2)this.transform.position;
        direction.Normalize();
        body.MovePosition((Vector2)this.transform.position + (direction * characterStats.speed * Time.deltaTime));
    }

    protected void AddForce(Vector2 direction)
    {
        this.body.AddForce(direction * characterStats.speed);
    }

    public SCR_characters.stats GetStats()
    {
        return this.characterStats;
    }
}
