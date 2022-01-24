using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private float speed;
    private int damages;

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
        }
    }
}
