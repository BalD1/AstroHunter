using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObject/ Character Configuration")]
public class SCR_characters : ScriptableObject
{
    [System.Serializable]
    public struct stats
    {
        public string name;
        public int maxHP;
        public int currentHP;
        public float speed;
        public int damages;
        public float invincibleTime;
    }
    public stats CharacterStats;
}