using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantaSkin : SkinsUnlock
{
    public override void setCondition()
    {
        conditionIsMet = GameManager.Instance.wonGameOnce;
    }
}
