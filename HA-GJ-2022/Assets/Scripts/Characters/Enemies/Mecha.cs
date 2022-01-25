using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : Enemies
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            base.Update();
        }
    }
}
