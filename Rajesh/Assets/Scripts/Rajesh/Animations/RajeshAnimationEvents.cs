using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajeshAnimationEvents : MonoBehaviour
{
    //definiujemy komponenty
    Animator animator;

    //definiujemy powiązania skryptów
    PlayerMovement playerMovement;

    private void Start()
    {
        //przypisujemy wartości komponentów
        animator = GetComponent<Animator>();

        //przypisujemy skrypty
        playerMovement = GetComponent<PlayerMovement>();
    }

    //triger do skoku
    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    //do animacji wstawania
    public void Stand()
    {
        playerMovement.jumped = false;
        playerMovement.boostTime = playerMovement.startBoostTime;
    }
}
