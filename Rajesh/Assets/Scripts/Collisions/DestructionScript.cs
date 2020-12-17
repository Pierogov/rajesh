using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour
{
    public GameObject destroyed;
    public string[] destructionTags;
    public GameObject destructor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        destructor = collision.gameObject;
        if(CheckIfTagInDestroyed(collision))
        {
            DestructionActive(); 
        }
    }

    public void DestructionActive()
    {
        //niszczenie
        destroyed.SetActive(true);
        gameObject.SetActive(false);
    }

    bool CheckIfTagInDestroyed(Collider2D coll)
    {
        //sprawdzenie czy obiekt może niszczyć
        foreach(string tag in destructionTags)
        {
            if(tag == coll.tag)
            {
                return true;
            }
        }
        return false;
    }
}
