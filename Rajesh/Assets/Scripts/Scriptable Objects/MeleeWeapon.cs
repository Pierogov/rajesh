using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Melee", menuName ="Melee")]

public class MeleeWeapon : ScriptableObject
{
    public float range;
    public float damage;
    public float attackCooldown;
    public int usages;

    public Sprite img;
}
