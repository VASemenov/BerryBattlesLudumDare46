using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponObject : ScriptableObject
{
    public Sprite weaponSprite;
    public int damage = 1;
    public float reach = 0.2f;
    public bool isRanged = false;
    public Arrow projectile;
}
