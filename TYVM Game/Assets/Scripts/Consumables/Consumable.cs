using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    //add more fields and methods as necessary 
    protected bool isUsed;
    protected GameObject playerTank;
    public abstract void Consume();   
}
