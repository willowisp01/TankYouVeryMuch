using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crush", menuName = "Skills/Crush", order = 2)]

public class Crush : Skill
{
    public override void Activate(GameObject player) {
        Debug.Log("crush");
    }
}
