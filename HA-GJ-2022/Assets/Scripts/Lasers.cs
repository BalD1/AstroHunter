using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        
    }

    private void Update()
    {
        Movements();
    }

    private void Movements()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
