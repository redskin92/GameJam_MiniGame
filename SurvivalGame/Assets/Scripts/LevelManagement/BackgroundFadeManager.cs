// fuck file headers copy my code idc
// this class deals with fading in and out each level

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BackgroundFadeManager : MonoBehaviour
{
    float fadeTime = 1.0f;
    float currentFadeTime = 1.0f;

    bool fadeDone = false;

    bool fadeIn = false;

    public event Action<BackgroundFadeManager> FadeInComplete;

    public event Action<BackgroundFadeManager> FadeOutComplete;

    // fade out the screen whenever needed
    void Update ()
    {
        if (currentFadeTime > 0)
        {
            GetComponent<Image>().enabled = true;

            currentFadeTime -= Time.deltaTime;

            Color color = Color.black;

            if (fadeIn)
            {
                color.a = 1.0f - (currentFadeTime / fadeTime);
            }
            else
            {
                color.a = currentFadeTime / fadeTime;
            }

            GetComponent<Image>().color = color;
        }
        else
        {
            if (!fadeDone)
            {
                fadeDone = true;

                if(fadeIn)
                {
                    if (FadeInComplete != null)
                    {
                        FadeInComplete(this);
                    }
                }
                else
                {
                    if (FadeOutComplete != null)
                    {
                        FadeOutComplete(this);
                    }
                }
            }
            if (!fadeIn)
            {
                GetComponent<Image>().enabled = false;
            }
        }
    }

    // reset fade variables
    public void SetTimer(float timer, bool _fadeIn)
    {
        fadeDone = false;
        fadeTime = timer;
        currentFadeTime = timer;
        fadeIn = _fadeIn;
    }
}
