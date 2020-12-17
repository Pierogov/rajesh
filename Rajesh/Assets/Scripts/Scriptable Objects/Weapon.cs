using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    //deklaracja parametrów
    //% w ułamku dziesiętnym np. 0.5 = 50%
    public float fireRate;

    public int ammoMax;
    //% w ułamku dziesiętnym np. 0.5 = 50%
    public float recoil;

    public float bulletSpeed;

    public float damage;

    public float lifeTime;

    public int shakeLevel;

    public bool automatic;
   
    public AudioClip shootSound;

    public Vector3 gunPointOffeset;

    public bool canDestroy;

    //bez obramówki
    public Sprite emptyIMG;
    //powyżej połowy magazynka
    public Sprite fullImg;
    //poniżej połowy
    public Sprite halfImg;
    //poniżej 1/4
    public Sprite lowIMG;
}
