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

    public event Action<int> OnNewRun = delegate { };
    public void InvokeOnNewRun(int level) => OnNewRun.Invoke(level);

    public event Action<float, float> OnUpdateHealth = delegate { };
    public void InvokeOnUpdateHealth(float health, float maxHealth) => OnUpdateHealth.Invoke(health, maxHealth);

    public event Action<GameObject> OnChangeGun = delegate { };
    public void InvokeOnChangeGun(GameObject weapon) => OnChangeGun.Invoke(weapon);

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
