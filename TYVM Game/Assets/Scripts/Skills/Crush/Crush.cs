using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crush", menuName = "Skills/Crush", order = 2)]

public class Crush : Skill
{
    [SerializeField]
    private GameObject CrushCirclePrefab;
    public override void Activate(GameObject player) {
        Transform playerTransform = player.transform.Find("Hull");
        GameObject ccPrefab = Instantiate(CrushCirclePrefab, playerTransform.position, playerTransform.rotation);
        Destroy(ccPrefab, activeTime);
    }

}
