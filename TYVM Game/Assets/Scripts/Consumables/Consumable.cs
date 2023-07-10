using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    //add more fields and methods as necessary 
    [SerializeField]
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    public Collider2D c2d;
    public abstract void Consume();   
}
