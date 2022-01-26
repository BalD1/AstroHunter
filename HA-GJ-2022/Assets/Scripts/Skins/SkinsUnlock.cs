using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsUnlock : MonoBehaviour
{
    protected bool conditionIsMet = false;

    public virtual void setCondition()
    {

    }

    public bool canUnlockSkin() { return this.conditionIsMet; }
}