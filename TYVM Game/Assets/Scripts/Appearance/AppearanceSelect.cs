using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AppearanceSelect : MonoBehaviour {

    [SerializeField]
    private TankAppearance appearance; // A ScriptableObject containing the sprites

    private void Awake() {
        Change();
    }

    private void Change() {
        transform.Find("Hull").GetComponent<SpriteRenderer>().sprite = appearance.hull;
        transform.Find("Hull/LeftTrack").GetComponent<SpriteRenderer>().sprite = appearance.track;
        transform.Find("Hull/RightTrack").GetComponent<SpriteRenderer>().sprite = appearance.track;
        transform.Find("Tower").GetComponent<SpriteRenderer>().sprite = appearance.tower;
        transform.Find("Tower/Gun").GetComponent<SpriteRenderer>().sprite = appearance.gun;
        transform.Find("Tower/GunConnector").GetComponent<SpriteRenderer>().sprite = appearance.gunConnector;
    }

    public void Change(TankAppearance appearance) {
        this.appearance = appearance;
        Change();
    }
}
