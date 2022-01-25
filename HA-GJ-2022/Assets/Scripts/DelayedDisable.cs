using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDisable : MonoBehaviour
{
    [SerializeField] private float disable_CD = 1;
    private float disable_TIMER;

    private void OnEnable()
    {
        disable_TIMER = disable_CD;
    }

    private void Update()
    {
        disable_TIMER -= Time.deltaTime;
        if (disable_TIMER <= 0)
            this.gameObject.SetActive(false);
    }
}
