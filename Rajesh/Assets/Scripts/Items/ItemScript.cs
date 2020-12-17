using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Weapon weapon;
    public MeleeWeapon melee;

    ItemManager itemManager;

    private void Start()
    {
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        itemManager.AddToItemsList(gameObject);
        if (weapon) 
        {
            GetComponent<SpriteRenderer>().sprite = weapon.fullImg;
        }
        else if (melee)
        {
            GetComponent<SpriteRenderer>().sprite = melee.img;
        }
    }

    private void Update()
    {
        if(itemManager.nearest == gameObject)
        {
            if (weapon)
            {
                GetComponent<SpriteRenderer>().sprite = weapon.fullImg;
            }
            else if (melee)
            {
                GetComponent<SpriteRenderer>().sprite = melee.img;
            }
        }
        else
        {
            if (weapon)
            {
                GetComponent<SpriteRenderer>().sprite = weapon.emptyIMG;
            }
            else if (melee)
            {
                GetComponent<SpriteRenderer>().sprite = melee.emptyIMG;
            }
        }
    }
}
