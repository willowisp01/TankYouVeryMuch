using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour {

    // Add more fields and methods as necessary 
    protected bool isUsed = false;
    protected GameObject playerTank;

    protected abstract void Consume();   
}
