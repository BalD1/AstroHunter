﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemies
{

    [SerializeField] private float lifeTime;

    protected override void Start()
    {
        base.Start();
        SetDirection();
    }

    protected override void Update()
    {
        base.Update();
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Death();
    }

    private void SetDirection()
    {
        Vector2 playerPos = GameManager.Instance.getPlayerRef().transform.position;
        Vector2 selfPos = this.gameObject.transform.position;

        direction.x = selfPos.x > playerPos.x ? -1 : 1;
        direction.y = selfPos.y > playerPos.y ? -1 : 1;
    }


}
