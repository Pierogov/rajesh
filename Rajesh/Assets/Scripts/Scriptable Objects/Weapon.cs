using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    //deklaracja parametrów
    public float fireRate;
    public int ammoMax;

    public Sprite img;
}
