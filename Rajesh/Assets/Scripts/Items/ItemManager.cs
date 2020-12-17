using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject nearest;
    Transform player;
    WeaponManager weaponManager;
    public float pickUpRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
    }

    private void LateUpdate()
    {
        MarkNearest();
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickItem();
        }
    }

    public void AddToItemsList(GameObject item)
    {
        items.Add(item);
    }

    public void RemoveFromItemsList(GameObject item)
    {
        items.Remove(item);
    }

    void MarkNearest()
    {
        if (items.Count != 0) 
        {
            nearest = items[0];

            foreach (GameObject item in items)
            {
                if (Vector2.Distance(player.position, item.transform.position) < Vector2.Distance(player.position, nearest.transform.position))
                {
                    nearest = item;
                }
            }

            if (Vector2.Distance(player.position, nearest.transform.position) > pickUpRange)
            {
                nearest = null;
            }
        }
    }

    void PickItem()
    {
        MarkNearest();
        if (nearest)
        {
            //wybierz bron do ustawienia
            if (nearest.GetComponent<ItemScript>().weapon)
            {
                weaponManager.activeWeapon = nearest.GetComponent<ItemScript>().weapon;
            }
            else if (nearest.GetComponent<ItemScript>().melee)
            {
                weaponManager.activeMelee = nearest.GetComponent<ItemScript>().melee;
            }
            RemoveFromItemsList(nearest);
            Destroy(nearest);
        }
    }
}
