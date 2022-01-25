using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private ParticleSystem hitParticles;
    private float speed;
    private int damages;

    private void Start()
    {
        GameManager.Instance.ev_ReloadEvent.AddListener(LaserReload);
    }

    public void Set(float _speed, int _damages)
    {
        speed = _speed;
        damages = _damages;
    }

    private void Update()
    {
        Movements();
    }

    private void Movements()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemies e = collision.GetComponent<Enemies>();
            e.TakeDamages(damages);
            this.gameObject.SetActive(false);

            hitParticles.gameObject.SetActive(true);
            hitParticles.transform.parent = null;
            hitParticles.transform.position = this.transform.position;
            hitParticles.transform.localScale = Vector3.one;
            hitParticles.Play();
            hitParticles.GetComponent<DelayedDisable>().enabled = true;
        }
    }

    private void OnEnable()
    {
        hitParticles.transform.parent = this.transform;
    }

    private void OnDisable()
    {
        trail.Clear();
    }

    private void LaserReload()
    {
        this.gameObject.SetActive(false);
    }
}
