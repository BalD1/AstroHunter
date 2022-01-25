using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private float fire_CD;
    [SerializeField] private float projectilesSpeed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Characters owner;
    [SerializeField] private ParticleSystem shootParticles;

    [SerializeField] private Vector2 rightPosition;
    [SerializeField] private Vector2 leftPosition;

    private float fire_TIMER;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (fire_TIMER > 0)
            fire_TIMER -= Time.deltaTime;

        if (Input.GetMouseButton(0) && GameManager.Instance.GameState == GameManager.GameStates.InGame)
            Fire();
    }

    private void Fire()
    {
        if (fire_TIMER > 0)
            return;

        shootParticles.Play();
        GameObject laser = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.position, firePoint.rotation);
        laser.GetComponent<Lasers>().Set(projectilesSpeed, owner.GetStats().damages);
        fire_TIMER = fire_CD;
    }

    public void Flip(bool right)
    {
        firePoint.transform.localPosition = right ? rightPosition : leftPosition;
    }
}
