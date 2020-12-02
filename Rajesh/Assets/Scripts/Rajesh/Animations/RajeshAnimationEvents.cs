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
    public void TakeOF()
    {
        playerMovement.takingof = !playerMovement.takingof;
        playerMovement.startJump = true;
        playerMovement.engagingJump = true;

    }

    public void Land()
    {
        playerMovement.landingAnim = !playerMovement.landingAnim;
    }

    public void BlockSpeed()
    {
        playerMovement.blockSpeed = !playerMovement.blockSpeed;
    }
    public void EndAnim()
    {
        playerMovement.startJump = false;
        //playerMovement.boostTime = playerMovement.startBoostTime;
    }
    public void EngageJump()
    {
        playerMovement.engagingJump = false;
    }
    public void SmoothEnd()
    {
        playerMovement.smoothEnd = false;
    }
    public void SmoothStart()
    {
        playerMovement.smoothEnd = true;
    }
}
