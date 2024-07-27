using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCntrl : MonoBehaviour
{
    private float health = 0.0f;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            EventManager.Instance.InvokeOnUpdateXP(10);

            Destroy(gameObject);
        }
    }
}
