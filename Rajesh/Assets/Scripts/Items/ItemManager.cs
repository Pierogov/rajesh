using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    List<GameObject> items;
    GameObject nearest;
    Transform player;
    WeaponManager weaponManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
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
        nearest = items[0];

        foreach(GameObject item in items)
        {
            if(Vector2.Distance(player.position, item.transform.position) < Vector2.Distance(player.position, nearest.transform.position))
            {
                nearest = item;
            }
        }
    }
}
