using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntoineSkin : SkinsUnlock
{
    public override void setCondition()
    {
        conditionIsMet = GameManager.Instance.deathsCount >= 5;
    }
}
