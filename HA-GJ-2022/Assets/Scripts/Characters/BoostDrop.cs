using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostDrop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float boost_DURATION;
    private Player playerRef;
    private float boost_TIMER;
    private bool pickedUp;

    private void Start()
    {
        playerRef = GameManager.Instance.getPlayerRef().GetComponent<Player>();
        GameManager.Instance.ev_ReloadEvent.AddListener(OnReload);
    }

    private void Update()
    {
        if (boost_TIMER > 0)
            boost_TIMER -= Time.deltaTime;
        else if (boost_TIMER <= 0 && pickedUp)
        {
            playerRef.ApplyBoost(false);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickedUp)
        {
            playerRef = collision.GetComponentInParent<Player>();
            this.sprite.enabled = false;
            boost_TIMER = boost_DURATION;
            pickedUp = true;
            playerRef.ApplyBoost(true);
        }
    }

    private void OnReload()
    {
        Destroy(this.gameObject);
    }
}
