using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float distance;
    public LayerMask whatIsHitable;
    float speed;
    public bool isPlayer;

    WeaponManager weaponManager; 
    Weapon activeWeapon;
    private void Start()
    {
        //deklarowanie wapon managera zawierajacego aktywną broń
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        
        if (weaponManager.activeWeapon)
        { 
            //uzupełnianie parametrów z broni
            activeWeapon = weaponManager.activeWeapon;
            Invoke("DestroyBullet", activeWeapon.lifeTime);
            speed = activeWeapon.bulletSpeed;
        }
    }

    void Update()
    {
        //wykrywanie kolizji
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsHitable);
        Debug.DrawLine(transform.position, transform.up * distance);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enamy"))
            {
                //deal dmg
            }
            if (hitInfo.collider.CompareTag("Destruction") && activeWeapon.canDestroy)
            {
                hitInfo.collider.GetComponent<DestructionScript>().destructor = gameObject;
                hitInfo.collider.GetComponent<DestructionScript>().DestructionActive();
                Destroy(gameObject);
                //Debug.Log("ej no byq");
            }
            Destroy(gameObject);
        }
        //ruch
        transform.Translate(Vector2.up * -speed * Time.deltaTime);   
    }

    void DestroyBullet()
    {
        //niszczenie obiektu
        Destroy(gameObject);
    }
}
