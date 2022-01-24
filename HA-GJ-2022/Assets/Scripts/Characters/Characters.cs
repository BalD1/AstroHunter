using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] protected SCR_characters characterInfos;
    [SerializeField] protected Rigidbody2D body;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    protected virtual void Movements()
    {

    }

    protected void Translate(Vector2 direction)
    {
        this.body.velocity = new Vector2(direction.x * characterInfos.CharacterStats.speed, 
                                         direction.y * characterInfos.CharacterStats.speed);
    }
}
