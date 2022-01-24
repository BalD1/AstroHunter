using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public static bool IsVectorInBetween(Vector2 v, float clamp)
    {
        if (v.x > -clamp && v.x < clamp &&
            v.y > -clamp && v.y < clamp)
            return true;
        return false;
    }
}
