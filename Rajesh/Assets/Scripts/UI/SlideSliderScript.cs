using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSliderScript : MonoBehaviour
{
    //deklaracja odwołań do skryptów
    PlayerMovement playerMovement;
    
    //deklaracja komponentów
    public Slider slider1, slider2;
    void Start()
    {
        //przypisanie skryptów
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //określenie granicznych wartości
        slider1.minValue = 0f;
        slider2.minValue = 0f;

        slider1.maxValue = playerMovement.startSlideTime;
        slider2.maxValue = playerMovement.startSlideTime;

        //aktualizacja wartości sliderów do czasu slide
        if (playerMovement.slideTime >= 0) { slider1.value = playerMovement.slideTime; }
        else { slider1.value = 0; }

        if (playerMovement.slideTime >= 0) { slider2.value = playerMovement.slideTime; }
        else { slider2.value = 0; }
    }
}
