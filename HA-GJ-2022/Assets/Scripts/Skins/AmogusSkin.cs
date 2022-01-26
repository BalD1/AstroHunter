using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmogusSkin : SkinsUnlock
{
    public override void setCondition()
    {
        conditionIsMet = GameManager.Instance.killsCount >= 25;
    }
}
