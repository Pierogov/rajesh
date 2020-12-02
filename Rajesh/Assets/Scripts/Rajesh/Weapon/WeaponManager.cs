using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //obecnie używana broń
    public Weapon activeWeapon;

    //zmienne
    public float offset;

    //komponenty
    Animator animator;

    //obiekty
    public GameObject rotator;

    //dwie ręce
    public SpriteRenderer upww, up, lowww, low;

    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
            low.color = new Vector4(low.color.r, low.color.g, low.color.b, 1f);
            up.color = new Vector4(up.color.r, up.color.g, low.color.b, 1f);
        }
        else
        {
            animator.SetBool("isWeaponActive", true);

            //ustawianie kolorów
            lowww.color = new Vector4(lowww.color.r, lowww.color.g, lowww.color.b, 1f);
            upww.color = new Vector4(upww.color.r, upww.color.g, lowww.color.b, 1f);
            low.color = new Vector4(low.color.r, low.color.g, low.color.b, 0f);
            up.color = new Vector4(up.color.r, up.color.g, low.color.b, 0f);

            //obrót

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            /*
            var addAngle = 90;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + addAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            */
        }
    }
}
