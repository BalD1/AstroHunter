using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : Enemies
{
    [SerializeField] private Animator animator;
    [SerializeField] private float minDistanceForDash;
    [SerializeField] private float dash_DURATION;
    [SerializeField] private float attack_CD;
    [SerializeField] private float dashSpeed;
    private float baseSpeed;

    private bool isDashing = false;
    private bool dashFlag = false;
    private float dash_TIMER;
    private float attack_TIMER;

    protected override void Start()
    {
        base.Start();
        baseSpeed = this.characterStats.speed;
        attack_TIMER = 0;
    }

    protected override void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            if (dash_TIMER > 0)
                dash_TIMER -= Time.deltaTime;
            else if (dash_TIMER <= 0 && isDashing)
                StopDash();

            if (attack_TIMER <= 0 && !dashFlag)
                isDashing = Vector2.Distance(this.transform.position, playerRef.transform.position) < minDistanceForDash;

            if (!isDashing)
            {
                direction = playerRef.transform.position - this.transform.position;
                direction.Normalize();
            }
            else if (isDashing && !dashFlag)
                Dash();


            if (attack_TIMER > 0)
                attack_TIMER -= Time.deltaTime;


            base.Update();
        }
    }

    protected override void Translate(Vector2 direction)
    {
        if (attack_TIMER <= 0)
        {
            if (CanMove())
                base.Translate(direction);
            else
                this.body.velocity *= 0.97f;
        }

        if (!isDashing)
        {

            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

            sprite.flipY = !(angle > -90f && angle < 90);

            body.rotation = angle;
        }
    }

    private void Dash()
    {
        dashFlag = true;
        animator.SetTrigger("dash");
        dash_TIMER = dash_DURATION;
        this.characterStats.speed = dashSpeed;
    }

    protected override void Death()
    {
        base.Death();

        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            GameManager.Instance.killsCount++;
    }

    private void StopDash()
    {
        dashFlag = false;
        isDashing = false;
        this.characterStats.speed = baseSpeed;
        this.body.velocity = Vector2.zero;
        attack_TIMER = attack_CD;
    }
}
