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

    [SerializeField] private Gradient effectColor;
    private Color currentColor;

    [SerializeField] private ParticleSystem upgradeParticles;
    [SerializeField] private AnimationCurve speedByWave;

    [SerializeField] private Vector2 rightPosition;
    [SerializeField] private Vector2 leftPosition;

    [SerializeField] private AudioSource source;

    private float fire_TIMER;

    private void Start()
    {
        currentColor = effectColor.Evaluate(0);
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

        source.PlayOneShot(AudioManager.Instance.GetAudioClip(AudioManager.ClipsTags.Laser));
        shootParticles.Play();
        GameObject laser = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.position, firePoint.rotation);
        laser.GetComponent<Lasers>().Set(projectilesSpeed, owner.GetStats().damages, currentColor);
        fire_TIMER = fire_CD;
    }

    public void Flip(bool right)
    {
        firePoint.transform.localPosition = right ? rightPosition : leftPosition;
    }

    public void UpgradeWeapon(int wave)
    {
        float spd = speedByWave.Evaluate(wave);
        if (spd < fire_CD)
        {
            currentColor = effectColor.Evaluate((float)wave / (float)GameManager.Instance.maxWave);
            ParticleSystem.MainModule ma = shootParticles.main;
            ma.startColor = currentColor;
            upgradeParticles.Play();
            fire_CD = spd;
        }
    }
}
