using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TankAppearance", menuName = "Appearance/TankAppearance", order = 0)]
public class TankAppearance : ScriptableObject {
    // These fields will be used to change the tank appearance on the fly
    public Sprite hull;
    public Sprite track;
    public Sprite tower;
    public Sprite gun;
    public Sprite gunConnector;

    /* We can continue adding any new appearance parts here (e.g. accessories). Remember to update the main prefab and
     * AppearanceSelect.cs when doing so. We can also consider making separate PlayerAppearance and EnemyAppearance assets
     * if we want to have player/enemy-specific cosmetics, but for now both tanks share pretty much the same parts.
     */
}
