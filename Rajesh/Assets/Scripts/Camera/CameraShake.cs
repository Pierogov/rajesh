using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Animator anim;
    public Animation shakeX;
    WeaponManager weaponManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
    }

    public void ShakeX()
    {
        if (weaponManager.activeWeapon) 
        {
            anim.speed = 1 / weaponManager.activeWeapon.fireRate;
            anim.SetTrigger("ShakeX");
        }
        else
        {
            anim.speed = 1;
        }
    }

    public void ResetSpeed()
    {
        anim.speed = 1;
    }
}
