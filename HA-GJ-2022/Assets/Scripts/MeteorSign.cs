using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSign : MonoBehaviour
{
    [HideInInspector] public Vector2 meteorPosition;

    void Update()
    {
        this.transform.position = meteorPosition;

        Vector3 pos = GameManager.Instance.getMainCamera().WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
