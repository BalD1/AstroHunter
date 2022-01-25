using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] protected SCR_characters characterInfosScriptable;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected GameObject hpBar;

    protected SCR_characters.stats characterStats;
    protected float invincibility_TIMER;

    protected virtual void Start()
    {
        characterStats = characterInfosScriptable.CharacterStats;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            if (invincibility_TIMER > 0)
                invincibility_TIMER -= Time.deltaTime;
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
