using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Player", menuName = "CyberRun/Player")]
public class PlayerSO : ScriptableObject, IDamageCntrl
{
    public float health = 100.0f;

    public Material reactionMaterial;

    public float GetHealth() => health;

    public Material GetDamageReaction() => reactionMaterial;

    public void Kill(GameObject gameObject)
    {
        
    }

    public void UpdateHealth(float damage)
    {
        EventManager.Instance.InvokeOnUpdateHealth(damage, 0.0f);
    }
}
