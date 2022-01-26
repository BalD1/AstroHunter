using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeSkin : SkinsUnlock
{
    public override void setCondition()
    {
        conditionIsMet = GameManager.Instance.wonGameOnceNoHit;
    }
}
