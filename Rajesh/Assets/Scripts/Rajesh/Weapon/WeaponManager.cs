using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //obecnie używana broń
    public Weapon activeWeapon;
    int currentAmmo;

    float timeToShoot;
    public float startTimeToShoot;

    //zmienne
    public float offset;

    //komponenty
    Animator animator;

    //obiekty
    public GameObject rotator;
    public Transform gunPoint;

    //dwie ręce
    public SpriteRenderer upww, up, lowww, low, weaponSprite;

    //prefaby
    public GameObject shootPrefab;

    //skrypty
    CameraShake cameraShake;

    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        currentAmmo = activeWeapon.ammoMax;
    }

    void Update()
    {
        //sprawdzanie czy ma broń
        if (!activeWeapon)
        {
            animator.SetBool("isWeaponActive", false);

            //ustawianie kolorów
            lowww.color = new Vector4(lowww.color.r, lowww.color.g, lowww.color.b, 0f);
            upww.color = new Vector4(upww.color.r, upww.color.g, lowww.color.b, 0f);
            weaponSprite.color = new Vector4(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 0f);
            low.color = new Vector4(low.color.r, low.color.g, low.color.b, 1f);
            up.color = new Vector4(up.color.r, up.color.g, low.color.b, 1f);
        }
        else
        {
            animator.SetBool("isWeaponActive", true);

            //ustawianie kolorów
            lowww.color = new Vector4(lowww.color.r, lowww.color.g, lowww.color.b, 1f);
            upww.color = new Vector4(upww.color.r, upww.color.g, lowww.color.b, 1f);
            weaponSprite.color = new Vector4(weaponSprite.color.r, weaponSprite.color.g, weaponSprite.color.b, 1f);
            low.color = new Vector4(low.color.r, low.color.g, low.color.b, 0f);
            up.color = new Vector4(up.color.r, up.color.g, low.color.b, 0f);

            //obrót

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            //ustawianie obrazka aktywnej broni w zależności od pozostałej amunicji
            if(currentAmmo >= 0.5 * activeWeapon.ammoMax)
            {
                weaponSprite.sprite = activeWeapon.fullImg;
            }
            else if(currentAmmo >= 0.25 * activeWeapon.ammoMax && currentAmmo < 0.5 * activeWeapon.ammoMax)
            {
                weaponSprite.sprite = activeWeapon.halfImg;
            }
            else
            {
                weaponSprite.sprite = activeWeapon.lowIMG;
            }

            //strzal
            if (!activeWeapon.automatic)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(currentAmmo>0)
                    {
                        if (timeToShoot <= 0)
                        {
                            Instantiate(shootPrefab, gunPoint.position, Quaternion.Euler(0f, 0f, transform.eulerAngles.z + transform.eulerAngles.z * Random.Range(-activeWeapon.recoil, activeWeapon.recoil)));
                            cameraShake.ShakeX();
                            currentAmmo -= 1;
                            timeToShoot = startTimeToShoot * activeWeapon.fireRate;
                        }
                        else
                        {
                            timeToShoot -= Time.deltaTime;
                        } 
                    }
                }
            }
            else 
            { 
                if (Input.GetMouseButton(0))
                {
                    if (currentAmmo > 0)
                    {
                        if (timeToShoot <= 0)
                        {
                            Instantiate(shootPrefab, gunPoint.position, Quaternion.Euler(0f, 0f, transform.eulerAngles.z + transform.eulerAngles.z * Random.Range(-activeWeapon.recoil, activeWeapon.recoil)));
                            cameraShake.ShakeX();
                            currentAmmo -= 1;
                            timeToShoot = startTimeToShoot * activeWeapon.fireRate;
                        }
                        else
                        {
                            timeToShoot -= Time.deltaTime;
                        }
                    }
                }
            }
            timeToShoot -= Time.deltaTime;
        }
    }
}
