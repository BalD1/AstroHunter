﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector2 offset;

    [SerializeField] private Vector2 clamp;

    private void FixedUpdate()
    {
        Vector2 dest = (Vector2)target.position + offset;
        Vector3 smooth = Vector2.Lerp(this.transform.position, dest, smoothSpeed);
        smooth.x = Mathf.Clamp(smooth.x, -clamp.x, clamp.x);
        smooth.y = Mathf.Clamp(smooth.y, -clamp.y, clamp.y);
        smooth.z = -10;
        this.transform.position = smooth;
    }
}
