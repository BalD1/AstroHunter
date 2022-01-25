using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemies
{
    [SerializeField] private GameObject sign;
    [SerializeField] private float lifeTime;
    [SerializeField] private float timeBeforeLaunch = 1;
    private bool directionIsSet = false;

    protected override void Start()
    {
        base.Start();
        Instantiate(sign, this.transform.position, Quaternion.identity);
    }

    protected override void Update()
    {
        if (timeBeforeLaunch <= 0)
        {
            if (!directionIsSet)
                SetDirection();

            base.Update();
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                Death();
        }
        else
            timeBeforeLaunch -= Time.deltaTime;
    }

    private void SetDirection()
    {
        Vector2 playerPos = GameManager.Instance.getPlayerRef().transform.position;
        Vector2 selfPos = this.gameObject.transform.position;

        direction.x = selfPos.x > playerPos.x ? -1 : 1;
        direction.y = selfPos.y > playerPos.y ? -1 : 1;
        directionIsSet = true;
    }


}
