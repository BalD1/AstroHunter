using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemies
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            base.Update();

            direction = playerRef.transform.position - this.transform.position;
            direction.Normalize();
        }
    }

    protected override void Death()
    {
        base.Death();

        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            GameManager.Instance.killsCount++;
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
}
