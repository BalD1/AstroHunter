using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSign : MonoBehaviour
{
    private Camera signsCamera;
    [SerializeField] private float lifeTime = 2;

    private void Start()
    {
        signsCamera = GameManager.Instance.getSignsCamera();

        Vector3 pos = signsCamera.WorldToViewportPoint(this.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        this.transform.position = signsCamera.ViewportToWorldPoint(pos);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(this.gameObject);
    }
}
