﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private float fire_CD;
    [SerializeField] private float projectilesSpeed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Characters owner;
    private float fire_TIMER;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (fire_TIMER > 0)
            fire_TIMER -= Time.deltaTime;

        if (Input.GetMouseButton(0))
            Fire();
    }

    private void Fire()
    {
        if (fire_TIMER > 0)
            return;

        GameObject laser = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.position, firePoint.rotation);
        laser.GetComponent<Lasers>().Set(projectilesSpeed, owner.GetStats().damages);
        fire_TIMER = fire_CD;
    }
}