using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Animator anim;
    WeaponManager weaponManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
    }

    //trzęsienie kamerą
    public void Shake(int nmb)
    {
        if (weaponManager.activeWeapon)
        {
            anim.speed = 1 / weaponManager.activeWeapon.fireRate;
        }
        else
        {
            anim.speed = 1;
        }
        switch (nmb){
            case 0:
                anim.SetTrigger("Shake");
                break;
            case 1:
                anim.SetTrigger("Shake1");
                break;
            default:
                anim.SetTrigger("Shake");
                break;
        }
    }

    //do cofania prędkości animacji do normalnej
    public void ResetSpeed()
    {
        anim.speed = 1;
    }
}
