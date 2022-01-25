using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] private float timer = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(this.gameObject);
    }
}
