using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCntrl : MonoBehaviour
{
    private float health = 100.0f;
    private float maxHealth = 100.0f;

    // Update is called once per frame
    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void SetDamage(float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        EventManager.Instance.InvokeUpdateHealthRatio(health, maxHealth);
        Debug.Log("Player health is zero ...");

        if (health <= 0.0f)
        {
        }
    }
}
