using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    Vignette m_Vignette;
    ColorGrading m_ColorGrading;
    PostProcessVolume m_Volume;
    PostProcessVolume m_Volume2;

    public float maxVig;
    public float maxCG;
    public float startPercent;
    public float val1, val2;
    public float start1, start2;
    public float timer;


    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerMovement>();
        
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        m_Volume2 = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_ColorGrading);
    }

    
    void Update()
    {
        AdjustToSlide();
    }

    void AdjustToSlide()
    {
        //wineta i color grading na slide
        timer = playerMovement.slideTime;
        if(timer < 0)
        {
            timer = 0;
        }
        //obliczanie winety
        if (timer / playerMovement.startSlideTime <= startPercent / 100)
        {
            val1 = (1 - timer / playerMovement.startSlideTime) * maxVig;
            m_Vignette.intensity.Override(val1);
        }
        else
        {
            val1 = 0f;
            m_Vignette.intensity.Override(val1);
        }


        //obliczanie color gradingu
        if (timer/playerMovement.startSlideTime <= startPercent/100)
        {
            val2 = -((1 - timer / playerMovement.startSlideTime) * maxCG);
            m_ColorGrading.saturation.Override(val2);
        }
        else
        {
            val2 = 0f;
            m_ColorGrading.saturation.Override(val2);
        }
    }
}
