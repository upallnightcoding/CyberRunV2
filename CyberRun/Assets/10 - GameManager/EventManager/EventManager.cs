using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager 
{
    // UI Events
    //==========
    public event Action<float> OnSliderMovement = delegate { };
    public void InvokeOnSliderMovement(float slider) => OnSliderMovement.Invoke(slider);

    public event Action<long> OnUpdateXP = delegate { };
    public void InvokeOnUpdateXP(long xp) => OnUpdateXP.Invoke(xp);

    public event Action<float, float> OnUpdateHealthRatio = delegate { };
    public void InvokeUpdateHealthRatio(float health, float maxHealth) => 
        OnUpdateHealthRatio.Invoke(health, maxHealth);

    // Event Manager Singleton
    //========================
    public static EventManager Instance
    {
        get
        {
            if (aInstance == null)
            {
                aInstance = new EventManager();
            }

            return (aInstance);
        }
    }

    public static EventManager aInstance = null;
}
